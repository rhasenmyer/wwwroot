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

public partial class ContactUs : AppLayer.BasePage
{
	
	#region OnInit
	protected override void OnInit(EventArgs e)
	{
		this.ucContactUs.OnSave += new CommandEventHandler(ucContactUs_OnSave);
		Page.Load +=new EventHandler(Page_Load);
		base.OnInit(e);
	}
	#endregion
	

	#region Page_Load
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
			if (Request.QueryString["thank"] != null)
			{
				this.phlForm.Visible = false;
				this.phlThankTou.Visible = true;
			}
			else
			{
				this.ucContactUs.ControlInit(AppLayer.BaseEditControl.Mode.Add, 0);
				this.phlForm.Visible = true;
				this.phlThankTou.Visible = false;

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
			}
		}
	}
	#endregion

	
	#region ucContactUs_OnSave
	void ucContactUs_OnSave(object sender, CommandEventArgs e)
	{
		Response.Redirect("/Public/ContactUs.aspx?thank=you", true);
	}
	#endregion 
	
}
