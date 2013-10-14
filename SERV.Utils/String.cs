using System;
using System.Collections.Generic;
using System.Reflection;
using SERV.Utils.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web;

namespace SERV.Utils
{
	public class String
	{
		private const string BLOCKEDWORDS = "clit,cock,coon,cums,cunt,damn,dink,fags,fart,fuck,fuks,hell,jism,jizm,kock,kums,lust,paki,phuk,phuq,piss,porn,shit,slut,smut,twat,wank";
		private const string SALT = "asdf6X87asdD6f87s6dfX87aD6dsf87687sdX6fD8a7s6df66f";

		/// <summary>
		/// Returns the characters representing 1/4 1/2 3/4 for the decimal part of a decimal number, as well as the number part as a string. e.g. 123.25 = 123[seperator]1/4
		/// </summary>
		/// <param name="value">A decimal to be returned, note that this value can be rounded up in the returned string.</param>
		/// <param name="seperator">A seperator to place betwene the whole number and the quarter character or null</param>
		/// <returns>A string like 123[seperator]3/4 - where 3/4 is the actual single character representing three quarters.</returns>
		public static string DecimalToQuarterCharString(decimal value, string seperator)
		{
			const string QUARTERS = "¼½¾";
			decimal[] QUARTERSBOUNDARIES = new decimal[4] { 0.13M, 0.35M, 0.6M, 0.85M };

			decimal Remainder = value - System.Math.Floor(value);

			if (Remainder >= QUARTERSBOUNDARIES[0] && Remainder < QUARTERSBOUNDARIES[1])
			{
				return System.Math.Floor(value) + (seperator ?? "") + QUARTERS[0].ToString();
			}
			else if (Remainder >= QUARTERSBOUNDARIES[1] && Remainder < QUARTERSBOUNDARIES[2])
			{
				return System.Math.Floor(value) + (seperator ?? "") + QUARTERS[1].ToString();
			}
			else if (Remainder >= QUARTERSBOUNDARIES[2] && Remainder < QUARTERSBOUNDARIES[3])
			{
				return System.Math.Floor(value) + (seperator ?? "") + QUARTERS[2].ToString();
			}
			else if (Remainder >= QUARTERSBOUNDARIES[3])
			{
				return (System.Math.Floor(value) + 1).ToString();
			}
			return System.Math.Floor(value).ToString();
		}
		/// <summary>
		/// Limits a string to a max length without cutting words in half.
		/// Optionally adds a ... at the end.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="maxLen"></param>
		/// <param name="suffixEllipse"></param>
		/// <returns></returns>
		public static string LimitStringByWholeWord(string input, int maxLen, bool suffixEllipse)
		{
			return LimitStringEndAtLastCharacterOccurence(input, maxLen, " ", suffixEllipse, false);
		}

		public static string LimitStringToLengthEndAtFullStop(string input, int maxLen)
		{
			return LimitStringEndAtLastCharacterOccurence(input, maxLen, ".", false, true);
		}

		public static string LimitStringEndAtLastCharacterOccurence(string input, int maxLen, string endAt, bool suffixEllipse, bool includeEndAtMatch)
		{
			if (maxLen == 0) { return input; }
			if (System.String.IsNullOrEmpty(input)) { return input; }
			if (input.Length <= maxLen) { return input; }
			int point = maxLen;
			while (point > 0 && input.Substring(point, 1) != endAt)
			{
				point--;
			}
			if (includeEndAtMatch) { point += endAt.Length; }
			string ret = input.Substring(0, point);
			if (suffixEllipse) { ret = ret + " ..."; }
			return ret;
		}

		/// <summary>
		/// Removes unsafe (') from strings and replaces with ('')
		/// Optionally also limits the length.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string MakeSafe(object input)
		{
			return input.ToString().Replace("'", "''");
		}

		/// <summary>
		/// Removes unsafe (') from strings and replaces with ('')
		/// Optionally also limits the length.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="maxLength"></param>
		/// <returns></returns>
		public static string MakeSafe(object input, int maxLength)
		{
			string ret = MakeSafe(input).Trim();
			if (ret.Length > maxLength)
			{
				ret = ret.Substring(0, maxLength);
			}
			return ret;
		}

		///// <summary>
		///// WIP
		///// </summary>
		///// <param name="sql"></param>
		///// <returns></returns>
		//public static string MakeSQLStatementSafe(string sql)
		//{
		//    string pattern = @",[\s]*'.*?'([[\s]]* | \)) | '.*?'[\s]*,";
		//    Regex regex = new Regex(pattern);
		//    MatchCollection matches = regex.Matches(sql);
		//    foreach (Match m in matches)
		//    {
		//        string content = m.Value.Substring(1, m.Value.Length - 2);
		//        content = MakeSafe(content);
		//        content = "'" + content + "'";
		//        sql.Replace(m.Value, content);
		//    }
		//    return sql;
		//}

		/// <summary>
		/// Takes a proper case string (word) and adds spaces
		/// ThisIsATest >> This Is A Test
		/// </summary>
		/// <param name="sIn"></param>
		/// <returns></returns>
		public static string ProperToSpacedString(string sIn)
		{
			string sOut = "";
			foreach (char c in sIn.ToCharArray())
			{
				if ((c >= 65 && c <= 90) || (c >= 48 && c <= 57))
				{
					sOut += " ";
				}
				sOut += c;
			}
			return sOut.Trim();
		}

		public static string StringToProperCaseWord(string sIn, char splitter)
		{
			string sOut = "";
			bool justSplit = true;
			foreach (char c in sIn.ToCharArray())
			{
				if (c == splitter)
				{
					justSplit = true;
				}
				else
				{
					if (justSplit)
					{
						sOut += c.ToString().ToUpper();
					}
					else
					{
						sOut += c;
					}
					justSplit = false;
				}
			}
			return sOut.Trim();
		}

		public static Dictionary<string, string> GetDictFromString(string strIn)
		{
			Dictionary<string, string> ret = new Dictionary<string, string>();
			string[] parts = strIn.Split(';');
			foreach (string p in parts)
			{
				string[] bits = p.Split('=');
				if (bits.Length == 2)
				{
					ret.Add(bits[0], bits[1]);
				}
			}
			return ret;
		}

		public static string GetStringFromDict(Dictionary<string, string> dictIn)
		{
			string res = "";
			foreach (string k in dictIn.Keys)
			{
				res += k + "=" + dictIn[k] + ";";
			}
			return res;
		}

		public static string Base64EncodeString(string toEnc)
		{
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes(toEnc);
			string res = Convert.ToBase64String(bytes);
			return res;
		}

		public static string Base64DecodeString(string toDec)
		{
			byte[] bytes = Convert.FromBase64String(toDec);
			string res = System.Text.Encoding.ASCII.GetString(bytes);
			return res;
		}

		public static string EncodeAndHashString(string toEncode, out string hash)
		{
			string encoded = Base64EncodeString(toEncode);
			string salted = SALT + encoded;
			byte[] saltedBytes = System.Text.Encoding.ASCII.GetBytes(salted);
			byte[] hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(saltedBytes);
			string hashBase64String = Convert.ToBase64String(hashBytes);
			//string hashHex = BitConverter.ToString(hashBytes).Replace("-","").ToLower();
			hash = hashBase64String;
			return encoded;
		}

		public static string DecodeAndValidateHash(string encoded, string hash)
		{
			string salted = SALT + encoded;
			byte[] saltedBytes = System.Text.Encoding.ASCII.GetBytes(salted);
			byte[] hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(saltedBytes);
			string hashBase64String = Convert.ToBase64String(hashBytes);
			if (hash != hashBase64String)
			{
				throw new System.Security.VerificationException();
			}
			return Base64DecodeString(encoded);
		}

		public static Dictionary<string, string> GetDictFromEncodedStringAndHash(string encoded, string hash)
		{
			string decoded = DecodeAndValidateHash(encoded, hash);
			return GetDictFromString(decoded);
		}

		public static string UrlEncode(string toEnc)
		{
			return System.Web.HttpUtility.UrlEncode(toEnc);
		}

		public static string UrlDecode(string toDec)
		{
			return System.Web.HttpUtility.UrlDecode(toDec); 
		}

		public static string GetStringFromDbMember(string propertyName, object src)
		{
			Type typeInfo = src.GetType();
			PropertyInfo pi = typeInfo.GetProperty(propertyName);
			if (pi == null) { throw new InvalidOperationException(); }
			object value = pi.GetValue(src, null);
			if (value == null) { return null; }
			string val = value.ToString();
			object[] attributes = pi.GetCustomAttributes(typeof (Data.DbMemberAttribute), false);
			if (attributes == null || attributes.Length == 0) { return val; }
			DbMemberAttribute db = attributes[0] as DbMemberAttribute;
			if (db == null) { return val; }
			if (db.EnsureHtmlSafeOnSave == true)
			{
				val = MakeSafeHtmlTags(val, db.MaxLength);
			}
			else
			{
				if (db.MaxLength <= 0) { return val; }
				return val.Length > db.MaxLength ? val.Substring(0, db.MaxLength) : val;
			}
			return val;
		}

		/// <summary>
		/// Check if the properties marked as keyfields have been edited. 
		/// If any one is edited, break and return true.
		/// </summary>
		/// <param name="editedObject"></param>
		/// <param name="sourceObject"></param>
		/// <returns></returns>
		public static bool CheckIfKeyDetailsChanged(object editedObject, object sourceObject)
		{
			bool keyFieldChanged = false;
			foreach (PropertyInfo pi in editedObject.GetType().GetProperties())
			{
				if (pi == null) { throw new InvalidOperationException(); }
				object value = pi.GetValue(editedObject, null);
				if (value != null)
				{
					string val = pi.GetValue(editedObject, null).ToString();
					object[] attributes = pi.GetCustomAttributes(typeof(DbMemberAttribute), false);
					if (attributes != null && attributes.Length > 0)
					{
						DbMemberAttribute db = (DbMemberAttribute)attributes[0];
						if (db != null && db.KeyField)
						{
							if (val != pi.GetValue(sourceObject, null).ToString())
							{
								keyFieldChanged = true;
								break;
							}
						}
					}
				}
			}
			return keyFieldChanged;
		}

		/// <summary>
		/// CAPTCHA
		/// Returns an encrypted string for the given text
		/// </summary>
		/// <param name="content"></param>
		/// <param name="propertyId"></param>
		/// <returns></returns>
		public static string Encrypt(string content, string propertyId)
		{
			byte[] contentBytes = Encoding.ASCII.GetBytes(content);
			byte[] saltBytes = Encoding.ASCII.GetBytes(SALT + propertyId);
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(SALT, saltBytes);
			byte[] encryptedData = Encrypt(contentBytes, pdb.GetBytes(32), pdb.GetBytes(16));
			return UrlEncode(Convert.ToBase64String(encryptedData));
		}

		/// <summary>
		/// Returns an encrypted byte array
		/// </summary>
		/// <param name="clearData"></param>
		/// <param name="key"></param>
		/// <param name="IV"></param>
		/// <returns></returns>
		private static byte[] Encrypt(byte[] clearData, byte[] key, byte[] IV)
		{
			MemoryStream ms = new MemoryStream();
			Rijndael rij = Rijndael.Create();
			rij.Key = key;
			rij.IV = IV;
			CryptoStream cs = new CryptoStream(ms, rij.CreateEncryptor(), CryptoStreamMode.Write);
			cs.Write(clearData, 0, clearData.Length);
			cs.Close();
			byte[] encryptedData = ms.ToArray();
			return encryptedData;
		}

		/// <summary>
		/// Returns the decrypted byte array
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		/// <param name="IV"></param>
		/// <returns></returns>
		private static byte[] Decrypt(byte[] data, byte[] key, byte[] IV)
		{
			MemoryStream ms = new MemoryStream();
			Rijndael rij = Rijndael.Create();
			rij.Key = key;
			rij.IV = IV;
			CryptoStream cs = new CryptoStream(ms, rij.CreateDecryptor(), CryptoStreamMode.Write);
			cs.Write(data, 0, data.Length);
			cs.Close();
			byte[] decryptedData = ms.ToArray();
			return decryptedData;
		}

		/// <summary>
		/// CAPTCHA
		/// Decrypts an encrypted string. The password must be the same password as 
		/// used in the encryption
		/// </summary>
		/// <param name="content"></param>
		/// <param name="propertyId"></param>
		/// <returns></returns>
		public static string Decrypt(string content, string propertyId)
		{
			content = content.Replace(" ", "+");
			byte[] contentBytes = Convert.FromBase64String(content);
			byte[] saltBytes = Encoding.ASCII.GetBytes(SALT + propertyId);
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(SALT, saltBytes);
			byte[] decryptedData = Decrypt(contentBytes, pdb.GetBytes(32), pdb.GetBytes(16));
			return Encoding.ASCII.GetString(decryptedData);
		}

		/// <summary>
		/// Returns a randomly character string for the given length
		/// </summary>
		/// <returns></returns>
		public static string GenerateRandomString(int length)
		{
			string source = "23456789ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz";
			string randomString = string.Empty;
			Random r = new Random();
			for (int i = 0; i < length; i++)
			{
				int j = r.Next(0, source.Length - 1);
				randomString += source.Substring(j, 1);
			}
			return randomString;
		}

		/// <summary>
		/// CAPTCHA
		/// </summary>
		/// <param name="length"></param>
		/// <param name="propertyId"></param>
		/// <returns></returns>
		public static string GenerateRandomEncryptedString(int length, string propertyId)
		{
			string str = GenerateRandomString(length);
			while (IsBlockedWord(str))
			{
				str = GenerateRandomString(length);
			}
			return Encrypt(str.ToUpper(), propertyId);
		}

		private static bool IsBlockedWord(string str)
		{
			if(BLOCKEDWORDS.IndexOf(str) >= 0)
			{
				return true;
			}
			return false;
		}

		public static int ComputeMaximumConsecutiveMatchLength(string searchString, string mainString)
		{
			return ComputeMaximumConsecutiveMatchLength(searchString, mainString, false);
		}

		/// <summary>
		/// Compares the two strings letter by letter and returns the maximum number 
		/// of consecutive matches found.
		/// </summary>
		/// <param name="searchString"></param>
		/// <param name="mainString"></param>
		/// <param name="caseSensitive"></param>
		/// <returns></returns>
		public static int ComputeMaximumConsecutiveMatchLength(string searchString, string mainString, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				searchString = searchString.ToUpper();
				mainString = mainString.ToUpper();
			}
			int n = searchString.Length;
			int m = mainString.Length;
			int[,] d = new int[n, m];
			int maxMatchCount = 0;
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < m; j++)
				{
					bool matched = (mainString[j] == searchString[i]);

					if (matched && (i > 0 && j > 0) && (mainString[j - 1] == searchString[i - 1]))
					{
						//if it a consecutive match than increment the match count by 1
						d[i, j] = d[i - 1, j - 1] + 1; 
					}
					else
					{
						//if matched than 1 else 0
						d[i, j] = (matched) ? 1 : 0;
					}
					//Assign the maximum match count
					if (d[i, j] > maxMatchCount) { maxMatchCount = d[i, j]; }
				}
			}
			return maxMatchCount;
		}

        /// <summary>
        /// Stores the Regular Expression for the parsing of CSV's
        /// </summary>
        private static System.Text.RegularExpressions.Regex _CsvRegExStorage = null;
        /// <summary>
        /// Provides a reference to the compiled regular expression
        /// </summary>
        private static System.Text.RegularExpressions.Regex _CsvRegEx
        {
            get
            {
                if (_CsvRegExStorage == null)
                {
                    string CsvRegExString = "^((\"(?:[^\"]|\"\")*\"|[^,]*)(,(\"(?:[^\"]|\"\")*\"|[^,]*))*)$";
                    _CsvRegExStorage = new System.Text.RegularExpressions.Regex(CsvRegExString, System.Text.RegularExpressions.RegexOptions.Compiled & System.Text.RegularExpressions.RegexOptions.Singleline);
                }
                return _CsvRegExStorage;
            }
        }

        /// <summary>
        /// Provides a List of type string containing the elements of a line in the Comma Separated Format
        /// </summary>
        /// <remarks>
        /// Supports elements delimited by " and containing "" as a single quote.
        /// e.g. "Hello",1234.2,"He said:""Hi there""","There was a cat, in a hat, pretty fat"
        /// </remarks>
        /// <param name="csvLine">One line containing the CSV values</param>
        /// <returns>List of type string, one entry for each comma separated value</returns>
        public static List<string> CsvToStringList(string csvLine)
        {
            List<string> Ret = new List<string>();
            if (csvLine == null)
            {
                Ret.Add(null);
            }
            else
            {
                string Temp;
                System.Text.RegularExpressions.MatchCollection mc = _CsvRegEx.Matches(csvLine);
                while (mc != null && mc.Count > 0)
                {
                    System.Text.RegularExpressions.Match m = mc[0];
                    Temp = null;
                    // Group 2 will have the desired results
                    if (m.Groups.Count > 1)
                    {
                        Temp = m.Groups[2].Value;
                        if (Temp.Length > 2)
                        {
                            Temp = Temp.Replace("\"\"", "\"");
                            if (Temp.Length > 2)
                            {
                                if (Temp[0] == '"' && Temp[Temp.Length - 1] == '"')
                                {
                                    Temp = Temp.Substring(1, Temp.Length - 2);
                                }
                            }
                        }
                        Ret.Add(Temp);
                        if (csvLine.Length > m.Groups[2].Value.Length)
                        {
                            csvLine = csvLine.Substring(m.Groups[2].Value.Length + 1);
                        }
                        else
                        {
                            Temp = null;
                        }
                    }
                    if (Temp == null)
                        mc = null;
                    else
                        mc = _CsvRegEx.Matches(csvLine);
                }
            }
            return Ret;
        }

		/// <summary>
		/// Returns the Position Adjusted Mod34 check digit that must be appended to the passed in string
		/// </summary>
		/// <remarks>
		/// This is a slightly modified version of the standard Code39 barcode check-digit claculator.
		/// It utilises an extra counter {PosAdjuster} to enable the CheckDigit to catch transposed letters.
		/// </remarks>
		/// <returns></returns>
		public static string Mod43CheckDigitCalculate(string IdentifierToAddCheckDigitTo)
		{
			string CheckDigit = string.Empty;
			int Mod43Sum = 0;
			int PosAdjuster = 1;
			int CheckDigitPos = -1;
			const string Mod34CharTable = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%";

			if (IdentifierToAddCheckDigitTo == null)
			{
				throw new ArgumentNullException("IdentifierToAddCheckDigitTo", "The argument IdentifierToAddCheckDigitTo may not be null.");
			}
			if (IdentifierToAddCheckDigitTo.Length == 0)
			{
				throw new ArgumentException("The argument IdentifierToAddCheckDigitTo may not be blank.", "IdentifierToAddCheckDigitTo");
			}

			foreach (char c in IdentifierToAddCheckDigitTo)
			{
				int inx = Mod34CharTable.IndexOf(c);
				if (inx == -1)
				{
					throw new FormatException("The character {" + c + "} in the Identifier {" + IdentifierToAddCheckDigitTo + "} is not a valid Mod43 character.");
				}
				Mod43Sum += (inx * PosAdjuster);
				PosAdjuster++;
			}

			CheckDigitPos = Mod43Sum % 43;
			if (CheckDigitPos < 0)
			{
				throw new ArithmeticException("The Mod43Sum of the Identifier {" + IdentifierToAddCheckDigitTo + "} is {" + CheckDigitPos + "} [Too Small].");
			}
			else if (CheckDigitPos > 42)
			{
				throw new ArithmeticException("The Mod43Sum of the Identifier {" + IdentifierToAddCheckDigitTo + "} is {" + CheckDigitPos + "} [Too Large].");
			}
			CheckDigit = Mod34CharTable[CheckDigitPos].ToString();
			return CheckDigit;
		}

		public static List<string> Mod34RangeGenerate(string prefix, int numDigits, int start, int amount, string suffix, string validCheckDigits)
		{
			return Mod34RangeGenerate(prefix, numDigits, start, amount, suffix, validCheckDigits, -1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="numDigits"></param>
		/// <param name="start"></param>
		/// <param name="amount"></param>
		/// <param name="suffix"></param>
		/// <param name="validCheckDigits"></param>
		/// <param name="end">When the number generation hits this number, it will stop and return what has been made thus far (If anything)</param>
		/// <returns></returns>
		public static List<string> Mod34RangeGenerate(string prefix, int numDigits, int start, int amount, string suffix, string validCheckDigits, int end)
		{
			List<string> Generated = new List<string>();
			int Counter = start;
			string MaxNumbers = new string('9', numDigits);
			while (Generated.Count < amount)
			{
				if (end > -1 && Counter >= end) break;
				string Numbers = Counter.ToString(new string('0', numDigits));
				string CheckDigit = SERV.Utils.String.Mod43CheckDigitCalculate(prefix + Numbers + suffix);
				if (validCheckDigits != null && validCheckDigits.Length > 0)
				{
					if (validCheckDigits.IndexOf(CheckDigit) > -1)
					{
						Generated.Add(prefix + Numbers + suffix + CheckDigit);
					}
				}
				else
				{
					Generated.Add(prefix + Numbers + suffix + CheckDigit);
				}
				Counter++;
				if (Numbers == MaxNumbers) break;
			}
			return Generated;
		}

		/// <summary>
		/// HTML Encodes non "approved" HTML tags and only allows approved elements of those approved tags sent in from a Web Service.
		/// </summary>
		public static string MakeSafeHtmlTagsWebServiceOnly(string html)
		{
			html = System.Web.HttpUtility.HtmlDecode(html);
			return MakeSafeHtmlTags(html, 0);
		}

		/// <summary>
		/// HTML Encodes non "approved" HTML tags and only allows approved elements of those approved tags.
		/// </summary>
		public static string MakeSafeHtmlTags(string html)
		{
			return MakeSafeHtmlTags(html, 0);
		}

		/// <summary>
        /// Stores the Regular Expression for the removal of HTML tags from the start tag to the end tag
        /// </summary>
        private static System.Text.RegularExpressions.Regex _HtmlTagsRemoveFromStartTagToEndTagRegExStorage = null;
        /// <summary>
		/// Provides a reference to the compiled regular expression for HTML tags from the start tag to the end tag
        /// </summary>
        private static System.Text.RegularExpressions.Regex _HtmlTagsRemoveFromStartTagToEndTagRegEx
        {
            get
            {
                if (_HtmlTagsRemoveFromStartTagToEndTagRegExStorage == null)
                {
					string HtmlTagsRemoveFromStartTagToEndTagRegExString = @"<([a-z0-9\?:]*)\b[^>]*>(.*?)</\1(.*?)>";
					_HtmlTagsRemoveFromStartTagToEndTagRegExStorage = new System.Text.RegularExpressions.Regex(HtmlTagsRemoveFromStartTagToEndTagRegExString, System.Text.RegularExpressions.RegexOptions.Compiled & System.Text.RegularExpressions.RegexOptions.Multiline & System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                }
                return _HtmlTagsRemoveFromStartTagToEndTagRegExStorage;
            }
        }

		/// <summary>
		/// Stores the Regular Expression for the removal of start HTML tags
		/// </summary>
		private static System.Text.RegularExpressions.Regex _HtmlTagsRemoveStartTagsRegExStorage = null;
		/// <summary>
		/// Provides a reference to the compiled regular expression for removal of start HTML tags
		/// </summary>
		private static System.Text.RegularExpressions.Regex _HtmlTagsRemoveStartTagsRegEx
		{
			get
			{
				if (_HtmlTagsRemoveStartTagsRegExStorage == null)
				{
					string HtmlTagsRemoveStartTagsRegExString = @"<([a-z0-9\?:]*)\b[^>]*>";
					_HtmlTagsRemoveStartTagsRegExStorage = new System.Text.RegularExpressions.Regex(HtmlTagsRemoveStartTagsRegExString, System.Text.RegularExpressions.RegexOptions.Compiled & System.Text.RegularExpressions.RegexOptions.Multiline & System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				}
				return _HtmlTagsRemoveStartTagsRegExStorage;
			}
		}

		/// <summary>
		/// Stores the Regular Expression for the removal of end HTML tags
		/// </summary>
		private static System.Text.RegularExpressions.Regex _HtmlTagsRemoveEndTagsRegExStorage = null;
		/// <summary>
		/// Provides a reference to the compiled regular expression for removal of end HTML tags
		/// </summary>
		private static System.Text.RegularExpressions.Regex _HtmlTagsRemoveEndTagsRegEx
		{
			get
			{
				if (_HtmlTagsRemoveEndTagsRegExStorage == null)
				{
					string HtmlTagsRemoveEndTagsRegExString = @"</([a-z0-9\?:]*)\b[^>]*>";
					_HtmlTagsRemoveEndTagsRegExStorage = new System.Text.RegularExpressions.Regex(HtmlTagsRemoveEndTagsRegExString, System.Text.RegularExpressions.RegexOptions.Compiled & System.Text.RegularExpressions.RegexOptions.Multiline & System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				}
				return _HtmlTagsRemoveEndTagsRegExStorage;
			}
		}

		/// <summary>
		/// HTML Keeps "approved" HTML tags and completely removes non-approved elements and everything inside those removed elements of those approved tags.
		/// </summary>
		public static string MakeSafeHtmlTags(string html, int maxReturnedLength)
		{
			// Find the start and end tag, check if it's valid according to the list HtmlAcceptedTags
			// Then check the attributes of the found tags, and keep the valid attributes, drop the rest.
			// Then put the whole thing back together again.
			// NOTE: If the tag is not valid, all the code inside the tag is simply removed.

			// Null returns null, empty string returns empty string, no < returns html
			if (html == null) return null;
			if (html.Length == 0) return html;
			if (html.IndexOf('<') == -1) return MakeSafeHtmlTagsLengthCheck(html, maxReturnedLength);

			string htmlLower = html.ToLower();

			// Storage for the stuff to keep and to be replaced with
			List<string> TagsOriginalStart = new List<string>();
			List<string> TagsEnd = new List<string>();
			List<string> TagsToReplaceWithStart = new List<string>();
			List<string> TagsNoEndTagOriginal = new List<string>();
			List<string> TagsNoEndTagReplaceWith = new List<string>();

			// Step 1: Find Tags with Starting and Ending Tags, store the ones that we do want to keep, also, keep only the elements of these tags that we want
			foreach (HtmlAcceptedTagDetail htmlad in HtmlAcceptedTags)
			{
				string TagStart = "<" + htmlad.Tag.ToLower();
				string TagEnd = "</" + htmlad.Tag.ToLower() + ">";
				string TagToKeep = string.Empty;
				int TagStartBeginPos = htmlLower.IndexOf(TagStart);
				while(TagStartBeginPos > -1)
				{
					int TagStartEndPos = htmlLower.IndexOf(">", TagStartBeginPos);
					if (TagStartEndPos > -1)
					{
						int TagEndBeginPos = htmlLower.IndexOf(TagEnd, TagStartEndPos + 1);
						if (TagEndBeginPos > -1)
						{
							// Any elements inside this tag that we want to keep?
							string ElementsToKeep = HtmlTagGetApprovedElements(htmlad, html, htmlLower, TagStartBeginPos);
							if (ElementsToKeep.Length > 0)
							{
								TagToKeep = TagStart + " " + ElementsToKeep + ">";
							}
							else
							{
								TagToKeep = TagStart + ">";
							}
							string OriginalTag = html.Substring(TagStartBeginPos, TagStartEndPos - TagStartBeginPos + 1);
							TagsOriginalStart.Add(OriginalTag);
							TagsEnd.Add(TagEnd);
							TagsToReplaceWithStart.Add(TagToKeep);
						}
					}
					TagStartBeginPos = htmlLower.IndexOf(TagStart, TagStartBeginPos + 2);
				}
			}
			// Step 2: Replace the start and end tags that we want to keep with a special token
			for(int i = 0; i < TagsOriginalStart.Count; i ++)
			{
				html = html.Replace(TagsOriginalStart[i],	"!s.t,r`^!" + i + "!s.t,r`^!");
				html = html.Replace(TagsEnd[i],				"!e.n,d`^!" + i + "!e.n,d`^!");
			}

			// Only do the rest if any tags remain
			if (html.IndexOf('<') > -1)
			{
				htmlLower = html.ToLower();
				// Step 2: Look for start tags that have no matching end tags that are allowed
				foreach (HtmlAcceptedTagDetail htmlad in HtmlAcceptedTags)
				{
					if (htmlad.AllowOnlyStartTag == true)
					{
						string TagStart = "<" + htmlad.Tag.ToLower();
						string TagToKeep = string.Empty;
						int TagStartBeginPos = htmlLower.IndexOf(TagStart);
						while (TagStartBeginPos > -1)
						{
							int TagStartEndPos = htmlLower.IndexOf(">", TagStartBeginPos);
							if (TagStartEndPos > -1)
							{
								// Any elements inside this tag that we want to keep?
								string ElementsToKeep = HtmlTagGetApprovedElements(htmlad, html, htmlLower, TagStartBeginPos);
								if (ElementsToKeep.Length > 0)
								{
									TagToKeep = TagStart + " " + ElementsToKeep + ">";
								}
								else
								{
									TagToKeep = TagStart + ">";
								}
								string OriginalTag = html.Substring(TagStartBeginPos, TagStartEndPos - TagStartBeginPos + 1);
								TagsNoEndTagOriginal.Add(OriginalTag);
								TagsNoEndTagReplaceWith.Add(TagToKeep);
							}
							TagStartBeginPos = htmlLower.IndexOf(TagStart, TagStartBeginPos + 2);
						}
					}
				}

				// Step 3: Tokenise the Tags with no end that we're keeping
				for (int i = 0; i < TagsNoEndTagOriginal.Count; i++)
				{
					html = html.Replace(TagsNoEndTagOriginal[i], "!n.o,e`^!" + i + "!n.o,e`^!");
				}

				// Step 4: Remove all remaining tags with starting and ending tags (replacing whatever is inbetween the tags with nothing)
				html = _HtmlTagsRemoveFromStartTagToEndTagRegEx.Replace(html, string.Empty);

				// Step 5: Remove all remaining start tags
				html = _HtmlTagsRemoveStartTagsRegEx.Replace(html, string.Empty);

				// Step 6: Remove all remaining end tags
				html = _HtmlTagsRemoveEndTagsRegEx.Replace(html, string.Empty);

			}

			// Step 7: Replace the special start and end tokens with the "approved" tokens
			for (int i = 0; i < TagsOriginalStart.Count; i++)
			{
				html = html.Replace("!s.t,r`^!" + i + "!s.t,r`^!", TagsToReplaceWithStart[i]);
				html = html.Replace("!e.n,d`^!" + i + "!e.n,d`^!", TagsEnd[i]);
			}

			// Step 8: Replace the special start tokens with the "approved" tokens
			for (int i = 0; i < TagsNoEndTagReplaceWith.Count; i++)
			{
				html = html.Replace("!n.o,e`^!" + i + "!n.o,e`^!", TagsNoEndTagReplaceWith[i]);
			}

			// Done.
			return MakeSafeHtmlTagsLengthCheck(html, maxReturnedLength);
		}
		/// <summary>
		/// Truncate length of string to specified maxReturnedLength if maxReturnedLength > 0
		/// </summary>
		/// <param name="html"></param>
		/// <param name="maxReturnedLength"></param>
		/// <returns></returns>
		private static string MakeSafeHtmlTagsLengthCheck(string html, int maxReturnedLength)
		{
			if (maxReturnedLength > 0)
			{
				if (html != null && html.Length > maxReturnedLength)
				{
					html = html.Substring(0, maxReturnedLength);
				}
			}
			return html;
		}
		/// <summary>
		/// Grabs the approved elements of the tag and their contents, e.g. href="/accepted.asp" onclick="ignored" -> href="/accepted.asp"
		/// </summary>
		private static string HtmlTagGetApprovedElements(HtmlAcceptedTagDetail htmlad, string html, string htmlLower, int tagStartBeginPos)
		{
			string ElementsToKeep = string.Empty;
			if (htmlad.Elements != null && htmlad.Elements.Count > 0)
			{
				foreach (string Element in htmlad.Elements)
				{
					string ElementStart = Element + "=";
					int ElementStartStartPos = htmlLower.IndexOf(ElementStart, tagStartBeginPos);
					if (ElementStartStartPos > -1)
					{
						if (html.Length > ElementStartStartPos + 2)
						{
							string Delimiter = string.Empty;
							char del = html[ElementStartStartPos + ElementStart.Length];
							if (del == '"')
							{
								Delimiter = "\"";
							}
							else if (del == '"')
							{
								Delimiter = "'";
							}
							if (Delimiter != string.Empty)
							{
								int ElementStartEndPos = htmlLower.IndexOf(Delimiter, ElementStartStartPos + ElementStart.Length + 1);
								if (ElementStartEndPos > -1)
								{
									if (ElementsToKeep.Length > 0) ElementsToKeep += " ";
									ElementsToKeep += html.Substring(ElementStartStartPos, ElementStartEndPos - ElementStartStartPos + 1);
								}
							}
						}
					}
				}
			}
			return ElementsToKeep;
		}
		/// <summary>
		/// Storage struct for the Aceepted HTML tahs and Elements
		/// </summary>
		private class HtmlAcceptedTagDetail
		{
			public string Tag { get; set; }
			public List<string> Elements { get; set; }
			private bool _AllowOnlyStartTag = false;
			public bool AllowOnlyStartTag
			{
				get { return _AllowOnlyStartTag; }
				set { _AllowOnlyStartTag = value; }
			}
		}
		private static List<HtmlAcceptedTagDetail> _HtmlAcceptedTags = null;
		/// <summary>
		/// The list of Accepted HTML tags and Elements
		/// </summary>
		private static List<HtmlAcceptedTagDetail> HtmlAcceptedTags
		{
			get
			{
				if (_HtmlAcceptedTags == null)
				{
					_HtmlAcceptedTags = new List<HtmlAcceptedTagDetail>();
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "title", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "h1", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "h2", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "h3", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "h4", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "h5", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "h6", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "p", Elements = null, AllowOnlyStartTag = true });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "br", Elements = null, AllowOnlyStartTag = true });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "pre", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "em", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "strong", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "code", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "b", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "i", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "a", Elements = new List<string>() { "href", "name", "rel", "target" }});
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "img", Elements = new List<string>() { "src", "alt", "width", "height", "border", "align" }});
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "ul", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "li", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "ol", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "dl", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "dt", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "dd", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "table", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "th", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "tr", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "thead", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "tfoot", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "tbody", Elements = null });
					_HtmlAcceptedTags.Add(new HtmlAcceptedTagDetail() { Tag = "td", Elements = new List<string>() { "colspan", "align" } });
				}
				return _HtmlAcceptedTags;
			}
		}

		/// <summary>
		/// Prepares a string for use in a database column, returns 'NULL' if the string is null, otherwise, the string in safe quotes.
		/// </summary>
		public static string DBSafeStringOrNull(string value)
		{
			if (value == null)
			{
				return "NULL";
			}
			else
			{
				return "'" + MakeSafe(value) + "'";
			}
		}

		/// <summary>
		/// Prepares a string for use in a database column, returns '' if the string is null, otherwise, the string in safe quotes.
		/// </summary>
		public static string DBSafeString(string value)
		{
			if (value == null || value == string.Empty)
			{
				return "''";
			}
			else
			{
				return "'" + MakeSafe(value) + "'";
			}
		}

		/// <summary>
		/// Replaces all occurences of findText with replacementText in container
		/// </summary>
		/// <param name="container"></param>
		/// <param name="findText"></param>
		/// <param name="replacementText"></param>
		/// <returns></returns>
		public static string ReplaceAll(string container, string findText, string replacementText)
		{
			if (container == null) return null;
			string Original = container;
			while (true)
			{
				container = container.Replace(findText, replacementText);
				if (container == Original) break;
				Original = container;
			}
			return container;
		}

		/// <summary>
		/// Replaces all occurences of findText with replacementText in container
		/// </summary>
		/// <param name="container"></param>
		/// <param name="findText"></param>
		/// <param name="replacementText"></param>
		/// <returns></returns>
		public static string ReplaceAll(string container, char findChar, char replacementChar)
		{
			if (container == null) return null;
			string Original = container;
			while (true)
			{
				container = container.Replace(findChar, replacementChar);
				if (container == Original) break;
				Original = container;
			}
			return container;
		}

		/// <summary>
		/// Replaces the last instance of text, e.g. 1,2,3,4 to 1,2,3 and 4
		/// </summary>
		public static string ReplaceLast(string container, string findLastInstanceOf, string replaceWith)
		{
			if(container != null && findLastInstanceOf != null && container.Length > 0 && findLastInstanceOf.Length > 0)
			{
				if(replaceWith == null) replaceWith = string.Empty;
				int p = container.LastIndexOf(findLastInstanceOf);
				if(p > -1)
				{
					container =  container.Substring(0, p) + replaceWith + container.Substring(p + findLastInstanceOf.Length);
				}
			}
			return container;
		}

		/// <summary>
		/// Sets the first letter to UCase - THIS IS NOT A SENTENCE TOOL.
		/// </summary>
		public static string ToUpperFirstLetter(string container)
		{
			if (container != null && container.Length > 0)
			{
				container = container.Substring(0, 1).ToUpperInvariant() + container.Substring(1);
			}
			return container;
		}

		/// <summary>
		/// Stores the Regular Expression for the parsing of ContainsEmailAddress's
		/// </summary>
		private static System.Text.RegularExpressions.Regex _ContainsEmailAddressRegExStorage = null;
		/// <summary>
		/// Provides a reference to the compiled ContainsEmailAddress regular expression
		/// </summary>
		private static System.Text.RegularExpressions.Regex _ContainsEmailAddressRegEx
		{
			get
			{
				if (_ContainsEmailAddressRegExStorage == null)
				{
					string ContainsEmailAddressRegExString = "(([^<>()[\\]\\\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))";
					_ContainsEmailAddressRegExStorage = new System.Text.RegularExpressions.Regex(ContainsEmailAddressRegExString, System.Text.RegularExpressions.RegexOptions.Compiled & System.Text.RegularExpressions.RegexOptions.Singleline);
				}
				return _ContainsEmailAddressRegExStorage;
			}
		}

		/// <summary>
		/// Returns true if the text contains an email address
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static bool ContainsEmailAddress(string text)
		{
			if (text == null) return false;
			if (text.Length == 0) return false;
			System.Text.RegularExpressions.Match m = _ContainsEmailAddressRegEx.Match(text);
			return m.Success;
		}

		/// <summary>
		/// If the text contains an eamil address, it is removed and replaced with whatever replace is. If replace is null, a match will return "".
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string RemoveEmailAddresses(string text, string replace)
		{
			if (ContainsEmailAddress(text))
			{
				if (replace == null) return "";
				System.Text.RegularExpressions.MatchCollection mc = _ContainsEmailAddressRegEx.Matches(text);
				if (mc != null)
				{
					string TextToReturn = text;
					foreach (System.Text.RegularExpressions.Match match in mc)
					{
						TextToReturn = TextToReturn.Replace(match.Value, replace);
					}
					return TextToReturn;
				}
			}
			return text;
		}
	}
}
