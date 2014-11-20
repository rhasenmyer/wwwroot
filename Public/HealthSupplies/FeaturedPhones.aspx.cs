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

public partial class Public_WellnessProducts_FeaturedPhones : AppLayer.BasePage
{
    #region PageVars
    protected int PhoneID
    {
        set { ViewState["PhoneID"] = value; }
        get { return AppLayer.Functions.ToInt(ViewState["PhoneID"]); }
    }
    protected int PageContentID
    {
        set { ViewState["PageContentID"] = value; }
        get { return AppLayer.Functions.ToInt(ViewState["PageContentID"]); }
    }
    #endregion       

    #region Initialize
    protected override void OnInit(EventArgs e)
    {
        this.xrptFeaturedPhones.ItemDataBound += new RepeaterItemEventHandler(xrptFeaturedPhones_ItemDataBound);
        base.OnInit(e);                
    }    
    #endregion

    #region xrptFeaturedPhones events
    protected void xrptFeaturedPhones_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (AppLayer.USession.IsMemberLogged)
        {
            switch (e.CommandName)
            {                    
                case "select":
                    int FeaturedPhoneId = AppLayer.Functions.ToInt(e.CommandArgument);
                    BSLayer.Shop.AddToCart(BSLayer.Shop.ProductType.FeaturedPhone, FeaturedPhoneId);
                    Response.Redirect("/Public/HealthSupplies/FeaturedPhones.aspx?pa=sc", true);
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
            this.PhoneID = AppLayer.Functions.ToInt(Request["item_id"]);
            if (AppLayer.Functions.ToString(Request.QueryString["pa"]) == "sc")
            {
                this.phlPA.Visible = true;
            }            

            xrptFeaturedPhones.DataSource = DBLayer.FeaturedPhone.GetList(this.PhoneID);
            xrptFeaturedPhones.DataBind();

            ucHealthSuppliesSmall.ControlInit(8);
        }
    }
    #endregion

    #region xrptFeaturedPhones_ItemDataBound
    void xrptFeaturedPhones_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex > 0)
        {
            PlaceHolder phlDevider = (PlaceHolder)xrptFeaturedPhones.Items[e.Item.ItemIndex - 1].FindControl("phlDevider");
            if (phlDevider != null)
            {
                phlDevider.Visible = true;
            }
        }
    }
    #endregion

}
