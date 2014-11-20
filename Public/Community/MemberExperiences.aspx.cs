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

public partial class Public_WhoIsItFor_MemberStories : AppLayer.BasePage
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

    protected string FeaturedVideoImage
    {
        get { return ViewState["FeaturedVideoImage"] != null ? ViewState["FeaturedVideoImage"].ToString() : ""; }
        set { ViewState["FeaturedVideoImage"] = value; }
    }
    

    #endregion

    #region Members
    protected int PageContentID = 0;    
    #endregion

    #region Properties

    protected int LastItemID
    {
        get { return ViewState["LastItemID"] != null ? (int)ViewState["LastItemID"] : 0; }
        set { ViewState["LastItemID"] = value; }
    }   

    #endregion

    #region IsLastItem
    protected bool IsLastItem(int id)
    {
        return LastItemID == id;
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
            this.PageContentID = CMS.ID;

            this.MetaDataDetails = new BSLayer.MetaData();
            this.MetaDataDetails.Description = CMS.MetaDescription.Value;
            this.MetaDataDetails.Keywords = CMS.MetaKeywords.Value;
            this.MetaDataDetails.Title = CMS.PageTitle.Value;
        }

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request["showAll"]) && Convert.ToBoolean(Request["showAll"]))
            {
                hlnkShowAll.Visible = false;
                phlVideos.Visible = false;
                rptMemberStories.DataSource = DBLayer.MemberStory.GetList();
            }
            else
            {
                hlnkShowAll.Visible = true;
                phlVideos.Visible = true;
                rptMemberStories.DataSource = DBLayer.MemberStory.GetList(4);
            }
            if (rptMemberStories.DataSource is DataTable)
            {
                DataTable dt = rptMemberStories.DataSource as DataTable;
                int rowsCount = dt.Rows.Count;
                LastItemID = AppLayer.Functions.GetValue<int>(dt.Rows[rowsCount - 1], DBLayer.MemberStory.ColumnName.ID, 0);
            }
            rptMemberStories.DataBind();
            
            ucMembersExperiences.Refresh();
            //ucCommunityBoard.Refresh();
            ucCommunityBoard.Visible = false;
            phlVideos.Visible = false;


            phFeatured.Visible = false;
            DataRow drFeatured = DBLayer.MemberVideo.GetFeaturedVideo();
            if (drFeatured != null)
            {
                int id = AppLayer.Functions.GetValue<int>(drFeatured, DBLayer.MemberVideo.ColumnName.ID, 0);
                string title = AppLayer.Functions.GetValue<string>(drFeatured, DBLayer.MemberVideo.ColumnName.Title, "");
                string description = AppLayer.Functions.GetValue<string>(drFeatured, DBLayer.MemberVideo.ColumnName.Description, "");

                DBLayer.MemberVideo.Details memberDetails = DBLayer.MemberVideo.GetDetails(new ZFort.DB.DataType.DBInt(id));

                try
                {
                    switch ((BSLayer.MemberVideo.MemberType)memberDetails.VideoType.Value)
                    {
                        case BSLayer.MemberVideo.MemberType.Internal:
                            FeaturedVideoUrl = BSLayer.ContentItem.GetItemVideoUrl(BSLayer.ContentItem.ItemType.MemberVideo, id);
                            break;
                        case BSLayer.MemberVideo.MemberType.URL:
                            FeaturedVideoUrl = memberDetails.VideoURL.Value;
                            break;
                    }
                }
                catch { FeaturedVideoUrl = BSLayer.ContentItem.GetItemVideoUrl(BSLayer.ContentItem.ItemType.MemberVideo, id); }
                

                FeaturedVideoImage = BSLayer.ContentItem.GetItemImageUrl(BSLayer.ContentItem.ItemType.MemberVideo, id);
                FeaturedVideoTitle = AppLayer.Functions.HtmlEncode(title);
                FeaturedVideoDescription = description;
                phFeatured.Visible = true;
            }

        }
		
    }
    #endregion

    #region OnInit
    protected override void OnInit(EventArgs e)
    {
        lbtnDownloadPDF.Click += new EventHandler(lbtnDownloadPDF_Click);        
        base.OnInit(e);
    }    
    
    #endregion

    #region lbtnDownloadPDF_Click
    void lbtnDownloadPDF_Click(object sender, EventArgs e)
    {
        string filePath = BSLayer.PageContent.GetPDFDocPath(PageContentID);
        if (filePath == "") return;

        byte[] FilePDF = System.IO.File.ReadAllBytes(BSLayer.PageContent.GetPDFDocPath(this.PageContentID));

        Response.ClearContent();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + DBLayer.PageContent.GetDetails(this.PageContentID).PageTitle + ".pdf");
        Response.ContentType = "application/pdf";
        Response.AppendHeader("content-length", FilePDF.Length.ToString());
        Response.BinaryWrite(FilePDF);
        Response.Flush();        
    }
    #endregion
}
