using System.Runtime.Serialization;

namespace SERVDataContract
{
	[DataContract]
	public class Tag
    {

		public Tag(){}

		public Tag(SERVDataContract.DbLinq.Tag metal)
		{
			this.TagID = metal.TagID;
			this.TagName = metal.Tag1;
		}

		[DataMember]
		public int TagID { get; set; }
		[DataMember]
		public string TagName { get; set; }
    }
}

