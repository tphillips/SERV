using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IMemberBLL
	{
        Member Get(int newclassID);
        int Create(Member newclass);
        List<Member> List(string search);
	}
}
