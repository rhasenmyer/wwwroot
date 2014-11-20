<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Xml" %>


<script language="C#" runat="server">
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        string hashstring = Request.QueryString["hash"];
        
        BSLayer.UserAccount.AuthenticationResult res = BSLayer.UserAccount.AccountByHash(hashstring);
        
        if (res.IsExists && res.Enable)
        {
            AppLayer.Authentication.SaveDetailsToCookie(false, res.UserAccountDetails.Email, "");

            AppLayer.USession.LogIn(res.UserAccountDetails);
            Application[Session.SessionID] = res.UserAccountDetails.ID.Value;
            if (Request.QueryString["page"] != null)
            {
                string URL = Request.QueryString["page"];
                URL = URL.Replace("!", "?").Replace("|", "&"); //use replacement characters to include querystrings
                Response.Redirect(URL);
            }
            else
            {
                AppLayer.Authorization.RedirectToHomepage();
            }
        }
        else if (res.IsExists && !res.Enable)
        {
            //cvLogin.ErrorMessage = "Account Disabled";
            //cvLogin.IsValid = false;
            Response.Write("Account Disabled");
            return;
        }
        else
        {
            //cvLogin.ErrorMessage = Resources.ErrorMessages.InvalidLogin;
            //cvLogin.IsValid = false;
            Response.Write("Invalid Login");
            return;
        }
    }
    
</script>





