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

public partial class Public_WellnessProducts_Accessories : AppLayer.BasePage
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
        lbtnSearch.Click += new EventHandler(lbtnSearch_Click);
        xrptAccessorys.XDataSource += new ZFort.Web.UI.WebControls.XRepeater.XDataSourceDelegate(XDataSource);
    }
    #endregion

    #region xrptAccessorys events
    protected ZFort.Web.UI.WebControls.XListDataSourceInfo XDataSource(int pageSize, int currentPage)
    {
        ZFort.Web.UI.WebControls.XListDataSourceInfo dataSourceInfo =
            AppLayer.Functions.ConvertListDetailsToXListDataSourceInfo(
                DBLayer.Accessory.GetListPublic(pageSize, currentPage, this.tbKeyWord.Text.Trim() == "Enter keywords" ? "" : this.tbKeyWord.Text)
            );

        return dataSourceInfo;
    }
    protected void xrptAccessorys_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (AppLayer.USession.IsMemberLogged)
        {
            switch (e.CommandName)
            {
                case "select":
                    int AccessoryId = AppLayer.Functions.ToInt(e.CommandArgument);
                    BSLayer.Shop.AddToCart(BSLayer.Shop.ProductType.Accessory, AccessoryId);
                    Response.Redirect("/Public/HealthSupplies/Accessories.aspx?pa=sc", true);
                    break;
            }
        }
        else
        {
            Session["LastRequest"] = HttpContext.Current.Request.Url.ToString();
                Response.Redirect("/Public/Login.aspx", true);
        }
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
        if (!IsPostBack)
        {
            if (AppLayer.Functions.ToString(Request.QueryString["pa"]) == "sc")
            {
                this.phlPA.Visible = true;
            }

            xrptAccessorys.Refresh();
            
            ucCommunicatorsSmall.ControlInit();
            ucHealthSuppliesSmall.ControlInit(8);
        }
    }
    #endregion

    #region lbtnSearch_Click
    void lbtnSearch_Click(object sender, EventArgs e)
    {
        xrptAccessorys.Refresh();
    }
    #endregion
}
