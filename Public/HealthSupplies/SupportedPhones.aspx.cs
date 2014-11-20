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

public partial class Public_WellnessProducts_SupportedPhones : AppLayer.BasePage
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
        ddlCarrier.SelectedIndexChanged += new EventHandler(ddlCarrier_SelectedIndexChanged);
        lbtnSearch.Click += new EventHandler(lbtnSearch_Click);
        xrptCommunicators.XDataSource += new ZFort.Web.UI.WebControls.XRepeater.XDataSourceDelegate(XDataSource);
    }    
    #endregion

    #region ddlCarrier_SelectedIndexChanged
    void ddlCarrier_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DBLayer.Communicator.GetRecordsCount(this.tbKeyWord.Text.Trim() == "Enter keywords" ? "" : this.tbKeyWord.Text, AppLayer.Functions.ToInt(this.ddlCarrier.SelectedValue)) == 0)
        {
            string Term = this.tbKeyWord.Text;
            string SelVal = this.ddlCarrier.SelectedValue;
            this.tbKeyWord.Text = String.Empty;
            this.ddlCarrier.SelectedValue = "0";
            nbCommunicators.Refresh();
            this.tbKeyWord.Text = Term;
            this.ddlCarrier.SelectedValue = SelVal;
            this.phlNoResult.Visible = true;
        }
        else
        {
            nbCommunicators.Refresh();
        }
    }
    #endregion

    #region xrptCommunicators events
    protected ZFort.Web.UI.WebControls.XListDataSourceInfo XDataSource(int pageSize, int currentPage)
    {        
        ZFort.Web.UI.WebControls.XListDataSourceInfo dataSourceInfo =
            AppLayer.Functions.ConvertListDetailsToXListDataSourceInfo(
                DBLayer.Communicator.GetListPublic(
                pageSize, 
                currentPage, 
                this.tbKeyWord.Text.Trim() == "Enter keywords" ? "" : this.tbKeyWord.Text, AppLayer.Functions.ToInt(this.ddlCarrier.SelectedValue))
            );

        return dataSourceInfo;
    }

    int pos = 0;
    protected void xrptCommunicators_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            if (pos == 4)
            {
                ((PlaceHolder)e.Item.FindControl("phlSeparator")).Visible = true;
                pos = 1;
            }
            else{pos++;}
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

            ddlCarrier.Items.Clear();
            ddlCarrier.Items.Add(new ListItem("-Select Carrier-", "0"));

            DataTable DtCats = DBLayer.MobileCompany.GetList();
            for (int i = 0; i < DtCats.Rows.Count; i++)
            {
                ddlCarrier.Items.Add(new ListItem(AppLayer.Functions.ToString(DtCats.Rows[i][DBLayer.MobileCompany.ColumnName.Name]), AppLayer.Functions.ToString(DtCats.Rows[i][DBLayer.MobileCompany.ColumnName.ID])));
            }

            nbCommunicators.Refresh();

            ucCommunicatorsSmall.ControlInit();
        }
    }
    #endregion

    #region lbtnSearch_Click
    void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (DBLayer.Communicator.GetRecordsCount(this.tbKeyWord.Text.Trim() == "Enter keywords" ? "" : this.tbKeyWord.Text, AppLayer.Functions.ToInt(this.ddlCarrier.SelectedValue)) == 0)
        {
            string Term = this.tbKeyWord.Text;
            string SelVal = this.ddlCarrier.SelectedValue;
            this.tbKeyWord.Text = String.Empty;
            this.ddlCarrier.SelectedValue = "0";
            nbCommunicators.Refresh();

            this.tbKeyWord.Text = Term;
            this.ddlCarrier.SelectedValue = SelVal;
            this.phlNoResult.Visible = true;
        }
        else
        {
            nbCommunicators.Refresh();
        }
    }
    #endregion
}
