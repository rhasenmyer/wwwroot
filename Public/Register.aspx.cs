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

public partial class Public_Register : AppLayer.BasePage
{
	#region PageVars
	protected int NewMemberID
	{
		set { Session["NewMemberID"] = value; }
		get { return AppLayer.Functions.ToInt(Session["NewMemberID"]); }
	}
	#endregion

	#region OnInit
	protected override void OnInit(EventArgs e)
	{
		this.ucProgram.OnBack += new EventHandler(ucProgram_OnBack);
		this.ucProgram.OnChoose += new CommandEventHandler(ucProgram_OnChoose);
		this.ucRegister.OnSavedSuccessful += new CommandEventHandler(ucRegister_OnSavedSuccessful);
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

		if (!Page.IsPostBack)
		{
			string Step = AppLayer.Functions.ToString(Request["step"]);
			if (Step == "program")
			{
				this.ShowControl(ucProgram);
			}
			else
			{				
				if (this.NewMemberID == 0)
				{
					ucRegister.ControlInit(AppLayer.BaseEditControl.Mode.Add, 0);
				}
				else
				{
					ucRegister.ControlInit(AppLayer.BaseEditControl.Mode.Edit, this.NewMemberID);
				}
				ShowControl(ucRegister);
			}

            ucFAQShortTable.Refresh(4);
		}
	}
	#endregion

	#region ucRegister_OnSavedSuccessful
	void ucRegister_OnSavedSuccessful(object sender, CommandEventArgs e)
	{
		this.NewMemberID = AppLayer.Functions.ToInt(e.CommandArgument);
		ucRegister.ControlInit(AppLayer.BaseEditControl.Mode.Edit, this.NewMemberID);
		Response.Redirect("/Public/Register.aspx?step=program", true);
	}
	#endregion

	#region ucProgram_OnChoose
	void ucProgram_OnChoose(object sender, CommandEventArgs e)
	{
		int ServicePlan = AppLayer.Functions.ToInt(e.CommandArgument);		
		if (ServicePlan == (int)BSLayer.UserAccount.ServicePlan.Custom)
		{			
			DBLayer.UserAccount.Details UDetails = DBLayer.UserAccount.GetDetails(this.NewMemberID);
            UDetails.ServicePlan = (int)BSLayer.UserAccount.ServicePlan.Custom;
			DBLayer.UserAccount.Update(this.NewMemberID, UDetails);
			
            //...
            //Response.Redirect("/Public/ThankYou.aspx", true);
		}
		else
		{
            DBLayer.UserAccount.Details UDetails = DBLayer.UserAccount.GetDetails(this.NewMemberID);
            UDetails.ServicePlan = (int)BSLayer.UserAccount.ServicePlan.Preset;
            DBLayer.UserAccount.Update(this.NewMemberID, UDetails);

            //... 
            //Response.Redirect("/Public/ThankYou.aspx", true);
		}
	}
	#endregion

	#region ucProgram_OnBack
	void ucProgram_OnBack(object sender, EventArgs e)
	{
		Response.Redirect("/Public/Register.aspx");
	}	
	#endregion

	#region ShowControl
	private void ShowControl(UserControl ucControlToShow)
	{
		this.ucProgram.Visible = false;
		this.ucRegister.Visible = false;
		ucControlToShow.Visible = true;
	}
	#endregion
}
