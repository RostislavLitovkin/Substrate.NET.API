﻿using Ajuna.NetApi;
using Ajuna.NetApi.Model.AjunaCommon;
using Ajuna.NetWallet;
using Ajuna.UnityInterface;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dot4GBot
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var config = new LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new FileTarget("logfile")
            {
                FileName = "log.txt",
                DeleteOldFileOnStartup = true
            };

            var logconsole = new ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            //config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;

            // Add this to your C# console app's Main method to give yourself
            // a CancellationToken that is canceled when the user hits Ctrl+C.
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                Console.WriteLine("Canceling...");
                cts.Cancel();
                e.Cancel = true;
            };

            try
            {
                Console.WriteLine("Press Ctrl+C to end.");
                await MainAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                // This is the normal way we close.
            }
        }

        public enum NodeState
        {
            None,
            Faucet,
            Queue,
            Players,
            Play,
            Worker,
        }

        private static async Task MainAsync(CancellationToken token)
        {
            SystemInteraction.ReadData = f => File.ReadAllText(Path.Combine(Environment.CurrentDirectory, f));
            SystemInteraction.DataExists = f => File.Exists(Path.Combine(Environment.CurrentDirectory, f));
            SystemInteraction.ReadPersistent = f => File.ReadAllText(Path.Combine(Environment.CurrentDirectory, f));
            SystemInteraction.PersistentExists = f => File.Exists(Path.Combine(Environment.CurrentDirectory, f));
            SystemInteraction.Persist = (f, c) => File.WriteAllText(Path.Combine(Environment.CurrentDirectory, f), c);

            Wallet wallet = new Wallet();
            await wallet.CreateAsync("aA1234dd");
            await wallet.StartAsync("ws://127.0.0.1:9944");

            var dot4gClient = new Dot4GClient(wallet,
                "ws://183c-84-75-48-249.ngrok.io",
                "Fdb2TM3owt4unpvESoSMTpVWPvCiXMzYyb42LzSsmFLi",
                "Fdb2TM3owt4unpvESoSMTpVWPvCiXMzYyb42LzSsmFLi");

            Console.WriteLine($"My Address => {wallet.Account.Value}");
            NodeState nodeState = NodeState.None;

            while (!token.IsCancellationRequested)
            {
                if (wallet.AccountInfo == null || wallet.AccountInfo.Data.Free.Value < 1000000000000)
                {
                    nodeState = NodeState.Faucet;
                }
                else
                {
                    var playerQueued = await dot4gClient.GetPlayerQueueAsync();
                    var runnerId = await dot4gClient.GetRunnerIdAsync();

                    if (playerQueued.Value == 0 && runnerId.Value == 0)
                    {
                        nodeState = NodeState.Queue;
                    }
                    else if (playerQueued.Value == 1 && runnerId.Value == 0)
                    {
                        nodeState = NodeState.Players;
                    }
                    else
                    {
                        var runnerState = await dot4gClient.GetRunnerStateAsync(runnerId);
                        if (runnerState is null)
                        {
                            throw new Exception($"Got a runner id {runnerId.Value}, but no state!");
                        }
                        else
                        {
                            switch (runnerState.Value)
                            {
                                case RunnerState.Queued:
                                    nodeState = NodeState.Worker;
                                    break;

                                case RunnerState.Accepted:
                                    nodeState = NodeState.Play;
                                    break;

                                case RunnerState.Finished:
                                    nodeState = NodeState.Queue;
                                    break;
                            }
                        }
                    }
                }

                switch (nodeState)
                {
                    case NodeState.None:
                        break;

                    case NodeState.Faucet:
                        Console.WriteLine($"action: {nodeState}");
                        var faucet = await dot4gClient.FaucetAsync();
                        break;

                    case NodeState.Queue:
                        Console.WriteLine($"action: {nodeState}");
                        var queued = await dot4gClient.QueueAsync();
                        break;

                    case NodeState.Players:
                        Console.WriteLine("Players...");
                        break;

                    case NodeState.Worker:
                        Console.WriteLine("Worker...");
                        break;
                }

                if (nodeState == NodeState.Play)
                {
                    Console.WriteLine($"ready to play!");
                }

                // wait on extrinsic
                if (dot4gClient.HasExtrinsics > 0)
                {
                    Console.Write("extrinics");
                    while (dot4gClient.HasExtrinsics > 0)
                    {
                        Console.Write(".");
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("ok");
                    continue;
                }

                Thread.Sleep(1000);
            }
        }

    }
}
