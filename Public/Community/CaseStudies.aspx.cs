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

public partial class Public_WhoIsItFor_MedicalExpiriences : AppLayer.BasePage
{
    #region Properties

    protected string FeaturedVideoUrl
    {
        get { return ViewState["FeaturedVideoUrl"] != null ? ViewState["FeaturedVideoUrl"].ToString() : ""; }
        set { ViewState["FeaturedVideoUrl"] = value; }
    }

    protected string FeaturedVideoTitle
    {
        get { return ViewState["FeaturedVideoTitle"] != null ? ViewState["FeaturedVideoTitle"].ToString() : ""; }
        set { ViewState["FeaturedVideoTitle"] = value; }
    }

    protected string FeaturedVideoDescription
    {
        get { return ViewState["FeaturedVideoDescription"] != null ? ViewState["FeaturedVideoDescription"].ToString() : ""; }
        set { ViewState["FeaturedVideoDescription"] = value; }
    }

    #endregion

    #region Members
    protected int PageContentID = 0;
    protected int MEID
    {
        set { ViewState["MEID"] = value; }
        get { return AppLayer.Functions.ToInt(ViewState["MEID"]); }
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
            PageContentID = CMS.ID;

            this.MetaDataDetails = new BSLayer.MetaData();
            this.MetaDataDetails.Description = CMS.MetaDescription.Value;
            this.MetaDataDetails.Keywords = CMS.MetaKeywords.Value;
            this.MetaDataDetails.Title = CMS.PageTitle.Value;
        }

        if (!IsPostBack)
        {
            gvMedicalExperience.GoToPage(0);
            this.phMain.Visible = true;
            this.phlDetailedView.Visible = false;

            //ucCommunityBoard.Refresh();
            ucCommunityBoard.Visible = false;
            ucCommunityBoard.ExcludeJoinAsACommunity();

            ucMemberStories.ControlInit(5);
        }
    }
    #endregion

    #region OnInit
    protected override void OnInit(EventArgs e)
    {
        this.lbtnMEBack.Click += new EventHandler(lbtnMEBack_Click);
        this.lbtnMEDownloadArticle.Click += new EventHandler(lbtnMEDownloadArticle_Click);
        this.gvMedicalExperience.XDataSource += new ZFort.Web.UI.WebControls.XGridView.XDataSourceDelegate(XSourceDelegate);
        this.gvMedicalExperience.RowCommand += new GridViewCommandEventHandler(gvMedicalExperience_RowCommand);
        this.gvMedicalExperience.RowDataBound += new GridViewRowEventHandler(gvMedicalExperience_RowDataBound);
        base.OnInit(e);        
    }

    void gvMedicalExperience_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex > 0)
        {
            PlaceHolder phlDevider = (PlaceHolder)gvMedicalExperience.Rows[e.Row.DataItemIndex-1].FindControl("phlDevider");
            if (phlDevider != null)
            {
                phlDevider.Visible = true;
            }
        }
    }

    void lbtnMEBack_Click(object sender, EventArgs e)
    {
        this.phMain.Visible = true;
        this.phlDetailedView.Visible = false;        
    }

    void lbtnMEDownloadArticle_Click(object sender, EventArgs e)
    {
        int MEId = this.MEID;
        if (MEId > 0)
        {
            string filePath = BSLayer.ContentItem.GetItemDocumentUrl(BSLayer.ContentItem.ItemType.MedicalExperience, MEId);

            if (System.IO.File.Exists(Server.MapPath(filePath)))
            {
                byte[] FilePDF = System.IO.File.ReadAllBytes(Server.MapPath(filePath));

                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + DBLayer.MedicalExperience.GetDetails(MEId).Title + ".pdf");
                Response.ContentType = "application/pdf";
                Response.AppendHeader("content-length", FilePDF.Length.ToString());
                Response.BinaryWrite(FilePDF);
                Response.Flush();
            }
        }
    }    

    void gvMedicalExperience_RowCommand(object source, GridViewCommandEventArgs e)
    {        
        if (e.CommandName == "download")
        {
            int MEId = AppLayer.Functions.ToInt(e.CommandArgument);
            if(MEId > 0)
            {
                string filePath = BSLayer.ContentItem.GetItemDocumentUrl(BSLayer.ContentItem.ItemType.MedicalExperience, MEId);
                
                if( System.IO.File.Exists(Server.MapPath(filePath)))
                {
                    byte[] FilePDF = System.IO.File.ReadAllBytes(Server.MapPath(filePath));

                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + DBLayer.MedicalExperience.GetDetails(MEId).Title + ".pdf");
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("content-length", FilePDF.Length.ToString());
                    Response.BinaryWrite(FilePDF);
                    Response.Flush();        
                }
            }
        }
        else if (e.CommandName == "view")
        { 
            int MEId = AppLayer.Functions.ToInt(e.CommandArgument);
            if (MEId > 0)
            {                
                this.PopulateDetails(MEId);
            }
        }
    }    
    #endregion

    #region XSourceDelegate
    protected ZFort.Web.UI.WebControls.XListDataSourceInfo XSourceDelegate(string sort, bool isAscending, int pageSize, int currentPageIndex, object[] selectedKeys)
    {

        ZFort.Web.UI.WebControls.XListDataSourceInfo dataSourceInfo =
            AppLayer.Functions.ConvertListDetailsToXListDataSourceInfo(
                DBLayer.MedicalExperience.GetPagedList(
                    string.Empty,
                    DBLayer.MedicalExperience.ColumnName.DateCreated,
                    false,
                    pageSize,
                    currentPageIndex
                )
            );

        return dataSourceInfo;
    }
    #endregion

    #region PopulateDetails
    private void PopulateDetails(int MEId)
    {
        DBLayer.MedicalExperience.Details MEDetails = DBLayer.MedicalExperience.GetDetails(MEId);
        if ((MEDetails != null) && (!MEDetails.ID.IsNull))
        {
            this.MEID = MEId;
            ltMETitle.Text = MEDetails.Title.Value;
            ltMEDescription.Text = MEDetails.Description.Value;
            if (!MEDetails.DocName.IsNull)
            {
                ltMEDocName.Text = (MEDetails.DocName.Value.Trim().Length > 0 ? MEDetails.DocName.Value : Resources.Form_PageContent.DownloadArticle);
            }
            else { ltMEDocName.Text = Resources.Form_PageContent.DownloadArticle; }
            lbtnMEDownloadArticle.Visible = System.IO.File.Exists(BSLayer.ContentItem.GetItemDocumentFullPath(BSLayer.ContentItem.ItemType.MedicalExperience, MEId));

            phlMEImage.Visible = System.IO.File.Exists(Server.MapPath(BSLayer.ContentItem.GetItemThumbImageUrl(BSLayer.ContentItem.ItemType.MedicalExperience, MEId)));
            imgMEImage.ImageUrl = BSLayer.ContentItem.GetItemThumbImageUrl(BSLayer.ContentItem.ItemType.MedicalExperience, MEId);
            imgMEImage.AlternateText = MEDetails.Title.Value;

            this.phMain.Visible = false;
            this.phlDetailedView.Visible = true;
        }
    }
    #endregion 
}
