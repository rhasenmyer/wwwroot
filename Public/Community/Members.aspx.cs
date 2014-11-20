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

public partial class Public_Community_Members : AppLayer.BasePage
{
    #region Page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rptBlogCategory.DataSource = DBLayer.BlogCategory.GetList();
            rptBlogCategory.DataBind();

            DBLayer.PageContent.Details CMS = BSLayer.PageContent.GetPageContent(
            DBLayer.Page.GetPageIDByCode(Request.Url.AbsolutePath),
            BSLayer.Language.GetMembersLanguageId());

            base.MetaDataDetails = new BSLayer.MetaData();

            if (CMS != null)
            {
                this.ltTitle.Text = "Blog";
                this.ltContent.Text = CMS.Content.Value;

                this.MetaDataDetails = new BSLayer.MetaData();
                this.MetaDataDetails.Description = CMS.MetaDescription.Value;
                this.MetaDataDetails.Keywords = CMS.MetaKeywords.Value;
                this.MetaDataDetails.Title = CMS.PageTitle.Value;
            }


            
            	if (!String.IsNullOrEmpty(Request["blog_id"]))
            	{
                	ucBlogOverview.Refresh(AppLayer.Functions.ToInt(Request["blog_id"]));
                	ShowControl(ucBlogOverview);
            	}
            	else if (!String.IsNullOrEmpty(Request["cat_id"]))
            	{
                	ucBlogList.Refresh(AppLayer.Functions.ToInt(Request["cat_id"]));
                	ShowControl(ucBlogList);
            	}
            	else
            	{
                	ucBlogList.Refresh();
                	ShowControl(ucBlogList);
            	}
           
        }
    }
    #endregion

    #region Show control
    private void ShowControl(Control target)
    {
        ucBlogList.Visible = false;
        ucBlogOverview.Visible = false;
        target.Visible = true;
    }
    #endregion

    #region ucBlogList events
    protected void ucBlogList_SelectBlog(object sender, Controls_Public_Blog_BlogList.SelectBlogEventArgs e)
    {
        ucBlogOverview.Refresh(e.BlogID);
        ShowControl(ucBlogOverview);
    }
    #endregion 

    #region ucBlogOverview events
    protected void ucBlogOverview_Back(object sender, Controls_Public_Blog_BlogOverview.BackEventArgs e)
    {
        ucBlogList.Refresh(e.CategoryID);
        ShowControl(ucBlogList);
    }
    #endregion

}
