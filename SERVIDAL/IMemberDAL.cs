using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIDAL
{
	public interface IMemberDAL
	{

		Member Get(int newclassID);
		int Create(Member newclass);
		int Save(Member newclass);
		void Update(Member newclass);
		List<Member> List(string search);
		List<Tag> ListMemberTags(int memberId);

	}
}
