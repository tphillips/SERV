using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SERV.Utils.Caching
{
	public class CallStackCaching
	{

		// How many method calls to ignore from the end of the stack, probably not needed if IGNORE_ASSEMBLIES_IN_SIG is right
		const int STACK_WALK = 0;
		const int FILE_SIG_PADDING = 700;
		const string IGNORE_ASSEMBLIES_IN_SIG = "System,System.Web,System.Web.RegularExpressions,App_Web_,Mono,Mono.WebServer2,DBHelperProvider,SERV.Utils,mscorlib,System.Web.Extensions,";
		const string IGNORE_ASSEMBLIES_IN_SIG2 = "App_Web_";
		const string DEFAULT_CACHE_MATRIX_HEADER = "# This cache matrix file controls the behaviour of caching for all apps that use the stack.  \r\n" +
			"# The matrix consists of 3 columns; Cache Signature, Cache True/False, and Cache TTL Override.  \r\n" +
				"# The Cache Sig is a signature generated from the call stack each time a data access method is called.  \r\n" +
				"# The signature identifies the precise source of the call, allowing individual settings to be specified for each combo of application, BLL method & DAL method. \r\n" +
				"# Cache True/False specifies if the call should be cached / recalled from cache.  \r\n" +
				"# The TTL Override allows the default cache TTL which is specified in the *.config to be overridden.  A value of 0 = no override.\r\n" +
				"# For example, a method such as ListCountries should probably be cached for a high duration (days).  \r\n" +
				"# A method such as ListUnpaidOrders should probably be cahed for a shorter period, such as 1 minute, if at all for admin. \r\n" +
				"# If a cahe matrix line does not exist for the method signature, one will be generated with default values;  \r\n" +
				"# The dafult values will be the signature, the value of the *.configs CacheEnabled and 0. \r\n" +
				"# Using CacheMatrix.aspx in the admin app the matrix can be viewed and edited in place.\r\n" +
				"# Usefully, the whole file can be copied and pasted into a csv, and edited in excel. \r\n" +
				"# Remember, each node will have its own matrix and own set of cache files. So synchronicity will need to be considered.  There is purposefully no use of the sync service. \r\n" +
				"# The cache matrix is stored in the same location as the cache files.\r\n\r\n";
				
		private static readonly Logger Log = new Logger();
		private static bool ConfigLoaded = false;
		private static string cacheMatrixFileContents;
		private static string cachePath = null;
		private static string lockPath = null;
		private static string confPath = null;

		private static SortedDictionary<string, StackSigCacheConfig> cacheMatrix = new SortedDictionary<string, StackSigCacheConfig>();

		private static string CachePath
		{
			get
			{
				if (cachePath == null)
				{
					cachePath =  System.Configuration.ConfigurationManager.AppSettings ["CachePath"];
				} 
				return cachePath;
			}
		}

		private static string LockPath
		{
			get
			{
				if (lockPath == null)
				{
					lockPath = string.Format("{0}{1}CacheMatrix.lock", CachePath, System.IO.Path.DirectorySeparatorChar);
				}
				return lockPath;
			}
		}

		private static string ConfPath
		{
			get
			{
				if (confPath == null)
				{
					confPath = string.Format("{0}{1}CacheMatrix.conf", CachePath, System.IO.Path.DirectorySeparatorChar);
				}
				return confPath;
			}
		}

		public static SortedDictionary<string, StackSigCacheConfig> CacheMatrix
		{
			get
			{
				if (cacheMatrix.Count == 0 && ConfigLoaded == false)
				{
					LoadAndMergeCacheMatrix();
				}
				return cacheMatrix;
			}
		}

		public static DataTable CacheMatrixDataTable
		{
			get
			{
				return GetCacheMatrixDataTable();
			}
		}

		public static string CacheMatrixFileContents
		{
			get
			{
				if (cacheMatrix.Count == 0 && ConfigLoaded == false)
				{
					LoadAndMergeCacheMatrix();
				}
				return cacheMatrixFileContents;
			}
		}

		public CallStackCaching()
		{
		}

		public static void ClearCache()
		{
			// DMK ToDo: Consider using Windows FindFirst + FindNext for this - GetFiles is slow and returns all - see: http://ripper234.com/p/c-alternative-to-directorygetfiles/
			// Note - DotNet 4.5 has Directory.EnumerateFiles that may be using FindFirst + FindNext
			string[] files = Directory.GetFiles(CachePath, "*.cached");
			foreach (string f in files)
			{
				File.Delete(f);
			}
		}

		public static int CountCacheFiles()
		{
			return Directory.GetFiles(CachePath, "*.cached").Length;
		}

		/// <summary>
		/// Called to force the update of the ConfigFile
		/// </summary>
		/// <remarks>
		/// This will NOT force cacheMatrix to be updated on it's own, call LoadCacheMatrixFile to update it.
		/// In fact, the existing memory structures must remain as they were.
		/// </remarks>
		/// <param name="val"></param>
		public static void OverwriteCacheMatrixFile(string val)
		{
			if (!File.Exists(LockPath))
			{
				File.Create(LockPath).Close();
				using (StreamWriter w = new StreamWriter(ConfPath, false))
				{
					w.WriteLine(val);
					w.Flush();
					w.Close();
				}
				KillLockFile();
			}
			else
			{
				throw new Exception("File Locked");
			}
		}

		private static DataTable GetCacheMatrixDataTable()
		{
			DataTable ret = new DataTable();
			ret.Columns.Add("No", typeof(int));
			ret.Columns.Add("App", typeof(string));
			ret.Columns.Add("Caching Enabled", typeof(bool));
			ret.Columns.Add("TTL Override", typeof(int));
			ret.Columns.Add("Stack Signature", typeof(string));
			int x = 1;
			foreach(string k in CacheMatrix.Keys)
			{
				StackSigCacheConfig c = CacheMatrix[k];
				string[] sigParts = c.Signature.Split(':');
				string app = sigParts[sigParts.Length-2].Split('.')[0];
				ret.Rows.Add(new object[]{x, app, c.Cache, c.MaxAgeOverride, c.Signature });
				x++;
			}
			return ret;
		}

		/// <summary>
		/// Merges the CacheMatrix file into the cacheMatrix SortedDictionary in memory, unless the lockfile exists
		/// </summary>
		/// <remarks>
		/// If the LockFile exists then this leaves the existing cacheMatrix (if any) alone.
		/// The merge logic assumes that the file's settings overrule the in-memory settings.
		/// The code expects cacheMatrix to be valid after a call to this - DO NOT remove the SortedDictionary create.
		/// </remarks>
		public static void LoadAndMergeCacheMatrix()
		{
			if(cacheMatrix == null) cacheMatrix = new SortedDictionary<string, StackSigCacheConfig>();
			if (!File.Exists(LockPath))
			{
				LoadAndMergeCacheMatrixIgnoringLockFile();
			}
		}

		/// <summary>
		/// Merges the CacheMatrix file into the cacheMatrix SortedDictionary in memory, regardless whether the lockfile exists
		/// </summary>
		/// <remarks>
		/// This function is called during the SaveCacheMatrix to merge the existing configuration file before overwriting.
		/// The code expects cacheMatrix to be valid after a call to this - DO NOT remove the SortedDictionary create.
		/// ConfigOverwrite is the only way to reset the config file without merging, but, even so, the config file will soon contain the merges again.
		/// </remarks>
		public static void LoadAndMergeCacheMatrixIgnoringLockFile()
		{
			if (cacheMatrix == null) cacheMatrix = new SortedDictionary<string, StackSigCacheConfig>();
			if (File.Exists(ConfPath))
			{
				Log.Debug(string.Format("CACHEMATRIX: Loading cache matrix from {0}", ConfPath));
				File.Create(LockPath).Close();
				using (StreamReader r = new StreamReader(ConfPath))
				{
					cacheMatrixFileContents = r.ReadToEnd();
					r.Close();
					string[] lines = cacheMatrixFileContents.Split('\n');
					foreach (string line in lines)
					{
						if (!line.Trim().StartsWith("#") && line.Trim() != string.Empty)
						{
							string l = line.Trim();
							string[] parts = l.Split(',');
							StackSigCacheConfig c = new StackSigCacheConfig(parts[0].Trim());
							if (parts.Length > 1) { c.Cache = Convert.ToBoolean(parts[1].Trim()); }
							if (parts.Length > 2) { c.MaxAgeOverride = Convert.ToInt32(parts[2].Trim()); }
							parts[0] = parts[0].Trim();
							if (cacheMatrix.ContainsKey(parts[0]))
								cacheMatrix[parts[0]] = c;
							else
								cacheMatrix.Add(parts[0], c);
						}
					}
				}
				ConfigLoaded = true;
				KillLockFile();
				Log.Debug(string.Format("CACHEMATRIX: Loaded {0} lines from {1}", cacheMatrix.Count, ConfPath));
			}
		}

		/// <summary>
		/// Writes the cacheMatrix to the Configuration file - using file locking to prevent reading whilst writing
		/// </summary>
		/// <remarks>
		/// Before writing the file out, the existing file is read and merged into cacheMatrix (Ignoring any existing file-lock file).
		/// If the read fails, it does not bother to save, we'll never write the file out unless we're dead sure that we're not destroying what's in the file.
		/// </remarks>
		public static void SaveCacheMatrix()
		{
			Log.Debug(string.Format("CACHEMATRIX: Attempting to save cache matrix of {0} items to {1}", cacheMatrix.Count, ConfPath));
			if (!File.Exists(LockPath))
			{
				File.Create(LockPath).Close();
				LoadAndMergeCacheMatrixIgnoringLockFile();
				using (StreamWriter w = new StreamWriter(ConfPath, false))
				{
					w.WriteLine(DEFAULT_CACHE_MATRIX_HEADER);
					foreach (string k in cacheMatrix.Keys)
					{
						StackSigCacheConfig c = cacheMatrix [k];
						w.WriteLine(string.Format("{0},{1},{2}", c.Signature.PadRight(FILE_SIG_PADDING), c.Cache, c.MaxAgeOverride.ToString()));
					}
					w.Flush();
					w.Close();
				}
				KillLockFile();
				Log.Debug(string.Format("CACHEMATRIX: Saved cache matrix to {0}", ConfPath));
			} 
			else
			{
				CheckLockFileAge();
			}
		}

		static void CheckLockFileAge()
		{
			try
			{
				FileInfo i = new FileInfo(LockPath);
				if (i.CreationTime < DateTime.Now.AddMinutes(-3))
				{
					KillLockFile();
				}
			} 
			catch { }
		}

		public static void KillLockFile()
		{
			try { File.Delete(LockPath); } catch { }
		}

		static string GetStackSig()
		{
			StackTrace stack = new StackTrace();
			StackFrame[] frames = stack.GetFrames();
			StringBuilder stackSig = new StringBuilder();
			for (int x = STACK_WALK; x < frames.Length; x++)
			{
				StackFrame f = frames[x];
				System.Reflection.MethodBase method = f.GetMethod();
				string assembly = method.DeclaringType.Assembly.FullName.Split(',')[0];
				// Look for [NoCache] attributes
				object[] attributes = method.GetCustomAttributes(true);
				foreach (Attribute a in attributes)
				{
					// If found, return a null sig
					if (a.GetType() == typeof(NoCacheAttribute)) 
					{ 
						Log.Debug(string.Format("CACHEMATRIX: [NoCache] block produced by {0}.{1}.{2}()", assembly, method.DeclaringType.Name, method.Name));
						return null; 
					}
				}
				if (!IGNORE_ASSEMBLIES_IN_SIG.Contains(assembly) && !assembly.StartsWith(IGNORE_ASSEMBLIES_IN_SIG2))
				{
					stackSig.Append(string.Format("{0}.{1}.{2}():", assembly, method.DeclaringType.Name, method.Name));
				}
			}
			return stackSig.ToString();
		}

		static StackSigCacheConfig GetStackSigConfig()
		{
			string stackSig = GetStackSig();
			// if the sig is null, return a null sig config (stops caching)
			if (stackSig == null) { return new StackSigCacheConfig(null); }
			// Check for a cache config override in the matrix
			if (!CacheMatrix.ContainsKey(stackSig))
			{
				Log.Info(string.Format("CACHEMATRIX: Cache Matrix does not have an entry for:\r\n\t{0}", stackSig));
				// This line will add a row to the cache matrix with caching enabled or disabled DEPENDING ON THE web.configs CacheEnabled setting
				// This means the web.configs CacheEnabled setting is the /default/ caching behaviour, however each cahe matrix entry can override that
				StackSigCacheConfig c = new StackSigCacheConfig(stackSig) { Cache = CacheManager.CacheEnabled };
				try
				{
					CacheMatrix.Add(stackSig, c); 
					SaveCacheMatrix();
				}
				catch{}
				return c;
			}
			return CacheMatrix[stackSig];
		}

		public static void SaveToCache(string key, object obj)
		{
			try
			{
				if (GetStackSigConfig().Cache)
				{
					SERV.Utils.Caching.CacheManager.SaveToCache(obj, 0, SERV.Utils.Caching.CacheManager.GetCacheIdentifier(key), true);
				}
			} 
			catch (Exception e)
			{
				Log.Warn("A (now non-fatal) caching error occured at CallStackCaching::SaveToCache() - " + e.Message);
			}
		}

		public static T GetFromCache<T>(string key) where T:class
		{
			try
			{
				StackSigCacheConfig c = GetStackSigConfig();
				Log.Debug(string.Format("CACHEMATRIX: CallStackCaching.GetFromCache() with sig {0} is {1}", c.Signature, c.Cache));
				if (c.Cache)
				{
					object ret = SERV.Utils.Caching.CacheManager.GetFromCache<T>(0, SERV.Utils.Caching.CacheManager.GetCacheIdentifier(key), true, c.MaxAgeOverride);
					return (T)ret;
				} 
				else
				{
					return null;
				}
			} 
			catch (Exception e)
			{
				Log.Warn("A (now non-fatal) caching error occured at CallStackCaching::GetFromCache() - " + e.Message);
				return null;
			}
		}

	}
}

