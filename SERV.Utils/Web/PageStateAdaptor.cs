using System.Web.UI;

namespace SERV.Utils.Web
{
	public class PageStateAdapter : System.Web.UI.Adapters.PageAdapter
	{
		public override PageStatePersister GetStatePersister()
		{
			return new SessionPageStatePersister(this.Page);
		}
	}
}
