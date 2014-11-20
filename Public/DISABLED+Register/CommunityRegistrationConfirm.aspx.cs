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

public partial class Public_Register_CommunityRegisterConfirm : AppLayer.BasePage
{
    #region PageVars
    private string Email
    {
        set { ViewState["ALEmail"] = value; }
        get { return AppLayer.Functions.ToString(ViewState["ALEmail"]); }
    }
    private string Password 
    {
        set { ViewState["ALPwd"] = value; }
        get { return AppLayer.Functions.ToString(ViewState["ALPwd"]); }
    }
    #endregion

    #region OnInit
    protected override void OnInit(EventArgs e)
    {
        this.lbtnOk.Click +=new EventHandler(lbtnOk_Click);
        this.Page.Load +=new EventHandler(Page_Load);
        base.OnInit(e);
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        this.phlCfmStart.Visible = true;
        this.phlCfmEnd.Visible = false;

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
            string AC = AppLayer.Functions.ToString(Request.QueryString["ac"]);
            if (AC.Length > 0)
            {
                DBLayer.UserAccount.Details UDetails = DBLayer.UserAccount.GetDetailsByAC(AC);
                if ((UDetails != null) && (!UDetails.ID.IsNull))
                {
                    this.Email = UDetails.Email.Value;
                    this.Password = UDetails.Password.Value;
                    UDetails.Active = true;                    
                    DBLayer.UserAccount.Update(UDetails.ID.Value, UDetails);
                    this.phlCfmEnd.Visible = true;
                    this.phlCfmStart.Visible = false;

                    if (Session["mail_sent"] == null || !Convert.ToBoolean(Session["mail_sent"]))
                    {
                        Session["mail_sent"] = true;
                        BSLayer.Mail.SendWelcomeForMembers(UDetails);
                    }
                }
            }            
        }
    }
    #endregion

    #region lbtnOk_Click
    protected void lbtnOk_Click(object sender, EventArgs e)
    {        
        if (this.Email.Length > 0 && this.Password.Length >0)
        {
            Response.Redirect("/Public/Login.aspx?pas=" + this.Password + "&email=" + this.Email, true);
        }        
    }
    #endregion

}
