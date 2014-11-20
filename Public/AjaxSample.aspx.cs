using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Public_AjaxSample : AppLayer.BasePage
{
	#region OnInit
	protected override void OnInit(EventArgs e)
	{
		rptPages.ItemDataBound +=new RepeaterItemEventHandler(rptPages_ItemDataBound);
		Page.Load +=new EventHandler(Page_Load);
		base.OnInit(e);
	}
	#endregion

	#region Page_Load
	protected void Page_Load(object sender, EventArgs e)
	{
		DBLayer.PageContent.Details CMS = BSLayer.PageContent.GetPageContent(
			DBLayer.Page.GetPageIDByCode(Request.Url.AbsolutePath),
			BSLayer.Language.GetMembersLanguageId());

		base.MetaDataDetails = new BSLayer.MetaData();

		if (CMS != null)
		{
			this.ltTitle.Text = CMS.PageTitle.Value;
			this.ltContent.Text = CMS.Content.Value;

			this.MetaDataDetails = new BSLayer.MetaData();
			this.MetaDataDetails.Description = CMS.MetaDescription.Value;
			this.MetaDataDetails.Keywords = CMS.MetaKeywords.Value;
			this.MetaDataDetails.Title = CMS.PageTitle.Value;
		}
		
		this.rptPages.DataSource = DBLayer.Page.GetListParentPagesPublic();
		this.rptPages.DataBind();
	}
	#endregion

	#region rptPages_ItemDataBound
	void rptPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.DataItem != null)
		{
			Literal ltMenuItem = (Literal)e.Item.FindControl("ltMenuItem");			
			DataRow DrRow = ((System.Data.DataRowView)e.Item.DataItem).Row;

			int PageId = AppLayer.Functions.ToInt(DrRow[DBLayer.Page.ColumnName.ID]);

			if (PageId > 0)
			{
				int PageContentId = DBLayer.PageContent.GetPageContentID(PageId, BSLayer.Language.GetMembersLanguageId());
				if (PageContentId == 0)
				{
					PageContentId = DBLayer.PageContent.GetPageContentID(PageId, BSLayer.Language.GetDefaultLanguageId());
				}
				
				if (PageContentId > 0)
				{
					ltMenuItem.Text = DBLayer.PageContent.GetDetails(PageContentId).MenuItem;
				}				
			}
		}
	}
	#endregion
}
