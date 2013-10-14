using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SERV.Utils.Caching
{
	public class CacheManager
	{
		private static readonly Logger Log = new Logger();
		
		private static bool? _CacheEnabled = null;
		public static bool CacheEnabled
		{ 
			get
			{
				if (_CacheEnabled.HasValue == false)
				{
					string CacheEnabledString = System.Configuration.ConfigurationManager.AppSettings["CacheEnabled"];
					bool NewValue = false;
					if (CacheEnabledString == null)
					{
						throw new Exception("The Configuration file setting CacheEnabled is not set.");
					}
					else if(bool.TryParse(CacheEnabledString, out NewValue) == false)
					{
						throw new Exception("Converting the Configuration file setting CacheEnabled to Bool failed.");
					}
					_CacheEnabled = NewValue;
				}
				return _CacheEnabled.Value;
			}
		}

		private static string _CacheLoc = null;
		private static string CacheLoc
		{
			get
			{
				if (_CacheLoc == null)
				{
					_CacheLoc = System.Configuration.ConfigurationManager.AppSettings["CachePath"];
					if (_CacheLoc == null)
					{
						throw new Exception("The Configuration file setting CachePath is not set.");
					}
				}
				return _CacheLoc;
			}
		}

		private static int _MaxCacheAgeMins = -63783;
		private static int MaxCacheAgeMins
		{
			get
			{
				if(_MaxCacheAgeMins == -63783)
				{
					string MaxCacheAgeMinsString = System.Configuration.ConfigurationManager.AppSettings["CacheTTLMins"];
					if (MaxCacheAgeMinsString == null)
					{
						throw new Exception("The Configuration file setting CacheTTLMins is not set.");
					}
					else if(Int32.TryParse(MaxCacheAgeMinsString, out _MaxCacheAgeMins) == false)
					{
						throw new Exception("Converting the Configuration file setting CacheTTLMins to Int failed.");
					}
				}
				return _MaxCacheAgeMins;
			}
		}

		public static string GetCacheIdentifier(string uniqueKey)
		{
			byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(uniqueKey);
			bytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
			string identifier = "";
			foreach (byte b in bytes)
			{
				identifier += Convert.ToString(b, 16).ToUpper().PadLeft(2, '0');
			}
			return identifier;
		}
		
		public static T GetFromCache<T>(object ID) where T : class
		{
			return GetFromCache<T>(ID, "");
		}
		
		public static T GetFromCache<T>(object ID, string key) where T:class
		{
			return GetFromCache<T>(ID, key, false, -1);
		}
		
		public static T GetFromCache<T>(object ID, string key, bool ignoreCacheEnabled, int maxCacheAgeOverride) where T:class
		{
			int maxAge = MaxCacheAgeMins;
			if (maxCacheAgeOverride > 0) { maxAge = maxCacheAgeOverride; }
			if (!CacheEnabled && !ignoreCacheEnabled) { return null; }
			string cachePath = string.Format(@"{0}{1}[{2}]-{3}.cached", CacheLoc, System.IO.Path.DirectorySeparatorChar, key, ID.ToString());
			Log.Debug("CACHE: Getting " + cachePath + " from cache");
			if (File.Exists(cachePath))
			{
				FileInfo info = new FileInfo(cachePath);
				DateTime cacheCreated = info.LastWriteTime;
				info = null;
				if (cacheCreated > DateTime.Now.AddMinutes(maxAge * -1))
				{
					Log.Debug("CACHE: Returning cached version");
					StreamReader r;
					try
					{
						r = new StreamReader(cachePath);
					}
					catch
					{
						return null;
					}
					BinaryFormatter bf = new BinaryFormatter();
					r.BaseStream.Position = 0;
					T result = (T)bf.Deserialize(r.BaseStream);
					r.Close();
					return result;
				}
				else
				{
					Log.Debug("CACHE: Cache is stale");
					KillCachedObject(cachePath);
					return null;
				}
			}
			else
			{
				Log.Debug("CACHE: Nothing cached");
				return null;
			}
		}

		public static void SaveToCache(object obj, object ID)
		{
			SaveToCache(obj, ID, "");
		}
		
		public static void SaveToCache(object obj, object ID, string key)
		{
			SaveToCache(obj, ID, key, false);
		}
		
		public static void SaveToCache(object obj, object ID, string key, bool ignoreCacheEnabled)
		{
			if (!CacheEnabled && !ignoreCacheEnabled) { return; }
			if (obj == null) { return; }
			int rnd = new Random().Next(int.MaxValue-1);
			string cachePath = string.Format(@"{0}{1}[{2}]-{3}.cached{4}", CacheLoc, System.IO.Path.DirectorySeparatorChar, key, ID.ToString(), rnd.ToString());
			string destCachePath = string.Format(@"{0}{1}[{2}]-{3}.cached", CacheLoc, System.IO.Path.DirectorySeparatorChar, key, ID.ToString());
			BinaryFormatter bf = new BinaryFormatter();
			try
			{
				using (StreamWriter r = new StreamWriter(cachePath, false))
				{
					Log.Debug("CACHE: Caching " + cachePath);
					r.BaseStream.Position = 0;
					bf.Serialize(r.BaseStream, obj);
					r.Flush();
					r.Close();
				}
				File.Move(cachePath, destCachePath);
			}
			catch(Exception e) 
			{ 
				Log.Error ("CACHE: " + e.Message, e); 
			}
		}

		public static void KillCachedObject(string fileName)
		{
			try
			{
				Log.Debug(string.Format("CACHE: Killing cached object {0}", fileName));
				File.Delete(fileName);
			}
			catch { }
		}

	}
}
