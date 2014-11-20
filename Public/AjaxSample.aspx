<%@ Page Language="C#" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="AjaxSample.aspx.cs" Inherits="Public_AjaxSample" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

<script type="text/javascript">
<!-- 
function WriteLoading(blockName)
{
	var strResponse = "<DIV ALIGN=center VALIGN=center STYLE='width:100%; height:100%'><IMG SRC='/images/loading.gif'></DIV>";
	blockObj = layer(blockName);
	blockObj.write(strResponse);
	blockObj.show();
}

function LoadContent(PageId)
{
	WriteLoading("divContent");
	var url = "/WS/WS.aspx?action=getcmscontent";
	var strParams = "page_id=" + PageId;
	var loaderl = new net.ContentLoader(url, on_LoadContent, null, "POST", strParams);
}    

function on_LoadContent()
{    
	var strResponse = this.req.responseText
	listModel = layer("divContent");
	listModel.write("");
	listModel.write(strResponse);
	listModel.show();
}
//-->
</script>    
    
    <asp:PlaceHolder runat="server" ID="phlForm">
    
        <!-- CMS Content Block -->
	    <div class="box">
            <h1><asp:Literal runat="server" ID="ltTitle" /></h1>
            <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
	    </div>
	    <!-- CMS Content Block -->
	
	    <table width="100%">
	        <tr>
	            <td valign="top" width="20%">
	                <asp:Repeater runat="server" ID="rptPages">
	                    <HeaderTemplate>
	                        <ul>
	                    </HeaderTemplate>    
	                    <ItemTemplate>	        
	                        <li>
	                            <a href="javascript:LoadContent(<%# AppLayer.Functions.ToString(DataBinder.Eval(Container.DataItem, DBLayer.Page.ColumnName.ID)) %>)"><asp:Literal runat="server" ID="ltMenuItem"></asp:Literal></a>
	                        </li>
	                    </ItemTemplate>
	                    <FooterTemplate>
	                        </ul>
	                    </FooterTemplate>	        
	                </asp:Repeater>	                
	            </td>
	            <td valign="top" width="80%" >
	                <div id="divContent"><b>Please select item from the list left.</b></div>
	            </td>
	        </tr>
	    </table>	    	    
	
	</asp:PlaceHolder>	
	
</asp:Content>