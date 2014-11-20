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

public partial class Public_Register_Registration : AppLayer.BasePage
{
    #region 
    protected override void OnInit(EventArgs e)
    {
        this.rptRegisterTips.ItemDataBound += new RepeaterItemEventHandler(rptRegisterTips_ItemDataBound);
        this.lbtnCommunityRegister.Click += new EventHandler(lbtnCommunityRegister_Click);
        this.lbtnRegister.Click += new EventHandler(lbtnRegister_Click);
        base.OnInit(e);
    }

    void rptRegisterTips_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        PlaceHolder phlHaveDescription = (PlaceHolder)e.Item.FindControl("phlHaveDescription");
        PlaceHolder phlHaveNoDescription = (PlaceHolder)e.Item.FindControl("phlHaveNoDescription");

        if (phlHaveDescription != null)
        {
            phlHaveDescription.Visible = AppLayer.Functions.ToString(((DataRowView)e.Item.DataItem)[DBLayer.RegisterTip.ColumnName.Description]).Trim().Length > 0;
            phlHaveNoDescription.Visible = !phlHaveDescription.Visible;
        }
    }

    void lbtnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Public/Register/Registration.aspx", true);
    }

    void lbtnCommunityRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Public/Register/Registration.aspx?register_type=community", true);
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
            rptRegisterTips.DataSource = DBLayer.RegisterTip.GetList();
            rptRegisterTips.DataBind();
        }
    }
    #endregion
}
