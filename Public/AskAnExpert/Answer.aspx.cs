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

public partial class Public_AskAnExpert_Answer : AppLayer.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DBLayer.PageContent.Details CMS = BSLayer.PageContent.GetPageContent(
            DBLayer.Page.GetPageIDByCode(Request.Url.AbsolutePath),
            BSLayer.Language.GetMembersLanguageId());

            base.MetaDataDetails = new BSLayer.MetaData();

            if (CMS != null)
            {
                this.ltTitle.Text = CMS.PageTitle.Value;
                this.ltContent.Text = CMS.Content.Value;

                this.MetaDataDetails = new BSLayer.MetaData();
                this.MetaDataDetails.Description = CMS.MetaDescription.Value;
                this.MetaDataDetails.Keywords = CMS.MetaKeywords.Value;
                this.MetaDataDetails.Title = CMS.PageTitle.Value;
            }

            if (String.IsNullOrEmpty(Request["CategoryID"]) || String.IsNullOrEmpty(Request["AskAnExpertID"]))
            {
                this.Visible = false;
                return;
            }

            ucSearchCategoryQuestion.CategoryID = AppLayer.Functions.ToInt(Request["CategoryID"]);
            ucAnswer.Refresh(AppLayer.Functions.ToInt(Request["AskAnExpertID"]));

            ucFAQShortTable.Refresh(4);
            ucTipOfTheDay.Refresh();

            this.rptAskAnExpertCategory.DataSource = DBLayer.AskAnExpertCategory.GetList();
            this.rptAskAnExpertCategory.DataBind();
        }
    }
}
