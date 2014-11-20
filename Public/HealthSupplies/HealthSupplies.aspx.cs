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

public partial class Public_WellnessProducts_HealthSupplies : AppLayer.BasePage
{
    #region PageVars
    protected int PageContentID
    {
        set { ViewState["PageContentID"] = value; }
        get { return AppLayer.Functions.ToInt(ViewState["PageContentID"]); } 
    }
    #endregion

    #region Initialize
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ddlCategory.SelectedIndexChanged += new EventHandler(ddlCategory_SelectedIndexChanged);
        lbtnSearch.Click += new EventHandler(lbtnSearch_Click);
        xrptHealthSupplies.XDataSource += new ZFort.Web.UI.WebControls.XRepeater.XDataSourceDelegate(XDataSource);
    }    
    #endregion

    #region 
    void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DBLayer.HealthSupply.GetRecordsCount(this.tbKeyWord.Text.Trim() == "Keywords Search" ? "" : this.tbKeyWord.Text, AppLayer.Functions.ToInt(this.ddlCategory.SelectedValue)) == 0)
        {
            string Term = this.tbKeyWord.Text;
            string SelVal = this.ddlCategory.SelectedValue;
            this.tbKeyWord.Text = String.Empty;
            this.ddlCategory.SelectedValue = "0";
            nbHealthSupplys.Refresh();
            this.tbKeyWord.Text = Term;
            this.ddlCategory.SelectedValue = SelVal;
            this.phlNoResult.Visible = true;
        }
        else
        {
            nbHealthSupplys.Refresh();
        }
    }
    #endregion

    #region xrptHealthSupplies events
    protected ZFort.Web.UI.WebControls.XListDataSourceInfo XDataSource(int pageSize, int currentPage)
    {
        ZFort.Web.UI.WebControls.XListDataSourceInfo dataSourceInfo =
            AppLayer.Functions.ConvertListDetailsToXListDataSourceInfo(
                DBLayer.HealthSupply.GetListPublic(
                pageSize, 
                currentPage,
                this.tbKeyWord.Text.Trim() == "Keywords Search" ? "" : this.tbKeyWord.Text, AppLayer.Functions.ToInt(ddlCategory.SelectedValue))
            );

        return dataSourceInfo;

    }
    int pos = 0;
    protected void xrptHealthSupplies_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {

            ((PlaceHolder)e.Item.FindControl("phlSpecialPrices")).Visible = !AppLayer.USession.IsMemberLogged;
            if (pos == 4)
            {
                ((PlaceHolder)e.Item.FindControl("phlSeparator")).Visible = true;
                pos = 1;
            }
            else { pos++; }
        }
    }
    protected void xrptHealthSupplies_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
	//lblTest.Text = "function entered";

        if (AppLayer.USession.IsMemberLogged)
        {
            switch (e.CommandName)
            {
                case "select":
                    int HealthSupplyId = AppLayer.Functions.ToInt(e.CommandArgument);
                    BSLayer.Shop.AddToCart(BSLayer.Shop.ProductType.HealthSupply, HealthSupplyId);
                    if (AppLayer.USession.IsMemberLogged)
                    {
                        Response.Redirect("/User/Checkout.aspx");
                    }
                    else
                    {
                        Response.Redirect("/Public/HealthSupplies/HealthSupplies.aspx?pa=sc", true);
                    }
                    break;
            }
        }
        else
        {
            Response.Redirect("/Public/Register/Registration.aspx?program_id=10&from=product", true);
        }
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
	//lblTest.Text = "Page Load";

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
        if (!IsPostBack)
        {
            if (AppLayer.Functions.ToString(Request.QueryString["pa"]) == "sc")
            {
                this.phlPA.Visible = true;
            }

            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(new ListItem("All Health Supplies", "0"));

            DataTable DtCats = DBLayer.HealthSupplyCategory.GetList();
            for (int i = 0; i < DtCats.Rows.Count; i++)
            {
                ddlCategory.Items.Add(new ListItem(AppLayer.Functions.ToString(DtCats.Rows[i][DBLayer.HealthSupplyCategory.ColumnName.Name]), AppLayer.Functions.ToString(DtCats.Rows[i][DBLayer.HealthSupplyCategory.ColumnName.ID])));
                DataTable DtSubCats = DBLayer.HealthSupplyCategory.GetList(AppLayer.Functions.ToInt(DtCats.Rows[i][DBLayer.HealthSupplyCategory.ColumnName.ID]));

                for (int j = 0; j < DtSubCats.Rows.Count;j++)
                {
                    ddlCategory.Items.Add(
                        new ListItem(
                        AppLayer.Functions.ToString(DtCats.Rows[i][DBLayer.HealthSupplyCategory.ColumnName.Name]) + "/" + AppLayer.Functions.ToString(DtSubCats.Rows[j][DBLayer.HealthSupplyCategory.ColumnName.Name]), 
                        AppLayer.Functions.ToString(DtSubCats.Rows[j][DBLayer.HealthSupplyCategory.ColumnName.ID])));
                }
            }
            
            nbHealthSupplys.Refresh();

            DataTable DtOffers = DBLayer.SpecialOffer.GetList();
            if (DtOffers.Rows.Count > 0)
            {
                rptSpecialOffers.DataSource = DtOffers;
                rptSpecialOffers.DataBind();
                rptSpecialOffers.Visible = true;
            }
            else
            { 
                rptSpecialOffers.Visible = false; 
            }

            if (AppLayer.USession.IsMemberLogged)
            {
                rptSpecialOffers.Visible = false;
            }
        }
    }
    #endregion

    #region lbtnSearch_Click
    void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (DBLayer.HealthSupply.GetRecordsCount(this.tbKeyWord.Text.Trim() == "Keywords Search" ? "" : this.tbKeyWord.Text, AppLayer.Functions.ToInt(this.ddlCategory.SelectedValue)) == 0)
        {
            string Term = this.tbKeyWord.Text;
            string SelVal = this.ddlCategory.SelectedValue;
            this.tbKeyWord.Text = String.Empty;
            this.ddlCategory.SelectedValue = "0";
            nbHealthSupplys.Refresh();
            this.tbKeyWord.Text = Term;
            this.ddlCategory.SelectedValue = SelVal;
            this.phlNoResult.Visible = true;
        }
        else
        {
            nbHealthSupplys.Refresh();
        }
    }
    #endregion
}
