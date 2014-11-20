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

public partial class Public_PressRelease : AppLayer.BasePage
{
	#region PageVars
    private bool IsFullList
    {
        set { ViewState["IsFullList"] = value; }
        get { return AppLayer.Functions.ToBool(ViewState["IsFullList"]); }
    }
	protected int PageContentID
	{
		set { ViewState["PageContentID"] = value; }
		get { return AppLayer.Functions.ToInt(ViewState["PageContentID"]); }
	}
	protected int PressID
	{
		set { ViewState["PressID"] = value; }
		get { return AppLayer.Functions.ToInt(ViewState["PressID"]); }
	}
	#endregion

	#region OnInit
	protected override void OnInit(EventArgs e)
	{
        this.lbtnMore.Click += new EventHandler(lbtnMore_Click);
		this.lbtnPressBack.Click +=new EventHandler(lbtnPressBack_Click);
		this.lbtnDownloadArticle.Click += new EventHandler(lbtnDownloadArticle_Click);
		this.gvPress.RowDataBound += new GridViewRowEventHandler(gvPress_RowDataBound);
		this.gvPress.RowCommand += new GridViewCommandEventHandler(gvPress_RowCommand);
		this.gvPress.XDataSource += new ZFort.Web.UI.WebControls.XGridView.XDataSourceDelegate(XSourceDelegate);
		base.OnInit(e);
	}

    void lbtnMore_Click(object sender, EventArgs e)
    {
        this.PopulateAll();
    }

	void lbtnDownloadArticle_Click(object sender, EventArgs e)
	{
		byte[] FilePDF = System.IO.File.ReadAllBytes(Server.MapPath(BSLayer.ContentItem.GetItemDocumentUrl(BSLayer.ContentItem.ItemType.PressRelease, this.PressID)));

		Response.ClearContent();
		Response.AddHeader("Content-Disposition", "attachment; filename=" + DBLayer.PressRelease.GetDetails(this.PressID).Title + ".pdf");
		Response.ContentType = "application/pdf";
		Response.AppendHeader("content-length", FilePDF.Length.ToString());
		Response.BinaryWrite(FilePDF);
		Response.Flush();
		Response.Close();		
	}
	
	void lbtnPressBack_Click(object sender, EventArgs e)
	{
		phlList.Visible = true;
		phlPressView.Visible = false;
        if (!this.IsFullList)
        {
            lbtnMore.Visible = true;
        }
	}
	
	protected ZFort.Web.UI.WebControls.XListDataSourceInfo XSourceDelegate(string sort, bool isAscending, int pageSize, int currentPageIndex, object[] selectedKeys)
	{

		ZFort.Web.UI.WebControls.XListDataSourceInfo dataSourceInfo =
			AppLayer.Functions.ConvertListDetailsToXListDataSourceInfo(
                DBLayer.PressRelease.GetListForPublic(					
					pageSize,
					currentPageIndex,
					String.Empty
				)
			);

		return dataSourceInfo;
	}	
	
	void gvPress_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		if (e.CommandName == "view")
		{
			int id = Convert.ToInt32(e.CommandArgument);
			ShowPress(id);
		}
	}	

	void gvPress_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		Literal ltPDFDoc = (Literal)e.Row.FindControl("ltPDFDoc");
		PlaceHolder phlFeaturedTitle = (PlaceHolder)e.Row.FindControl("phlFeaturedTitle");
		PlaceHolder phlFeatured = (PlaceHolder)e.Row.FindControl("phlFeatured");
		PlaceHolder phlRegular = (PlaceHolder)e.Row.FindControl("phlRegular");

		if (ltPDFDoc != null)
		{
			DataRowView DrRow = (DataRowView)e.Row.DataItem;
			int id = AppLayer.Functions.ToInt(DrRow[DBLayer.PressRelease.ColumnName.ID]);
			if (id > 0)
			{
				bool IsFeatured = AppLayer.Functions.ToBool(DrRow[DBLayer.PressRelease.ColumnName.Featured]);
				phlFeatured.Visible = IsFeatured;
				phlFeaturedTitle.Visible = IsFeatured;
				phlRegular.Visible = !IsFeatured;

				string DocUrl = BSLayer.ContentItem.GetItemDocumentUrl(BSLayer.ContentItem.ItemType.PressRelease, id);
				if (System.IO.File.Exists(Server.MapPath(DocUrl)))
				{
					ltPDFDoc.Text = "<a href=\"" + DocUrl + "\" target=\"_blank\"><img src=\"/images/pdf_small.gif\" border=\"0\" /></a>";
				}
				else { ltPDFDoc.Text = ""; }
			}
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

        #region Populate top 5 records
        if (!Page.IsPostBack)
        {
            gvPress.EmptyDataText = Resources.ErrorMessages.NoRecords;
            gvPress.DataSource = DBLayer.PressRelease.GetListTopByDate(5);
            gvPress.DataBind();
            nbPress.Visible = false;
            lbtnMore.Visible = true;
            phlPressView.Visible = false;
            this.IsFullList = false;
        }
        #endregion

    }
	#endregion

	#region ShowPress
	private void ShowPress(int PressId)
	{
		this.PressID = PressId;
		phlList.Visible = false;
        lbtnMore.Visible = false;
		phlPressView.Visible = true;
		DBLayer.PressRelease.Details PDetails = DBLayer.PressRelease.GetDetails(PressId);
		if (PDetails != null && !PDetails.ID.IsNull)
		{
			ltPressTitle.Text = PDetails.Title.Value;
			ltPressDescription.Text = PDetails.Description.Value;
			ltPressDate.Text = PDetails.DateCreated.Value.ToShortDateString();
			ltUserName.Text = PDetails.UserProduced.Value;
			ltPublications.Text = PDetails.Publications.Value;
			phlList.Visible = false;
			phlPressView.Visible = true;

			lbtnDownloadArticle.Visible = System.IO.File.Exists(Server.MapPath(BSLayer.ContentItem.GetItemDocumentUrl(BSLayer.ContentItem.ItemType.PressRelease, this.PressID)));
		}
	}
	#endregion

    #region PopulateAll
    private void PopulateAll()
    {        
        gvPress.EmptyDataText = Resources.ErrorMessages.NoRecords;
        if (!Page.IsPostBack)
        {
            gvPress.GoToPage(0);
            phlList.Visible = true;
            phlPressView.Visible = false;
        }
        else
        {
            gvPress.GoToPage(gvPress.CurrentPage);
        }
        nbPress.Visible = true;
        lbtnMore.Visible = false;
        phlPressView.Visible = false;
        this.IsFullList = true;
    }
    #endregion
}