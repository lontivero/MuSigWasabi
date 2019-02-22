using System;
using System.Collections.Generic;
using System.Linq;

namespace MuSigWasabi
{
	class ByteArrayHelpers
	{
		public static byte[] Concat(IEnumerable<byte[]> arrs)
		{
			return Concat(arrs.ToArray());
		}

		public static byte[] Concat(params byte[][] arrs)
		{
			var len = arrs.Sum(a => a.Length);
			var ret = new byte[len];
			var pos = 0;
			foreach (var a in arrs)
			{
				Buffer.BlockCopy(a, 0, ret, pos, a.Length);
				pos += a.Length;
			}
			return ret;
		}
	}
}