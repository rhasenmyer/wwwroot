<%@ Page Title="EosHealth - Program" Language="C#" ValidateRequest="true" MasterPageFile="~/MasterPages/PublicRight.master" AutoEventWireup="true" CodeFile="ProgramComponents.aspx.cs" Inherits="Public_ProgramComponents" %>
<%@ Register TagPrefix="uc" TagName="WWIs" Src="~/Controls/Public/WWIs.ascx" %>
<%@ Register TagPrefix="uc" TagName="AskAnExpert" Src="~/Controls/Public/AskAnExpert.ascx" %>

<asp:Content ID="cMain" runat="server" ContentPlaceHolderID="cphlMain">
<div class="about_programs">
    <asp:Literal runat="server" ID="ltTopImage"></asp:Literal>
    <h4>Living Well with Diabetes</h4> <span class="hidden"><asp:Literal runat="server" ID="ltTitle" /></span>
    <div id="divGCNT" class="CmsContent left_content_fix about_c_content" style="font-size: <%=(Request.Cookies["CMSFontSize"] == null ? "12" : Request.Cookies["CMSFontSize"].Value) %>px;"><asp:Literal runat="server" ID="ltContent" /></div>
<asp:Repeater runat="server" ID="rptProgramComponents" Visible="false">
<HeaderTemplate>
    <h3 class="category">Our Four Cornerstones</h3>
    <ul class="list resources">
</HeaderTemplate>
<ItemTemplate>
        <li>
            <div class="f_left">
                <div class="thumb_90x100"><span class="frame">&nbsp;</span><span class="swc0"><span class="swc1"><span class="swc2"><asp:Literal runat="server" ID="ltImage"></asp:Literal></span><span class="swc3">&nbsp;</span></span></span></div>
            </div>                    
            <div class="text">                   
                <h6><%# DataBinder.Eval(Container.DataItem, DBLayer.ProgramComponent.ColumnName.Title) %></h6>
                <%# DataBinder.Eval(Container.DataItem, DBLayer.ProgramComponent.ColumnName.Description) %>
            </div>
        </li>            
</ItemTemplate>
<FooterTemplate>
    </ul>
</FooterTemplate>	    
</asp:Repeater>

	<div class="pdf_down_box">

		<asp:Label ID="LabelPDFLink0"><a href="/Public/AboutUs/Downloads/EosHealth_FAQs.pdf" Class="pdf_downl_lnk margin_top_10" Target=_new>EosHealth Program FAQs</a></asp:Label>                                                

	</div>

</div>
</asp:Content>
<asp:Content ID="cRightPanel" runat="server" ContentPlaceHolderID="cphlRightPanel">
    <uc:WWIs ID="ucWWIs" runat="server" />
    <uc:AskAnExpert ID="ucAskAnExpert" runat="server" visible="false"/>
</asp:Content>