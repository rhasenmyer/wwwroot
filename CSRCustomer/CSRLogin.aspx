<%@ Page Language="C#"%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ OutputCache Duration="1" Location="None"%>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Xml.Xsl" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System.Web.Configuration" %>

<%@ Import Namespace="Microsoft.Reporting.WebForms" %>
<%@ Import Namespace="System.Security.Principal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
        <script language="C#" runat="server">

    
    protected void Page_Load(object sender, EventArgs e)
    {
            Response.CacheControl = "Public";
            
            if (Membership.GetUser() == null)
            {
                HiddenField user = (HiddenField)this.loginView.FindControl("txtUserName2");
                user.Value = "WESTPARKAGENT";
            }

    }
            
    protected void Login(string username)
    {
        
        bool isPersistent = false;
        string userData = "ApplicationSpecific data for this user.";

        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
          username,
          DateTime.Now,
          DateTime.Now.AddMinutes(30),
          isPersistent,
          userData,
          FormsAuthentication.FormsCookiePath);


            
        // Encrypt the ticket.
        string encTicket = FormsAuthentication.Encrypt(ticket);

        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        //authCookie.Secure = FormsAuthentication.RequireSSL;

        // Create the cookie.
        //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        Response.Cookies.Add(authCookie);
        
        //Response.Redirect(Request.Url.ToString());
        //Response.Redirect("CSRPortal.aspx");
	Response.Redirect("CSRPortal_CustomerQuery.aspx");
        
    }
    

    
    public void logout(Object sender, EventArgs e)
    {
        //Response.Write("logout");
        FormsAuthentication.SignOut();

        Response.Redirect(Request.Url.ToString());
    }

    public void Login_Click2(Object sender, EventArgs e)
    {
        // Create a custom FormsAuthenticationTicket containing
        // application specific data for the user.
        HiddenField user = (HiddenField)this.loginView.FindControl("txtUserName2");
        string username = user.Value;
        TextBox pwd = (TextBox)this.loginView.FindControl("txtUserPwd2");
        string password = pwd.Text;

        if (Membership.ValidateUser(username, password))
        {
            Login(username);
            LoginMsg.Text = "Login succeeded.";
        }
        else
        {
	    //Login(username);
            LoginMsg.Text = "Login failed. Please check your password and try again.";
        }
    }


    
            


        </script>    
<html>
<head id="Head1" runat="server">
<title>CSR Portal</title>

<style type="text/css">


body {
	font-family: Helvetica, Verdana, Lucida Grande, Arial, sans-serif;
	font-size: 12px;
}

.border1
{			
	BORDER-TOP: #c2cde3 1px solid;
	BORDER-RIGHT: #c2cde3 1px solid;
	BORDER-LEFT: #c2cde3 1px solid;
	BORDER-BOTTOM: #c2cde3 1px solid;
}	

.msg
{
	font-family: Lucida Grande, Arial, Helvetica, sans-serif;
	font-size: 12px;
}
h3
{
	font-weight: bold;
	color: #01485b;
}
table
{
	font-family: Lucida Grande, Arial, Helvetica, sans-serif;
	font-size: 12px;
}
.menu1a
{
	/*BORDER-BOTTOM: #ffffff 1px solid; BACKGROUND-COLOR: #000066; TEXT-ALIGN: center;*/
	height: 32px;
	width:982px;
	color: green;
	font-family: helvetica;
	font-weight:bold;
	text-align:center;
	text-transform: uppercase;
	xfont-size:16px;
}
A.Menu1aLink:link {
	FONT-SIZE: 10pt;
	PADDING-RIGHT: 0px;
	PADDING-LEFT: 0px;
	COLOR: #ffffff;
	font-weight: bold;
	TEXT-DECORATION: none
}
A.Menu1aLink:visited {
	FONT-SIZE: 10pt;
	PADDING-RIGHT: 0px;
	PADDING-LEFT: 0px;
	COLOR: #ffffff;
	font-weight: bold;
	TEXT-DECORATION: none
}
A.Menu1aLink:active {
	FONT-SIZE: 10pt;
	PADDING-RIGHT: 0px;
	PADDING-LEFT: 0px;
	COLOR: #ffffff;
	font-weight: bold;
	TEXT-DECORATION: none
}
A.Menu1aLink:hover {
	FONT-SIZE: 10pt;
	PADDING-RIGHT: 0px;
	PADDING-LEFT: 0px;
	COLOR: #FFE300;
	font-weight: bold;
	TEXT-DECORATION: none
}
        
/* Profile Bar */
        
#profile-bar
{
	xheight: 18px;
	margin:0 10px 0 20px;
	padding: 8px 0 5px 20px;
	xfont-family: Arial, Helvetica, sans-serif;
	xbackground: #FFFFFF;
	font-size: 11px;
}
#profile-bar label{color:#666666;}
#profile-bar a{color:#666666;}
#profile-bar A:hover
{
	COLOR: #FFE300; TEXT-DECORATION: none;
}
#profile-bar h5{font-size: 14px;margin: 0;}
#profile-bar ul{margin: 0; float: right; margin-right: 10px; }
#profile-bar li{display:inline; list-style-type: none;padding: 0 15px 0 15px; border-right: 1px solid #999;font-size: 10px;text-transform:uppercase;}
#profile-bar li.last{padding: 0 5px 0 15px; border-right: none;}
#profile-bar li.last { border: 0; }
#profile-bar h5 a{font-weight: bold; }
#profile-bar ul, #profile-bar li{display:inline;text-align:right;}
#profile-bar input.text
{
	font-size: 9px;
        border:1px solid #c3c3c3;
        border-top:1px solid #7c7c7c;
        border-bottom:1px solid #dddddd;
        width: 80px;
        line-height: 10px;
}
#display-username{float:left;font-weight: bold;}
#display-username a{font-weight: bold;}
#profile-bar #profile-signin{float:left;}
#profile-bar #profile-signin h6{display: inline;text-transform: uppercase;font-size: 11px;font-weight: bold; color: #666666;}
#profile-bar #profile-signin label{text-transform: uppercase;margin: 0 3px 0 15px;font-size: 10px; font-family: Arial, Helvetica, sans-serif;}
</style>

<script id="dynamicScript" src=""></script>

</head>

<body>
    

<form id="form1" runat="server">

<table width="855" border="0" align="center" cellpadding="0" cellspacing="0" ID="Table1">
<tr>
	<td bgcolor="#FFFFFF" align="left"><a href="CSRPortal.aspx"><img src="images/logo_new_EosHealth.jpg" width="450" height="85" border="0" alt="EosHealth"></a></td>
	<td>
	
	<table height="22" border="0" align="center" cellpadding="0" cellspacing="0" ID="Table2">
	<tr>
		<td class="menu1a">

		<div id="profile-bar">
		<asp:LoginView ID="loginViewHeader" runat="server">
                <loggedintemplate>
                    <div id="profile-signin" style="vertical-align: middle;">
	                    <h6>
	                        <asp:Label ID="lblLoginName" runat="server" />
	                    </h6>
                    </div>
                    <ul>
	                    <li class="last">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="logout">Logout</asp:LinkButton>
	                    </li>
                    </ul>
                </loggedintemplate>
                <anonymoustemplate>
	                
                </anonymoustemplate>
             	</asp:LoginView>

            	</div>


		<div style="text-align: right; margin-bottom: 5px;">
			<br /><br />
    			<asp:Label ID="LoginMsg" runat="server" ForeColor="#FA5858"></asp:Label>
		</div>


		</td>
	</tr>
	</table>
	

	</td>
	
</tr>
</table>

<br />

<table width="975" border="0" align="center" cellpadding="0" cellspacing="0" ID="Table3">
<tr>
	<td>
	<div style="text-align:left;">
        
        <asp:LoginView runat="server" ID="loginView">
            <AnonymousTemplate>
                <div>

		<table width="975" height="35" border="0" align="center" cellpadding="0" cellspacing="0" background="images/bg_green_gradient.jpg">
		<tr>
			<td width="6"><img src="images/lft_green_gradient.jpg" border="0" width="6" height="35" /></td>
			<td>
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td align="center">&nbsp;</td>
			</tr>
			</table>
			</td>
			<td width="6"><img src="images/rt_green_gradient.jpg" border="0" width="6" height="35" /></td>
		</tr>
		</table>
		
		<table width="899" height="35" border="0" align="center" cellpadding="10" cellspacing="0">
		<tr>
			<td>

			<div align="center">
			<table border="0" style="background-color:#032564;" cellpadding="0" cellspacing="0" width="850">
			<tr>
			
				<td width="90%" valign="top">
			
				<br/><br/><br/><br/>
				<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td><img src="images/spacer.gif" border="0" width="20" height="1"/></td>
					<td valign="top" align="left">
				
					<div style="font-family: verdana; font-size: 18px; color: #fdcd00; font-weight: bold; text-shadow: black 3px 3px 2px; filter: Shadow(Color=#000000, Direction=135); line-height:200%;">Welcome to the Livongo CSR Portal...please Log In<br/></div>
					<br/><br/>

					<blockquote style="font-family: verdana; font-size: 18px; color: #fdcd00; font-weight: bold; text-shadow: black 3px 3px 2px; filter: Shadow(Color=#000000, Direction=135); line-height:200%;">
								
					<div id="portal-signin" style="vertical-align: middle;">
                       				<asp:HiddenField id="txtUserName2" runat="server"></asp:HiddenField>
                        			<label for="UserPassTextBox2">Password: </label>

                        			<asp:TextBox TextMode="password" ID="txtUserPwd2" CssClass="text" runat="server" style="VERTICAL-ALIGN:middle;"></asp:TextBox>
						<asp:RegularExpressionValidator id="regextxtUserPwd2" runat="server" controltovalidate="txtUserPwd2" errormessage='&lt;, &gt;, ", and ^ not allowed' display="Dynamic" validationexpression='([^<>\"\^])*'>*</asp:RegularExpressionValidator>
						<asp:RegularExpressionValidator id="RegularExpressionValidatortxtUserPwd2" runat="server" ValidationExpression="[^']*"  ErrorMessage="No single quotes" ControlToValidate ="txtUserPwd2">*</asp:RegularExpressionValidator>
					
                        			<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/go2.gif" OnClick="Login_Click2" value="Login" style="height:12px;border: #ffffff 1px solid;VERTICAL-ALIGN:middle;"/>
                        		</div>
              			
				
					</blockquote>
					

					</td>
				</tr>
				</table>
				</td>

				<td width="10%"><div align="center"><img src="images/spacer.gif" border="0" width="1" height="404" /></div></td>

			</tr>
			</table>
			</div>

			</td>
		</tr>
		</table>
		
		</div>
            </AnonymousTemplate> 
            <LoggedInTemplate></LoggedInTemplate>
        </asp:LoginView>
        </div>
        
        </td>
    </tr>
    </table>
    
</form>
</body>

</html>
