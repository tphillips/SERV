using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SERV.Utils.Web
{
	public class UI
	{
		/// <summary>
		/// Add numbers specified to each dropdown in the list of dropdowns specified
		/// </summary>
		/// <param name="dds"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="firstItem"></param>
		public static void NumberFillDropDowns(IEnumerable<DropDownList> dds, int start, int end, string firstItem)
		{
			foreach (DropDownList l in dds)
			{
				//string current = l.Text;
				if (firstItem != null) { l.Items.Add(firstItem); }
				if (end > start)
				{
					for (int x = start; x <= end; x++)
					{
						l.Items.Add(new ListItem(x.ToString(), x.ToString()));
					}
				}
				else
				{
					for (int x = start; x >= end; x--)
					{
						l.Items.Add(new ListItem(x.ToString(), x.ToString()));
					}
				}
				//if (!System.String.IsNullOrEmpty(current))
				//{
				//    l.Text = current;
				//}
			}
		}

		/// <summary>
		/// Fills a list of dropdowns with a list of items
		/// </summary>
		/// <param name="dds"></param>
		/// <param name="items"></param>
		public static void FillDropDowns(IEnumerable<DropDownList> dds, IEnumerable<string> items)
		{
			foreach (DropDownList l in dds)
			{
				foreach (string s in items)
				{
					l.Items.Add(new ListItem(s, s));
				}
			}
		}

		/// <summary>
		/// Fills a list of dropdowns with a list of items
		/// </summary>
		/// <param name="dds"></param>
		/// <param name="en"></param>
		public static void FillDropDowns<T>(IEnumerable<DropDownList> dds)
		{
			List<string> items = new List<string>(Enum.GetNames(typeof(T)));
			List<string> values = new List<string>();
			foreach(int i in Enum.GetValues(typeof(T)))
			{
				values.Add(((char)i).ToString());
			}
			foreach (DropDownList l in dds)
			{
				int i = 0;
				foreach (string s in items)
				{
					l.Items.Add(new ListItem(s, values[i]));
					i++;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <param name="list"></param>
		public static void FillCurrencyDropDown<T1>(DropDownList list)
		{
			FillDropDowns<T1>(new List<DropDownList>() {list});
			//Move GBP and EUR to top
			ListItem lst = list.Items.FindByText("GBP");
			list.Items.Remove(lst);
			list.Items.Insert(0,lst);

			lst = list.Items.FindByText("EUR");
			list.Items.Remove(lst);
			list.Items.Insert(1, lst);
		}
	}
}
