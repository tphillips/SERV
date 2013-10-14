using System;
using System.Net;
using System.Text;

namespace SERV.Utils.Translate
{
	public class Google
	{
		const string BASE_URL = "https://www.googleapis.com/language/translate/v2";
		const string FORMAT = "{0}?key={1}&q={2}&source={3}&target={4}";
		
		public static string GetTranslation(string src, string srcLang, string dstLang , string key)
		{
			if (string.IsNullOrEmpty(src)) { return src; }
			string ret="";
			if(src.Length > 1000)
			{
				ret = TranslateInSmallChunks(src, ret, srcLang, dstLang, key);
			}
			else
			{
				ret = MakeAndParseRequest(src, srcLang, dstLang, key);
			}
			if (ret == null){ throw new Exception("Translation failed (Returned Null)"); }
			return ret;
		}

		private static string TranslateInSmallChunks(string src, string ret, string srcLang, string dstLang, string key)
		{
			int start = 0;
			int end = 950;
			while (start < src.Length)
			{
				int endIndex = 0;
				if (end + start >= src.Length || ((src.Length - start) <1000)) //if we the next chunk is past the original length or if less than 1000 characters left
				{
					endIndex = src.Length; //then send it all in one go
				}
				else
				{
					//find the first index of a '.' soon after the the next chunk. Then use this index as the length for substring.
					endIndex = src.IndexOf('.', end + start);
					if (endIndex == -1 || endIndex - start > 1000)
					{
						endIndex = src.IndexOf(' ', end + start);
					}
					if (endIndex == -1)
					{
						endIndex = end + start; //if no space found then 
					}
					endIndex++;
				}
				string source = src.Substring(start, endIndex-start); 
				ret += MakeAndParseRequest(source, srcLang, dstLang, key);
				start = endIndex;
			}
			return ret;
		}

		private static string MakeAndParseRequest(string src, string srcLang, string dstLang, string key)
		{
			try
			{
				string req = System.String.Format(FORMAT, BASE_URL, key, src, srcLang, dstLang);
				//Console.WriteLine(req);
				WebClient c = new WebClient();
				// MUST add a known browser user agent or else response encoding doesn't return UTF-8 (what the hell Google?)
				c.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
				c.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");

				// Make sure we have response encoding to UTF-8
				c.Encoding = Encoding.UTF8;
				string res = c.DownloadString(req);
                string[] lines = res.Split('\n');
				res = "";
                foreach(string l in lines)
                {
                    if (l.Contains("translatedText"))
                    {
                        string[] parts = l.Split(':');
						for (int x = 1; x < parts.Length; x++)
						{
                        	res += parts[x].Replace("\"", "").Replace("\r","").Replace("\n","").Trim();
							if (parts.Length >= 3 && x != parts.Length -1) { res += ": "; }
						}
						//Replace the zero width space
						res = res.Replace("\\u200b", ""); //.Replace("u200D","").Replace("uFEFF","");
                        return res.Trim();
                    }
                }
				res = res.Replace("\n", "");
				throw new Exception("No translatedText entry found, result returned without the NewLine chars:[" + (res ?? "Null") + "]");
			} 
			catch(Exception ex)
			{
				throw new Exception("Google Translate Failed [" + (ex.Message ?? "Null") + "]", ex);
			}
		}
	}
}

