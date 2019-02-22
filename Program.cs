using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NBitcoin;
using Org.BouncyCastle.Math.EC;
using static NBitcoin.Crypto.Hashes;
using static MuSigWasabi.ByteArrayHelpers;

namespace MuSigWasabi
{
	class Alice
	{
		public Key Key { get; }
		public byte[] PubKey => Key.PubKey.ToBytes();
		public ECPoint PubKeyx => Secp256k1.G.Multiply(Key.)
		public byte[] L { get; set; }

		public byte[] Scalar => SHA256(Concat(L, PubKey));

		public Alice(Key key)
		{
			Key = key;
			PubKeyx = new ECKey()
		}
	}

	class Program
	{
		public static int N = 3;

		static void Main(string[] args)
		{
			// We have N Alices with a key pair each
			var alices = new List<Alice>();
			for(var i=0; i<N; i++)
			{
				var password = new String('a', N+1);
				var signingKey = GetKeyFromPassword(password);
				var alice = new Alice(signingKey);
				alices.Add(alice); 
			}
			
			// Combining public keys...  L=H(x1, x2,...,xn)
			var pubkeys = alices.Select(a=>a.PubKey);
			var ell = SHA256(Concat(pubkeys));
			foreach(var alice in alices)
			{
				alice.L = ell;
			}


			var combinedPubKey = new BigInteger();
			foreach(var alice in alices)
			{				
				combinedPubKey +=  alice.PubKey
			}



		}

		private static Key GetKeyFromPassword(string password)
		{
			var salt = Encoding.UTF8.GetBytes("salt");
			var kdf = new Rfc2898DeriveBytes(password, salt);
			return new Key(kdf.GetBytes(32));
		}

	}
}
