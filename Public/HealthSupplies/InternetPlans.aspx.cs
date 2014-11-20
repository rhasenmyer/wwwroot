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

public partial class Public_InternetPlans : AppLayer.BasePage
{
    #region PageVars
    protected int PageContentID
    {
        set { ViewState["PageContentID"] = value; }
        get { return AppLayer.Functions.ToInt(ViewState["PageContentID"]); }
    }
    protected int MobileCompanyID
    {
        set { ViewState["MobileCompanyID"] = value; }
        get { return AppLayer.Functions.ToInt(ViewState["MobileCompanyID"]); }
    }
    #endregion

    #region OnInit
    protected override void OnInit(EventArgs e)
    {        
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
            this.PageContentID = CMS.ID.Value;
            this.ltTitle.Text = CMS.PageTitle.Value;
            this.ltContent.Text = CMS.Content.Value;
            this.MetaDataDetails = new BSLayer.MetaData();
            this.MetaDataDetails.Description = CMS.MetaDescription.Value;
            this.MetaDataDetails.Keywords = CMS.MetaKeywords.Value;
            this.MetaDataDetails.Title = CMS.PageTitle.Value;

        }

        if (!Page.IsPostBack)
        {
            rptCompanys.DataSource = DBLayer.MobileCompany.GetList();
            rptCompanys.DataBind();            
        }
    }
    #endregion

    #region rptCompanys_ItemDataBound
    protected void rptCompanys_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            ((LinkButton)e.Item.FindControl("lbtnSelect")).CommandArgument = ((DataRowView)e.Item.DataItem).Row[DBLayer.MobileCompany.ColumnName.ID].ToString();
            ((Literal)e.Item.FindControl("litName")).Text = ((DataRowView)e.Item.DataItem).Row[DBLayer.MobileCompany.ColumnName.Name].ToString();
        }
    }
    #endregion

    #region rptCompanys_ItemCommand
    protected void rptCompanys_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "select":
                phlPlans1.Visible = false;
                phlPlans2.Visible = false;
                int MobileCompany = Convert.ToInt32(e.CommandArgument);

                if (MobileCompany == (int)BSLayer.MobileCompanyPlan.MobileCompany.Sprint)
                {
                    ltMobileGoupTitle1.Text = BSLayer.MobileCompanyPlan.GetGroupName(BSLayer.MobileCompanyPlan.MobileGroup.SprintSimplyEverything);
                    rptPlans1.DataSource = DBLayer.MobileCompanyPlan.GetList(MobileCompany, (int)BSLayer.MobileCompanyPlan.MobileGroup.SprintSimplyEverything);
                    rptPlans1.DataBind();

                    ltMobileGoupTitle2.Text = BSLayer.MobileCompanyPlan.GetGroupName(BSLayer.MobileCompanyPlan.MobileGroup.SprintDirectConnect);
                    rptPlans2.DataSource = DBLayer.MobileCompanyPlan.GetList(MobileCompany, (int)BSLayer.MobileCompanyPlan.MobileGroup.SprintDirectConnect);
                    rptPlans2.DataBind();

                    phlPlans1.Visible = true;
                    phlPlans2.Visible = true;
                }
                else if (MobileCompany == (int)BSLayer.MobileCompanyPlan.MobileCompany.ATT)
                {
                    ltMobileGoupTitle1.Text = BSLayer.MobileCompanyPlan.GetGroupName(BSLayer.MobileCompanyPlan.MobileGroup.ATTIndividual);
                    rptPlans1.DataSource = DBLayer.MobileCompanyPlan.GetList(MobileCompany, (int)BSLayer.MobileCompanyPlan.MobileGroup.ATTIndividual);
                    rptPlans1.DataBind();

                    ltMobileGoupTitle2.Text = BSLayer.MobileCompanyPlan.GetGroupName(BSLayer.MobileCompanyPlan.MobileGroup.ATTGoPhone);
                    rptPlans2.DataSource = DBLayer.MobileCompanyPlan.GetList(MobileCompany, (int)BSLayer.MobileCompanyPlan.MobileGroup.ATTGoPhone);
                    rptPlans2.DataBind();

                    phlPlans1.Visible = true;
                    phlPlans2.Visible = true;
                }
                else if (MobileCompany == (int)BSLayer.MobileCompanyPlan.MobileCompany.Fusion)
                {
                    ltMobileGoupTitle1.Text = BSLayer.MobileCompanyPlan.GetGroupName(BSLayer.MobileCompanyPlan.MobileGroup.FusionGeneral);
                    rptPlans1.DataSource = DBLayer.MobileCompanyPlan.GetList(MobileCompany, 0);
                    rptPlans1.DataBind();
                    phlPlans1.Visible = true;                    
                }

                MobileCompanyID = Convert.ToInt32(e.CommandArgument);
                break;
        }
    }
    #endregion    
}
