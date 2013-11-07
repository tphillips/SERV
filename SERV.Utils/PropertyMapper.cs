using System;
using System.Runtime.Serialization;
using System.Reflection;

namespace SERV.Utils
{
	public class PropertyMapper
	{
		public static void MapProperties(object from, object to, bool ignoreNullValues, bool ignore0Values)
		{
			if (from == null || to == null) { return; }
			Type tFrom = from.GetType();
			Type tTo = to.GetType();
			PropertyInfo[] toProps = tTo.GetProperties();
			foreach (PropertyInfo prop in toProps)
			{
				PropertyInfo srcProp = tFrom.GetProperty(prop.Name);
				if (srcProp != null)
				{
					try { 
						object val = srcProp.GetValue(from, null);
						if (val != null || !ignoreNullValues)
						{
							if ((val is int && ((int)val != 0 || !ignore0Values)) || !(val is int))
							{
								prop.SetValue(to, val, null); 
							}

						}
					} 
					catch {}
				}
			}
		}
	}
}

