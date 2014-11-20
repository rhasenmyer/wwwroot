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

public partial class Public_ThankYou : AppLayer.BasePage
{
	#region PageVars
	protected int NewMemberID
	{
		set { Session["NewMemberID"] = value; }
		get { return AppLayer.Functions.ToInt(Session["NewMemberID"]); }
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

		this.NewMemberID = 0;

        if(AppLayer.Functions.ToString(Request.QueryString["thtype"]) == "online")
        {
            this.phlPayment.Visible = false;
        }
        else if (AppLayer.Functions.ToString(Request.QueryString["thtype"]) == "onlinemobile")
        {
            this.phlPayment.Visible = true;
        }        
	}
	#endregion
}
