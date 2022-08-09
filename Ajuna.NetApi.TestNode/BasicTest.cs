using Ajuna.NetApi.Model.FrameSystem;
using Ajuna.NetApi.Model.Rpc;
using Ajuna.NetApi.Model.SpCore;
using Ajuna.NetApi.Model.Types;
using Ajuna.NetApi.Model.Types.Base;
using NUnit.Framework;
using Schnorrkel.Keys;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ajuna.NetApi.TestNode
{
    public class BasicTest
    {
        private const string WebSocketUrl = "ws://127.0.0.1:9944";

        private SubstrateClient _substrateClient;


        // Secret Key URI `//Alice` is account:
        // Secret seed:      0xe5be9a5092b81bca64be81d212e7f2f9eba183bb7a90954f7b76361f6edb5c0a
        // Public key(hex):  0xd43593c715fdd31c61141abd04a99fd6822c8558854ccde39a5684e7a56da27d
        // Account ID:       0xd43593c715fdd31c61141abd04a99fd6822c8558854ccde39a5684e7a56da27d
        // SS58 Address:     5GrwvaEF5zXb26Fz9rcQpDWS57CtERHpNehXCPcNoHGKutQY
        public MiniSecret MiniSecretAlice => new MiniSecret(Utils.HexToByteArray("0xe5be9a5092b81bca64be81d212e7f2f9eba183bb7a90954f7b76361f6edb5c0a"), ExpandMode.Ed25519);
        public Account Alice => Account.Build(KeyType.Sr25519, MiniSecretAlice.ExpandToSecret().ToBytes(), MiniSecretAlice.GetPair().Public.Key);

        // Secret Key URI `//Bob` is account:
        // Secret seed:      0x398f0c28f98885e046333d4a41c19cee4c37368a9832c6502f6cfd182e2aef89
        // Public key(hex):  0x8eaf04151687736326c9fea17e25fc5287613693c912909cb226aa4794f26a48
        // Account ID:       0x8eaf04151687736326c9fea17e25fc5287613693c912909cb226aa4794f26a48
        // SS58 Address:     5FHneW46xGXgs5mUiveU4sbTyGBzmstUspZC92UhjJM694ty
        public MiniSecret MiniSecretBob => new MiniSecret(Utils.HexToByteArray("0x398f0c28f98885e046333d4a41c19cee4c37368a9832c6502f6cfd182e2aef89"), ExpandMode.Ed25519);
        public Account Bob => Account.Build(KeyType.Sr25519, MiniSecretBob.ExpandToSecret().ToBytes(), MiniSecretBob.GetPair().Public.Key);

        [SetUp]
        public void Setup()
        {
            _substrateClient = new SubstrateClient(new Uri(WebSocketUrl));

        }

        [TearDown]
        public void TearDown()
        {
            _substrateClient.Dispose();
        }

        [Test]
        public async Task GetMethodChainNameTestAsync()
        {
            await _substrateClient.ConnectAsync(false, CancellationToken.None);

            var result = await _substrateClient.GetMethodAsync<string>("system_chain");
            Assert.AreEqual("Development", result);

            await _substrateClient.CloseAsync();
        }

        /// <summary>
        /// >> AccountParams
        ///  The full account information for a particular account ID.
        /// </summary>
        public static string AccountParams(AccountId32 key)
        {
            return RequestGenerator.GetStorage("System", "Account", Model.Meta.Storage.Type.Map, new Model.Meta.Storage.Hasher[] {
                        Model.Meta.Storage.Hasher.BlakeTwo128Concat}, new IType[] {
                        key});
        }

        /// <summary>
        /// >> Account
        ///  The full account information for a particular account ID.
        /// </summary>
        public async Task<AccountInfo> AccountInfo(SubstrateClient _client, AccountId32 key, CancellationToken token)
        {
            string parameters = AccountParams(key);
            return await _client.GetStorageAsync<AccountInfo>(parameters, token);
        }

        [Test]
        public async Task GetBalanceTestAsync()
        {
            await _substrateClient.ConnectAsync(false, CancellationToken.None);

            var account32 = new AccountId32();
            account32.Create(Utils.GetPublicKeyFrom(Alice.Value));

            var result = await AccountInfo(_substrateClient, account32, CancellationToken.None);

            Assert.IsTrue(result != null);
            Assert.AreEqual("999998999999874999852", result.Data.Free.Value.ToString());

            await _substrateClient.CloseAsync();
        }

        /// <summary>
        /// Simple extrinsic tester
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="extrinsicUpdate"></param>
        static void ActionExtrinsicUpdate(string subscriptionId, ExtrinsicStatus extrinsicUpdate)
        {
            switch (extrinsicUpdate.ExtrinsicState)
            {
                case ExtrinsicState.None:
                    Assert.IsTrue(true);
                    Assert.IsTrue(extrinsicUpdate.InBlock.Value.Length > 0 || extrinsicUpdate.Finalized.Value.Length > 0);
                    break;
                case ExtrinsicState.Future:
                    Assert.IsTrue(false);
                    break;
                case ExtrinsicState.Ready:
                    Assert.IsTrue(true);
                    break;
                case ExtrinsicState.Dropped:
                    Assert.IsTrue(false);
                    break;
                case ExtrinsicState.Invalid:
                    Assert.IsTrue(false);
                    break;
            }
        }

    }
}