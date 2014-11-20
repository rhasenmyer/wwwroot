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

using System.Xml;
using System.Xml.Xsl;
using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Web.Mail;
using System.Text;
using System.IO;

public partial class Public_Community_ViewProfile : AppLayer.BasePage
{
	static string strUserID;
	

	#region PageVars
	public int UserID
	{
		set { ViewState["UserID"] = value; }
		get { return AppLayer.Functions.ToInt(ViewState["UserID"]); }
	}
	protected bool IsQuestions
	{
		set { ViewState["IsQuestions"] = value; }
		get { return AppLayer.Functions.ToBool(ViewState["IsQuestions"]); }
	}	
	protected bool IsPostings
	{
		set { ViewState["IsPostings"] = value; }
		get { return AppLayer.Functions.ToBool(ViewState["IsPostings"]); }
	}	
	protected bool IsBoards
	{
		set { ViewState["IsBoards"] = value; }
		get { return AppLayer.Functions.ToBool(ViewState["IsBoards"]); }
	}	
	protected bool IsPhoto
	{
		set { ViewState["IsPhoto"] = value; }
		get { return AppLayer.Functions.ToBool(ViewState["IsPhoto"]); }
	}	
	protected bool IsVideo
	{
		set { ViewState["IsVideo"] = value; }
		get { return AppLayer.Functions.ToBool(ViewState["IsVideo"]); }
	}
	#endregion

	#region OnInit
	protected override void OnInit(EventArgs e)
	{
		this.lbtnQuestions.Click += new EventHandler(lbtnTab_Click);
		this.lbtnPostings.Click += new EventHandler(lbtnTab_Click);
		this.lbtnBoards.Click += new EventHandler(lbtnTab_Click);
		this.lbtnPhotos.Click += new EventHandler(lbtnTab_Click);
		this.lbtnVideos.Click += new EventHandler(lbtnTab_Click);
		base.OnInit(e);

        ucUserFriends.ViewAll += new Controls_User_UserFriends.ViewAllEventHandler(ucUserFriends_ViewAll);
	}
	void lbtnTab_Click(object sender, EventArgs e)
	{
		ShowTab((LinkButton)sender);
	}	
	#endregion

	private void showMemberProfile(int intUserID)
	{

		string strProfileFirstName = getDBValueEOS("SELECT FirstName FROM Eos..UserAccount WHERE ID = " + intUserID);

		string strProfileLastName = getDBValueEOS("SELECT LastName FROM Eos..UserAccount WHERE ID = " + intUserID);

		lblProfileFirstName.Text = strProfileFirstName;

		lblProfileLastName.Text = strProfileLastName;
	}

	#region Page_Load
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!AppLayer.USession.IsMemberLogged)
        	{
            		Session["LastRequest"] = HttpContext.Current.Request.Url.ToString();
                Response.Redirect("/Public/Login.aspx", true);
        	}

		strUserID = Request["user_id"];
		
		showMemberProfile(Convert.ToInt32(strUserID));

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
            if (AppLayer.USession.IsMemberLogged)
            {
                phlRegisterFromPhotos.Visible = false;
            }
            else
            {
                phlRegisterFromPhotos.Visible = true;
            }

            this.UserID = AppLayer.Functions.ToInt(Request["user_id"]);
            ucUserInfo.ControlInit(this.UserID, true);
            ShowTab(lbtnQuestions);

            ucUserFriends.Visible = false;
            ucJoinAsACommunityMember.Visible = false;
            if (!AppLayer.USession.IsMemberLogged)
            {
                ucJoinAsACommunityMember.Visible = true;
            }
            else
            {
                ucUserFriends.Refresh(this.UserID,4);
                ucUserFriends.Visible = true;
            }

			this.phlPhotoList.Visible = (DBLayer.UserPhoto.GetCount(this.UserID) > 0);
			this.phlPhotoNoRecords.Visible = !this.phlPhotoList.Visible;

            if(Request["search_name"]!=null&&
               Request["search_location"]!=null&&
               Request["search_agefrom"]!=null&&
               Request["search_ageto"]!=null)
            {
                hlnkBackToSearch.NavigateUrl = "Members.aspx?search_name=" + Request["search_name"] + 
                    "&search_location=" + Request["search_location"]+
                    "&search_agefrom=" + Request["search_agefrom"]+
                    "&search_ageto=" + Request["search_ageto"];
                hlnkBackToSearch.Visible = true;
            }
            else if(Request["search_community"]!=null)
            {
                hlnkBackToSearch.NavigateUrl = "CommunityBoard.aspx?search_community=" + Request["search_community"];
                hlnkBackToSearch.Visible = true;
            }
		}
	}
	#endregion

	#region ShowTab
	private void ShowTab(LinkButton lbtn)
	{
		phlQuestions.Visible = false;
		phlPostings.Visible = false;
		phlBoards.Visible = false;
		phlPhoto.Visible = false;
		phlVideo.Visible = false;

		this.IsQuestions = false;
		this.IsPostings = false;
		this.IsBoards = false;
		this.IsPhoto = false;
		this.IsVideo = false;

		switch (lbtn.ID)
		{			
			case "lbtnQuestions":
				phlQuestions.Visible = true;
				this.IsQuestions = true;
				this.ucQuestions.ControlInit(this.UserID);
				break;

			case "lbtnBoards":
				phlBoards.Visible = true;
				this.IsBoards = true;
				ucBoards.ControlInit(this.UserID);
				break;

			case "lbtnPostings":
				phlPostings.Visible = true;
				this.IsPostings = true;
				this.ucPostings.ControlInit(this.UserID);
				break;

			case "lbtnPhotos":
				phlPhoto.Visible = true;
				this.IsPhoto = true;
				this.phlPhotoList.Visible = (DBLayer.UserPhoto.GetCount(this.UserID) > 0);
				this.phlPhotoNoRecords.Visible = !this.phlPhotoList.Visible;
				break;

			case "lbtnVideos":
				phlVideo.Visible = true;
				this.IsVideo = true;
				this.ucVideos.ControlInit(this.UserID);
				break;
		}
	}
	#endregion 

    #region ucUserFriends events
    protected void ucUserFriends_ViewAll(object sender, Controls_User_UserFriends.ViewAllEventArgs e)
    {
        ucUserFriendsOverView.Refresh(e.UserID);
        phlFriendTab.Visible = true;
        phlMainTab.Visible = false;
    }
    #endregion

    #region lbtnBack events
    protected void lbtnBack_Click(object sender, EventArgs e)
    {
        phlFriendTab.Visible = false;
        phlMainTab.Visible = true;
    }
    #endregion

    //function to execute a SQL statement and return a value from Eos Database
    protected string getDBValueEOS(string Sql)
    {
        	string ret = "";
        
        	string connString = ConfigurationManager.ConnectionStrings["WW"].ConnectionString;
        	SqlConnection conn = new SqlConnection(connString);
        	SqlCommand sqlCmd;

        	// Create a new command object
        	sqlCmd = new SqlCommand();
        	// Specify the command to be excecuted
        	sqlCmd.CommandType = CommandType.Text;

        	// execute the dynamic sql
        	sqlCmd.CommandText = Sql;

        	sqlCmd.Connection = conn;
        	
		try
      		{
        		conn.Open();
        		SqlDataReader myReader = sqlCmd.ExecuteReader();
        		while (myReader.Read())
        		{
            		ret = myReader[0].ToString();
        		}

			myReader.Close();
			return ret;
		}
		finally
		{
        		
        		// close the connection
        		conn.Close();
			conn.Dispose();
		}
    }


    //function to execute a SQL statement and return a value from DBWireless Database
    protected string getDBValueDBWIRELESS(string Sql)
    {
        	string ret = "";
        
        	string connString = ConfigurationManager.ConnectionStrings["WW2"].ConnectionString;
        	SqlConnection conn = new SqlConnection(connString);
        	SqlCommand sqlCmd;

        	// Create a new command object
        	sqlCmd = new SqlCommand();
        	// Specify the command to be excecuted
        	sqlCmd.CommandType = CommandType.Text;

        	// execute the dynamic sql
        	sqlCmd.CommandText = Sql;

		
        	sqlCmd.Connection = conn;

		try
      		{
        		conn.Open();
        		SqlDataReader myReader = sqlCmd.ExecuteReader();
        		while (myReader.Read())
        		{
            			ret = myReader[0].ToString();
        		}
			myReader.Close();
        		return ret;
		}
		finally
		{
        		// close the connection
        		conn.Close();
			conn.Dispose();
		}
    }
}
