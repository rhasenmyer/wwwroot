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

public partial class Public_Community_CommunityBoard : AppLayer.BasePage
{
	#region OnInit
	/// <summary>
	/// OnInit implementaion
	/// </summary>
	protected override void OnInit(EventArgs e)
	{
		ucCommunityThreadList.OnAdd += new CommandEventHandler(ucCommunityThreadList_OnAdd);
		ucCommunityThreadEdit.OnSave += new CommandEventHandler(ucCommunityThreadEdit_OnSave);
		ucCommunityThreadEdit.OnCancel += new EventHandler(ucCommunityThreadEdit_OnCancel);
		base.OnInit(e);
	}
	#endregion

	#region Page_Load
	/// <summary>
	/// Page_Load implementaion
	/// </summary>
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

		if (!IsPostBack)
		{
			ListInit();

            ucMemberStories.ControlInit(3);
		}
	}
	#endregion

	#region ListInit
	private void ListInit()
	{
		HideControls();
		ucCommunityThreadList.Visible = true;
		ucCommunityThreadList.ControlInit();
	}
	#endregion

	#region HideControls
	/// <summary>
	/// Hide controls
	/// </summary>
	private void HideControls()
	{
		ucCommunityThreadList.Visible = false;
		ucCommunityThreadEdit.Visible = false;
	}
	#endregion

	#region Handlers

	/// <summary>
	/// Record OnAdd event handler
	/// </summary>
	void ucCommunityThreadList_OnAdd(object sender, CommandEventArgs e)
	{
		HideControls();
		ucCommunityThreadEdit.Visible = true;
		ucCommunityThreadEdit.ControlInit(AppLayer.BaseEditControl.Mode.Add, 0);
	}	

	/// <summary>
	/// Record OnSave event handler
	/// </summary>
	void ucCommunityThreadEdit_OnSave(object sender, CommandEventArgs e)
	{
		ListInit();
		ucConfirmView.ShowEditMessage();

		if ((AppLayer.BaseEditControl.Mode)Enum.Parse(typeof(AppLayer.BaseEditControl.Mode), e.CommandName) == AppLayer.BaseEditControl.Mode.Add)
			ucConfirmView.ShowAddMessage();
		else
			ucConfirmView.ShowEditMessage();
	}

	/// <summary>
	/// Record OnCancel add\edit event handler
	/// </summary>
	private void ucCommunityThreadEdit_OnCancel(object sender, EventArgs e)
	{
		ListInit();
	}

	#endregion	
}
