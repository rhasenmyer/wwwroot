<%@ Page Language="C#"%>
<%@ OutputCache Duration="1" Location="None"%>

<%@ Import Namespace="System" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Xml.Xsl" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Web.UI.WebControls.WebParts" %>
<%@ Import Namespace="System.Web.UI.HtmlControls" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
        <script language="C#" runat="server">

    
    protected void Page_Load(object sender, EventArgs e)
    {
            Response.CacheControl = "Public";
            if (Membership.GetUser() != null)
            {
                //get the display user name (if logged in)
                Label lblLoginName = (Label)this.loginViewHeader.FindControl("lblLoginName");
                lblLoginName.Text = "Welcome " + getDBValue("SELECT FirstName + ' ' + LastName FROM Eos..UserAccount WHERE Login = '" + Membership.GetUser().UserName + "'");

                //get the CS version of the user id (for the querystring of the user portal iframe URL)
                string CSID = getDBValue("SELECT dbo.GetCSByPID('" + Membership.GetUser().UserName + "')");
                HtmlControl frame1 = (HtmlControl)this.loginView.FindControl("frame1");
                frame1.Attributes["src"] = "UserDataCS.aspx?CS=" + CSID;
            }
    }
 
    //function to execute a sql statement and return a value
    protected string getDBValue(string Sql)
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
        conn.Open();
        SqlDataReader myReader = sqlCmd.ExecuteReader();
        while (myReader.Read())
        {
            ret = myReader[0].ToString();
        }
        myReader.Close();
        return ret;
    }
    

    // convert text xml to an xmlreader
    private XmlTextReader XmlToXmlReader(string xml)
    {
        StringReader textStream = new StringReader(xml);
        XmlTextReader xtr = new XmlTextReader(textStream);
        return xtr;
    }    

    //generate a login ticket
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

        // Create the cookie.
        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        
        Response.Redirect(Request.Url.ToString());
    }
    
    //logout this user
    public void logout(Object sender, EventArgs e)
    {
        //Response.Write("logout");
        FormsAuthentication.SignOut();

        Response.Redirect(Request.Url.ToString());
    }
       
    //click the login button..
    public void Login_Click(Object sender, EventArgs e)
    {
        //get the email address entered in
        TextBox user = (TextBox)this.loginViewHeader.FindControl("txtUserName1");
        //convert the email address into our user id
        string username = getDBValue("SELECT Login FROM WW..UserAccount WHERE Email = '" + user.Text + "'");
        
        TextBox pwd = (TextBox)this.loginViewHeader.FindControl("txtUserPwd");
        string password = pwd.Text;

        //validate the login
        if (Membership.ValidateUser(username, password))
        {
            Login(username);
        }
        else
        {
            LoginMsg.Text = "Login failed. Please check your user name and password and try again.";
        }
    }
 

        </script>    
<html>
<head id="Head1" runat="server">
    <title>EosHealth</title>
	<style type="text/css">
		.msg {
			font-family: Lucida Grande, Arial, Helvetica, sans-serif;
			font-size: 12px;
		}
		h3 { font-weight: bold; color: #01485b;}
		body {
			font-family: Lucida Grande, Arial, Helvetica, sans-serif;
			font-size: 12px;
		}
		table {
			font-family: Lucida Grande, Arial, Helvetica, sans-serif;
			font-size: 12px;
		}
		
		
        .menuProfile
        {
	        /*BORDER-BOTTOM: #FFFFFF 1px solid; BACKGROUND-COLOR: #000066; TEXT-ALIGN: center;*/
            height: 32px;
            width:700px;
            /*background: url(images/bg_blue_profilemenu.jpg) repeat;*/
            color: green;
            font-family: helvetica;
            font-weight:bold;
            text-align:center;
            /*padding-top:10px;*/
            text-transform: uppercase;
            xfont-size:16px;
        }
        
        /* Profile Bar */
        
        #profile-bar{
            xheight: 18px; 
            margin:0 10px 0 20px ;
            padding: 8px 0 5px 20px; 
            xfont-family: Arial, Helvetica, sans-serif;
            xbackground: #000066;
            font-size: 11px;
        }
        #profile-bar label{color:#CCCCCC;}
        #profile-bar a{color:#FFFFFF;}

        #profile-bar A:hover
        {
            COLOR: #FFE300; TEXT-DECORATION: none
        }

        #profile-bar h5{font-size: 14px;margin: 0;}
        #profile-bar ul{margin: 0; float: right; margin-right: 10px; }
        #profile-bar li{display:inline; list-style-type: none;padding: 0 15px 0 15px; border-right: 1px solid #999;font-size: 10px;text-transform:uppercase;}
        #profile-bar li.last{padding: 0 5px 0 15px; border-right: none;}

        #profile-bar li.last { border: 0; }
        #profile-bar h5 a{font-weight: bold; }
        #profile-bar ul, #profile-bar li{display:inline;text-align:right;}

        #profile-bar input.text{font-size: 9px;
        border:1px solid #c3c3c3;
        border-top:1px solid #7c7c7c;
        border-bottom:1px solid #dddddd;
        width: 80px;
        line-height: 10px;
        }
        #display-username{float:left;font-weight: bold;}
        #display-username a{font-weight: bold;}

        #profile-bar #profile-signin{float:left;}
        #profile-bar #profile-signin h6{display: inline;text-transform: uppercase;font-size: 11px;font-weight: bold; color: #CCCCCC;}
        #profile-bar #profile-signin label{text-transform: uppercase;margin: 0 3px 0 15px;font-size: 10px; font-family: Arial, Helvetica, sans-serif;}
	</style>
<script id="dynamicScript" src=""></script>
</head>
<body>
    
    <form id="form1" runat="server">
<!--(begin)menu1--------------------------------------------------------------------------------------------------->
<table width="800" height="32" border="0" align="center" cellpadding="0" cellspacing="0" ID="Table2">
    <tr>
    	<td class="menuProfile" colspan="2">
	
	<table width="800" height="32" border="0" align="center" cellpadding="0" cellspacing="0" background="images/bg_blue_profilemenu.jpg">
	<tr>
		<td width="6"><img src="images/lft_blue_profilemenu.jpg" border="0" width="10" height="32" /></td>
		<td>
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
                    <div id="profile-signin" style="vertical-align: middle;">
	                    <h6 style="VERTICAL-ALIGN:middle;">User Log In</h6>
                        <label for="UserNameTextBox">Email: </label>
                        <asp:TextBox CssClass="text" id="txtUserName1" style="width:140px;VERTICAL-ALIGN:middle;" runat="server"></asp:TextBox>
                        <label for="UserPassTextBox">Password: </label>
                        <asp:TextBox TextMode="password" ID="txtUserPwd" CssClass="text" runat="server" style="VERTICAL-ALIGN:middle;"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/go2.gif" OnClick="Login_Click" value="Login" style="height:12px;border: #ffffff 1px solid;VERTICAL-ALIGN:middle;"/>
                    </div>
                </anonymoustemplate>
              	</asp:LoginView>

        	</div>
		</td>
		<td width="6"><img src="images/rt_blue_profilemenu.jpg" border="0" width="10" height="32" /></td>
	</tr>
	</table>


        <div style="text-align:left;margin-left:5px;margin-bottom:5px;">
        <br />
    		<asp:Label ID="LoginMsg" runat="server" ForeColor="#FA5858"></asp:Label>
  	</div>
    </td>
</tr>
</table>
<!--(end)menu1----------------------------------------------------------------------------------------------------->

<br />
<table width="800" border="0" align="center" cellpadding="0" cellspacing="0" ID="Table3">
    <tr>
        <td>
        
    <div style="text-align:left;">
        <asp:HiddenField ID="StepVal" Value="1" runat="server"/>
        
        <asp:LoginView runat="server" ID="loginView">
            <AnonymousTemplate>
                <asp:Panel ID="Step1" runat="server" Visible="true">

			<!--// ...NON-LOGGED IN LANDING PAGE CONTENT HERE....//-->

			<center>
			Please Log in.
			</center>

			<!--// ...END NON-LOGGED IN LANDING PAGE CONTENT....//-->

                </asp:Panel>
            </AnonymousTemplate>
            <LoggedInTemplate>
		        
                <IFRAME id="frame1" scrolling="no" runat="server" frameborder="0" width="700px" height="700px"></IFRAME>
            </LoggedInTemplate>
        </asp:LoginView>
        <div id="Msg" runat="server"></div>
    </div>
        
        </td>
    </tr>
</table>
    
   </form>

</body>

</html>
