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

public partial class Public_ProgramComponents : AppLayer.BasePage
{
	#region PageVars
    protected int Index = 0;
	protected int PageContentID
	{
		set { ViewState["PageContentID"] = value; }
		get { return AppLayer.Functions.ToInt(ViewState["PageContentID"]); }
	}
	#endregion

	#region OnInit
	protected override void OnInit(EventArgs e)
	{
		this.rptProgramComponents.ItemDataBound += new RepeaterItemEventHandler(rptProgramComponents_ItemDataBound);
		base.OnInit(e);
	}

	void rptProgramComponents_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		Literal ltImage = (Literal)e.Item.FindControl("ltImage");
		if (ltImage != null)
		{
			DataRowView DrRow = (DataRowView)e.Item.DataItem;
			int id = AppLayer.Functions.ToInt(DrRow[DBLayer.ProgramComponent.ColumnName.ID]);
			if (id > 0)
			{
                string ImageUrl = BSLayer.ContentItem.GetItemImageUrl(BSLayer.ContentItem.ItemType.ProgramComponent, id);

                if (System.IO.File.Exists(Server.MapPath(ImageUrl)))
                {
                    ltImage.Text = "<img src=\"" + ImageUrl + "\" border=\"0\" />";
                }
                else
                {
                    ltImage.Text = "<img src=\"/images/no_image/no_image_85x62.gif\" border=\"0\" alt=\"Image\" />";
                }
				
			}
		}
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
			this.PageContentID = CMS.ID.Value;
			this.ltTitle.Text = CMS.PageTitle.Value;
            this.ltContent.Text = System.Text.RegularExpressions.Regex.Replace(CMS.Content.Value, "<FONT(.*?)>", "").Replace("</FONT>", "");
			this.MetaDataDetails = new BSLayer.MetaData();
			this.MetaDataDetails.Description = CMS.MetaDescription.Value;
			this.MetaDataDetails.Keywords = CMS.MetaKeywords.Value;
			this.MetaDataDetails.Title = CMS.PageTitle.Value;
		}

		if (!Page.IsPostBack)
		{
			DataTable DtProgramComponents = DBLayer.ProgramComponent.GetList();
			this.rptProgramComponents.DataSource = DtProgramComponents;
			this.rptProgramComponents.DataBind();
		}
	}
	#endregion
}
