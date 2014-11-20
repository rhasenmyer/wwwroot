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

public partial class Public_Insurance : AppLayer.BasePage
{
    #region PageVars
    protected int PageContentID
    {
        set { ViewState["PageContentID"] = value; }
        get { return AppLayer.Functions.ToInt(ViewState["PageContentID"]); }
    }
    #endregion

    #region OnInit
    protected override void OnInit(EventArgs e)
    {
        this.rptInsurance.ItemDataBound += new RepeaterItemEventHandler(rptInsurance_ItemDataBound);
        base.OnInit(e);
    }

    void rptInsurance_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Literal ltImage = (Literal)e.Item.FindControl("ltImage");
        if (ltImage != null)
        {
            DataRowView DrRow = (DataRowView)e.Item.DataItem;
            int id = AppLayer.Functions.ToInt(DrRow[DBLayer.Insurance.ColumnName.ID]);
            if (id > 0)
            {
                string ImageUrl = BSLayer.ContentItem.GetItemImageUrl(BSLayer.ContentItem.ItemType.Insurance, id);
                if (System.IO.File.Exists(Server.MapPath(ImageUrl)))
                {
                    ltImage.Text = "<img src=\"" + ImageUrl + "\" border=\"0\" alt=\"Image\" />";
                }
                else
                {
                    ltImage.Text = "<img src=\"/images/no_image/no_image_85x62.gif\" border=\"0\" alt=\"Image\" />";
                }
            }
        }
    }

    void lbtnDownloadPDF_Click(object sender, EventArgs e)
    {
        byte[] FilePDF = System.IO.File.ReadAllBytes(BSLayer.PageContent.GetPDFDocPath(this.PageContentID));

        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + DBLayer.PageContent.GetDetails(this.PageContentID).PageTitle + ".pdf");
        Response.ContentType = "application/pdf";
        Response.AppendHeader("content-length", FilePDF.Length.ToString());
        Response.BinaryWrite(FilePDF);
        Response.Flush();
        Response.Close();
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
            DataTable DtInsurance = DBLayer.Insurance.GetList();
            this.rptInsurance.DataSource = DtInsurance;
            this.rptInsurance.DataBind();
            ucCommunicatorsSmall.ControlInit();
        }
    }
    #endregion
}
