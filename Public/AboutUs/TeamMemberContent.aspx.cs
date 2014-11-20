using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class TeamMemberContent : AppLayer.BasePage
    {
    protected override void OnInit(EventArgs e)
    {
     
        base.OnInit(e);
    }
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["Id"]);
            string ImageUrl = BSLayer.ContentItem.GetItemImageUrl(BSLayer.ContentItem.ItemType.LeadershipItem, id);
            if (System.IO.File.Exists(Server.MapPath(ImageUrl)))
            {
                ltImage.Text = "<img src=\"" + ImageUrl + "\" border=\"0\" alt=\"Image\" style='float:left; margin:5px;' />";
            }
            else
            {
                ltImage.Text = "<img src=\"/images/no_image/no_image_85x62.gif\" border=\"0\" alt=\"Image\" style='float:left;'/>";
            }
            DBLayer.LeadershipTeam.Details details = DBLayer.LeadershipTeam.GetDetails(new ZFort.DB.DataType.DBInt(id));
            ltTitle.Text = "<h3 class='category'>"+details.FirstName+" "+details.LastName+"</h3>";
            ltDesc.Text = details.Description;
        }
    }
