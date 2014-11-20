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

public partial class FAQ : AppLayer.BasePage
{
	#region PageVars
	private string CurrCatName = "";
	protected int CatID
	{
		set { ViewState["CatID"] = value; }
		get { return AppLayer.Functions.ToInt(ViewState["CatID"]); }
	}	
	#endregion

	#region OnInit
	protected override void OnInit(EventArgs e)
	{
		this.lbtnRefresh.Click += new EventHandler(lbtnRefresh_Click);
		this.rptFAQ.ItemDataBound += new RepeaterItemEventHandler(rptFAQ_ItemDataBound);
		base.OnInit(e);
	}	
	#endregion

	#region lbtnRefresh_Click
	void lbtnRefresh_Click(object sender, EventArgs e)
	{
        DataTable DtFAQ = DBLayer.FAQ.GetListPublic(this.CatID, this.tbFilter.Text == "Enter question" ? string.Empty : this.tbFilter.Text);
		rptFAQ.DataSource = DtFAQ;
		rptFAQ.DataBind();		
		
        phlNoRecords.Visible = DtFAQ.Rows.Count == 0;
	}
	#endregion

	#region rptFAQ_ItemDataBound
	void rptFAQ_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		PlaceHolder phlCatName = (PlaceHolder)e.Item.FindControl("phlCatName");

		if (phlCatName != null)
		{
			DataRowView DrRow = (DataRowView)e.Item.DataItem;
			string CatName = (AppLayer.Functions.ToString(DrRow[DBLayer.FAQCategory.ColumnName.Name]));
			if (CatName != this.CurrCatName)
			{
				phlCatName.Visible = true;
				this.CurrCatName = CatName;
			}
			else
			{
				phlCatName.Visible = false;
			}
		}
	}
	#endregion

	#region Page_Load
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack)
		{
            AppLayer.Functions.SetOnEnterEvent(lbtnRefresh, tbFilter);
			this.CatID = AppLayer.Functions.ToInt(Request["cat_id"]);

			DataTable DtFAQ = DBLayer.FAQ.GetListPublic(this.CatID, String.Empty);
			rptFAQ.DataSource = DtFAQ;
			rptFAQ.DataBind();

			phlNoRecords.Visible = DtFAQ.Rows.Count == 0;

			if (this.CatID > 0)
			{
				this.ltCatName.Text = DBLayer.FAQCategory.GetDetails(this.CatID).Name.Value;
			}
			else 
			{
				this.ltCatName.Text = Resources.Form_FAQCategory.AllCats;
			}

            if (!String.IsNullOrEmpty(Request["id"]))
            {
                ScriptManager.RegisterStartupScript(this, typeof(FAQ), "openThread", "ShowAnswer('trAnswer" + Request["id"].ToString()+"');", true);
            }
		}

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
	}
	#endregion
}
