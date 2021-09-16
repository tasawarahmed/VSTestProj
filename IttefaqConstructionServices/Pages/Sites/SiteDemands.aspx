<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sites/SitesMaster.master" AutoEventWireup="true" CodeBehind="SiteDemands.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Sites.SiteDemands" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Site Demands:&nbsp</h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan4">
        <h3>New Demand</h3>
        <hr />
        <label>Active Sites</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbSite"></asp:DropDownList>
        </div>
        <hr />
        <%--            <label>Date</label>--%>
        <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
            <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
        </div>
        <%--            <label>Amount</label>--%>
        <div class="input-control text full-size">
            <%--                <input placeholder="Amount" id="txtAmountDebit"   ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)" />--%>
            <asp:TextBox runat="server" placeholder="Amount" ID="txtAmount" ClientIDMode="Static" ondrop="return false;" onpaste="return false;" onkeypress="return isNumberKeyOrDecimal(event)"></asp:TextBox>
        </div>
        <%--            <label>Description</label>--%>
        <div class="input-control text full-size">
            <%--                <input placeholder="Description of Transaction" id="txtDescriptionDebit" onkeyup="document.getElementById('txtDescriptionCredit').value = this.value" />bodyContents_body_txtDescriptionCredit'--%>
            <asp:TextBox runat="server" placeholder="Requesting Person" ClientIDMode="Static" ID="txtForwardedBy"></asp:TextBox>
        </div>
        <%--            <label>Description</label>--%>
        <div class="input-control text full-size">
            <%--                <input placeholder="Description of Transaction" id="txtDescriptionDebit" onkeyup="document.getElementById('txtDescriptionCredit').value = this.value" />bodyContents_body_txtDescriptionCredit'--%>
            <asp:TextBox runat="server" placeholder="Description of Transaction" ClientIDMode="Static" ID="txtDescription"></asp:TextBox>
        </div>
        <hr />
        <asp:Button runat="server" ID="btnOK" Visible="true" CssClass="button warning full-size" Text="Add Demand" OnClick="btnOK_Click"/>

    </div>
    <div class="cell colspan7">
        <asp:Panel runat="server" ID="pnlStatus" Visible="false">
            <h3>Pending Requests Details:</h3>
        <asp:GridView runat="server" ID="gridAccounts" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="false" AutoGenerateColumns="False" OnRowDataBound="gridAccounts_RowDataBound" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="id" SortExpression="id" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" SortExpression="isPending">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("isPending") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("isPending") %>' Enabled="true" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}"  SortExpression="date" />
                <asp:BoundField DataField="siteName" HeaderText="Site" SortExpression="siteName" />
                <asp:BoundField DataField="requestingPerson" HeaderText="Contact Person" SortExpression="requestingPerson" />
                <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                <asp:BoundField DataField="amount" HeaderText="Amount" DataFormatString ="{0:0,0.00}" SortExpression="amount">
                <FooterStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:Button runat="server" ID="btnUpdate" Visible="true" CssClass="button warning full-size" Text="Update Status" OnClick="btnUpdate_Click"/>
        </asp:Panel>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT tblGenSiteRequests.date, tblGenSites.siteName, tblGenSiteRequests.requestingPerson, tblGenSiteRequests.amount, tblGenSiteRequests.description, tblGenSiteRequests.isPending, tblGenSiteRequests.id FROM tblGenSiteRequests INNER JOIN tblGenSites ON tblGenSiteRequests.siteID = tblGenSites.id"></asp:SqlDataSource>
    </div>

</asp:Content>
