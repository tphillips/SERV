using System;
using System.Runtime.Serialization;
using System.Reflection;

namespace SERVDataContract
{
	public class UpdatePolicyAttribute : Attribute
	{

		public UserLevel MinRequiredLevel { get; set; }
		public bool AllowOwner { get; set; }
		public bool ReadOnly { get; set; }

		public UpdatePolicyAttribute()
		{
			ReadOnly = false;
			AllowOwner = true;
			MinRequiredLevel = UserLevel.Admin;
		}

		public static void MapPropertiesWithUpdatePolicy(object from, object to, User user, bool isOwner)
		{
			if (from == null || to == null)
			{
				return;
			}
			Type tFrom = from.GetType();
			Type tTo = to.GetType();
			PropertyInfo[] toProps = tTo.GetProperties();
			foreach (PropertyInfo prop in toProps)
			{
				PropertyInfo srcProp = tFrom.GetProperty(prop.Name);
				{
					if (srcProp != null)
					{
						object[] policies = srcProp.GetCustomAttributes(false);
						UpdatePolicyAttribute policy = null;
						foreach(object p in policies)
						{
							if (p is UpdatePolicyAttribute) { policy = (UpdatePolicyAttribute) p; } 
						}
						if (policy != null && !policy.ReadOnly)
						{
							if (user.UserLevel >= (int)policy.MinRequiredLevel || (isOwner && policy.AllowOwner))
							{
								try
								{ 
									object val = srcProp.GetValue(from, null);
									prop.SetValue(to, val, null); 
								}
								catch (Exception ex)
								{
									Console.WriteLine(ex.Message);
								}
							}
						}
					}
				}
			}
		}
	}
}

