using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using NBitcoin.Protocol;
using NBitcoin.RPC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NBitcoin.Altcoins
{
	public class Techcoin : NetworkSetBase
	{
		public static Techcoin Instance { get; } = new Techcoin();

		public override string CryptoCode => "TLC";

		private Techcoin()
		{

		}
		public class TechcoinConsensusFactory : ConsensusFactory
		{
			private TechcoinConsensusFactory()
			{
			}

			public static TechcoinConsensusFactory Instance { get; } = new TechcoinConsensusFactory();

			public override BlockHeader CreateBlockHeader()
			{
				return new TechcoinBlockHeader();
			}
			public override Block CreateBlock()
			{
				return new TechcoinBlock(new TechcoinBlockHeader());
			}
		}

#pragma warning disable CS0618 // Type or member is obsolete
		public class TechcoinBlockHeader : BlockHeader
		{
			public override uint256 GetPoWHash()
			{
				var headerBytes = this.ToBytes();
				var h = NBitcoin.Crypto.SCrypt.ComputeDerivedKey(headerBytes, headerBytes, 1024, 1, 1, null, 32);
				return new uint256(h);
			}
		}

		public class TechcoinBlock : Block
		{
			public TechcoinBlock(TechcoinBlockHeader h) : base(h)
			{
			}
			public override ConsensusFactory GetConsensusFactory()
			{
				return TechcoinConsensusFactory.Instance;
			}
		}
#pragma warning restore CS0618 // Type or member is obsolete

		protected override void PostInit()
		{
			RegisterDefaultCookiePath("TechcoinCore");
		}

		protected override NetworkBuilder CreateMainnet()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				//MajorityEnforceBlockUpgrade = 750,
				//MajorityRejectBlockOutdated = 950,
				//MajorityWindow = 1000,
				BIP34Hash = new uint256(), //"0x0000000319f64f5a0d068d82efb8ce0843f20b3a56fee1bf04707ff92fc484d7\t"
				PowLimit = new Target(new uint256("00000000ffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				MinimumChainWork = new uint256("0x0000000000000000000000000000000000000000000000000000000000000001"),
				PowTargetTimespan = TimeSpan.FromSeconds(14 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(10 * 60),
				PowAllowMinDifficultyBlocks = false,
				//CoinbaseMaturity = 100,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 1815,
				MinerConfirmationWindow = 2016,
				ConsensusFactory = TechcoinConsensusFactory.Instance,
				SupportSegwit = true
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 23 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 56 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 101 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x24, 0x38, 0xA2, 0x2E })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x24, 0x18, 0xCD, 0xC4 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tc"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tc"))
			.SetMagic(0xBD6B0CBF)
			.SetPort(8863)
			.SetRPCPort(24156)
			.SetMaxP2PVersion(70208)
			.SetName("main")
			.AddAlias("mainnet")
			.AddDNSSeeds(new[]
			{
				new DNSSeedData("178.128.221.177", "178.128.221.177")
				//new DNSSeedData("dorado.Techcoin.io", "dorado.Techcoin.io"),
				//new DNSSeedData("block.Techcoin.io", "block.Techcoin.io")
			})
			.AddSeeds(new NetworkAddress[0])
			.SetGenesis("0x00000000324ede63db3fed1baf4c66aac921d3f760ece8c111e9b597ccf34b0a");
			return builder;
		}

		protected override NetworkBuilder CreateTestnet()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				/*MajorityEnforceBlockUpgrade = 51,
				MajorityRejectBlockOutdated = 75,
				MajorityWindow = 100,*/
				BIP34Hash = new uint256(), //0x0000000070b3a96d3400000fffff00000000000000000000000000000000000000000000000000000084e5abb3755c413e7d41500f8e2a5c3f0dd01299cd8ef8
				PowLimit = new Target(new uint256("00000000ffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				MinimumChainWork = new uint256("0x0000000000000000000000000000000000000000000000000000000000000001"),
				PowTargetTimespan = TimeSpan.FromSeconds(14 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(10 * 60),
				PowAllowMinDifficultyBlocks = true,
				//CoinbaseMaturity = 100,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 1512,
				MinerConfirmationWindow = 2016,
				ConsensusFactory = TechcoinConsensusFactory.Instance,
				SupportSegwit = false
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 70 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 134 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 219 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x64, 0x55, 0xA7, 0xAF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x54, 0x75, 0xB3, 0xA4 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("tt"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("tt"))
			.SetMagic(0xFFCAE2CE)
			.SetPort(8863)
			.SetRPCPort(34156)
			.SetMaxP2PVersion(70208)
		   	.SetName("test")
		   	.AddAlias("Techcoin-testnet")
		   	.AddSeeds(new NetworkAddress[0])
		   	.SetGenesis("0x00000000324ede63db3fed1baf4c66aac921d3f760ece8c111e9b597ccf34b0a");
			return builder;
		}

		protected override NetworkBuilder CreateRegtest()
		{
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 150,
				/*MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,*/
				BIP34Hash = new uint256(),
				PowLimit = new Target(new uint256("7fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				MinimumChainWork = new uint256("0x0000000000000000000000000000000000000000000000000000000000000000"),
				PowTargetTimespan = TimeSpan.FromSeconds(14 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(10 * 60),
				PowAllowMinDifficultyBlocks = true,
				//CoinbaseMaturity = 100,
				PowNoRetargeting = true,
				RuleChangeActivationThreshold = 108,
				MinerConfirmationWindow = 144,
				ConsensusFactory = TechcoinConsensusFactory.Instance,
				SupportSegwit = false
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 70 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 136 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 219 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x64, 0x55, 0xA7, 0xAF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x54, 0x75, 0xB3, 0xA4 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, Encoders.Bech32("bcrt"))
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, Encoders.Bech32("bcrt"))
			.SetMagic(0xDCB7C1FC)
			.SetPort(18444)
			.SetRPCPort(18332)
			.SetMaxP2PVersion(70208)
			.SetName("TLC-reg")
			.AddAlias("TLC-regtest")
			.AddAlias("Techcoin-reg")
			.AddAlias("Techcoin-regtest")
			.SetUriScheme("Techcoin")
			.AddSeeds(new NetworkAddress[0])
			.SetGenesis("0x63b08043ab6fddd2535409cfb9da47805e9c42f3f8cb3aa1aaef6619dc683b20");
			return builder;
		}

	}
}
