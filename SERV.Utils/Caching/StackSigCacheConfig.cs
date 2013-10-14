using System;

namespace SERV.Utils.Caching
{
	public class StackSigCacheConfig
	{

		public string Signature { get; set; }
		public bool Cache { get; set; }
		public int MaxAgeOverride { get; set; }

		public StackSigCacheConfig(string sig)
		{
			this.Signature = sig;
			this.Cache = true;
			this.MaxAgeOverride = 0;
			if (sig == null) { this.Cache = false; }
		}

	}
}

