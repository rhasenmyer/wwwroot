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

public partial class Public_Login : AppLayer.BasePage
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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
        }

        if (Request.QueryString["mode"] != null)
        {
            if (AppLayer.Functions.ToString(Request.QueryString["mode"]) == "logout")
            {
                AppLayer.USession.LogOut();
                Response.Redirect("/Default.aspx");
            }
        }
        if (!string.IsNullOrEmpty(Request["pas"]) && !string.IsNullOrEmpty(Request["email"]))
        {
            Login(Request["pas"], Request["email"], AppLayer.Functions.ToString(Request["landing"]));
        }
    }
    #endregion

    #region Login
    private void Login(string pas, string email, string landing)
    {
        BSLayer.UserAccount.AuthenticationResult res = BSLayer.UserAccount.Authenticate(email, pas);
        if (res.IsExists && res.Enable)
        {
            if (DBLayer.Group.GetFirstAssignedGroup(res.UserAccountDetails.ID.Value) == BSLayer.Group.SystemGroups.BaseUser)
            {
                if (!DBLayer.UserGroup.IsUserGroupsAlreadyExists(DBLayer.Group.GetAdminId(), res.UserAccountDetails.ID.Value))
                {
                    if (!new com.eoshealth.www.WWUserWS().ValidateUser(res.UserAccountDetails.Login.Value, pas))
                    {
                        return;
                    }
                }
            }
            AppLayer.USession.LogIn(res.UserAccountDetails);
            Application[Session.SessionID] = res.UserAccountDetails.ID.Value;
            if (landing.Length > 0 && landing == "ma")
            {
                Response.Redirect("~/User/MobileApplication.aspx");
                Response.End();
            }
            else
            {
                AppLayer.Authorization.RedirectToHomepage();
            }
        }
    }
    #endregion
}
