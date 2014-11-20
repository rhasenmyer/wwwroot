using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Public_MealsToLive : AppLayer.BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		this.Response.Redirect("../EmployerPrograms/MealsToLive.aspx");
		
		DBLayer.PageContent.Details CMS = BSLayer.PageContent.GetPageContent(
                DBLayer.Page.GetPageIDByCode(Request.Url.AbsolutePath),
                BSLayer.Language.GetMembersLanguageId());

        	base.MetaDataDetails = new BSLayer.MetaData();

        	if (CMS != null)
        	{
			/*
            		this.ltTitle.Text = CMS.PageTitle.Value;
            		this.ltContent.Text = CMS.Content.Value;

            		this.MetaDataDetails = new BSLayer.MetaData();
            		this.MetaDataDetails.Description = CMS.MetaDescription.Value;
            		this.MetaDataDetails.Keywords = CMS.MetaKeywords.Value;
            		this.MetaDataDetails.Title = CMS.PageTitle.Value;
			*/
        	}
       
	}

	public string Truncate(string input, int characterLimit)
	{
		string output = input;

		// Check if the string is longer than the allowed amount
		// otherwise do nothing
		if (output.Length > characterLimit && characterLimit > 0)
		{

			// cut the string down to the maximum number of characters
			output = output.Substring(0,characterLimit);

			// Check if the character right after the truncate point was a space
			// if not, we are in the middle of a word and need to remove the rest of it
			if (input.Substring(output.Length,1) != " ")
			{
				int LastSpace = output.LastIndexOf(" ");
			
				// if we found a space then, cut back to that space
				if (LastSpace != -1)
				{
					output = output.Substring(0,LastSpace);  
				}
			}

			// Finally, add the "..."
			output += "...";    
		}
	 	return output;
	}
   

    
    
}