using Ajuna.NetApi;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerTest
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            // configure serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console()
                .CreateLogger();

            // Add this to your C# console app's Main method to give yourself
            // a CancellationToken that is canceled when the user hits Ctrl+C.
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                Log.Information("Canceling...");
                cts.Cancel();
                e.Cancel = true;
            };

            try
            {
                Log.Information("Press Ctrl+C to end.");
                await MainAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                // This is the normal way we close.
            }

            // Finally, once just before the application exits...
            Log.CloseAndFlush();
        }

        private static async Task MainAsync(CancellationToken token)
        {
            // ngrok = "wss://0082-84-75-48-249.ngrok.io";
            // shardHex = "2WTKarArPH1jxUCCDMbLvmDKG9UiPZxfBrb2eQUWyU3K";

            var substrateClient = new SubstrateClient(new Uri("ws://0082-84-75-48-249.ngrok.io"));
            await substrateClient.ConnectAsync(false, false, CancellationToken.None);

            Log.Information("substrateClient.IsConnected = {flag}", substrateClient.IsConnected);

            var shieldingTask = await substrateClient.InvokeAsync<string>("author_getShieldingKey", null, CancellationToken.None);

            Log.Information("shieldingTask = {bytes}", shieldingTask);


        }
    }
}
