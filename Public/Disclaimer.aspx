<%@ Page Title="EosHealth - Disclaimer" Language="C#" MasterPageFile="~/MasterPages/Simple.master" AutoEventWireup="true" CodeFile="Disclaimer.aspx.cs" Inherits="Public_Disclaimer" ValidateRequest="true" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">

<div class="cntr_col min_height search_results">
    <div class="title">
        <h3><asp:Literal runat="server" ID="ltTitle" /> &amp; Terms of Use</h3>
    </div>
    <div class="content">
	    <div class="CmsContent"><asp:Literal runat="server" ID="ltContent" /></div>
    </div>        
</div>

<asp:DataList ID="DataList1" runat="server" RepeatColumns="1" DataSourceID="dsTest" Visible="False"> 

  <ItemTemplate>
		<table background="http://www.eoshealth.com/images/bg_cntr_col.gif" border="0" cellpadding="0" cellspacing="0" width="100%" style="width: 961px; background-repeat: no-repeat;"> 
		<tr> 
			<td colspan="3" background="http://www.eoshealth.com/images/bg_title_big.gif" style="font-size: 20px; font-family:'Trebuchet Ms',Verdana,sans-serif; font-size-adjust:none; font-stretch:normal; font-style:normal; font-variant:normal; font-weight:normal; color: #339933; height: 45px; vertical-align: middle; text-align: left;">&nbsp;&nbsp;Privacy Policy</td> 
		</tr> 
		<tr>
			<td style="width: 35px;">&nbsp;</td>
			<td valign="top" style="text-align: center;">
			<table border="0" cellpadding="0" cellspacing="0" style="width: 911px; text-align: center;"> 
			<tr> 
				<td style="font-size: 9pt; font: Verdana, Arial, sans serif; color: #666666; text-align: left;"> 
				<br/>
				<%# DataBinder.Eval(Container.DataItem, "Content") %>
				<br/><br/>
				</td> 
			</tr> 
			</table>
			</td>
			<td style="width: 15px;">&nbsp;</td>
		</tr>
    		</table>
    </ItemTemplate>

</asp:DataList>

<asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:WW %>" ID="dsTest" runat="server" SelectCommand="SELECT Content FROM PageContent WHERE PageTitle = 'Privacy'">
</asp:SqlDataSource>

<br/><br/>

</asp:Content>