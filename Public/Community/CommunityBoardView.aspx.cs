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

public partial class Public_Community_CommunityBoardView : AppLayer.BasePage
{
	#region PageVars
	public int ThreadID
	{
		set { ViewState["ThreadID"] = value; }
		get { return AppLayer.Functions.ToInt(ViewState["ThreadID"]); }
	}
	#endregion

	#region OnInit
	/// <summary>
	/// OnInit implementaion
	/// </summary>
	protected override void OnInit(EventArgs e)
	{
		ucCommunityPostList.OnAdd += new CommandEventHandler(ucCommunityPostList_OnAdd);
		ucCommunityPostEdit.OnSave += new CommandEventHandler(ucCommunityPostEdit_OnSave);
		ucCommunityPostEdit.OnCancel += new EventHandler(ucCommunityPostEdit_OnCancel);
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
			this.ThreadID = AppLayer.Functions.ToInt(Request["thread_id"]);
			DBLayer.CommunityThread.Details CTDetails = DBLayer.CommunityThread.GetDetails(this.ThreadID);
			if ((CTDetails == null) || (CTDetails.ID.IsNull))
			{
				Response.Redirect("/Public/Community/CommunityBoard.aspx", true);
			}
			ListInit();
		}
	}
	#endregion

	#region ListInit
	private void ListInit()
	{
		HideControls();
		ucCommunityPostList.Visible = true;
		ucCommunityPostList.ThreadID = this.ThreadID;
		ucCommunityPostList.ControlInit();
	}
	#endregion

	#region HideControls
	/// <summary>
	/// Hide controls
	/// </summary>
	private void HideControls()
	{
		ucCommunityPostList.Visible = false;
		ucCommunityPostEdit.Visible = false;
	}
	#endregion

	#region Handlers

	/// <summary>
	/// Record OnAdd event handler
	/// </summary>
	void ucCommunityPostList_OnAdd(object sender, CommandEventArgs e)
	{
		HideControls();
		ucCommunityPostEdit.Visible = true;
		ucCommunityPostEdit.ThreadID = this.ThreadID;
		ucCommunityPostEdit.ControlInit(AppLayer.BaseEditControl.Mode.Add, 0);
	}

	/// <summary>
	/// Record OnSave event handler
	/// </summary>
	void ucCommunityPostEdit_OnSave(object sender, CommandEventArgs e)
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
	private void ucCommunityPostEdit_OnCancel(object sender, EventArgs e)
	{
		ListInit();
	}

	#endregion
}
