<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Accounts/AccountsMaster.master" AutoEventWireup="true" CodeBehind="SiteLiabAssociationWithPM.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Accounts.SiteLiabAssociationWithPM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Site Liabilities Association With PMs:&nbsp<small>Associate Site Liabilities with Project Managers Here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan5">
        <h2><%= Session["companyName"].ToString() %></h2>
        <h3>Current Association</h3>
        <hr />
        <asp:GridView runat="server" ID="gridCurrentAssociation" CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true" ShowFooter="False">
        </asp:GridView>
    </div>

    <div class="cell colspan6">
        <h2><%= Session["companyName"].ToString() %></h2>
        <h3>Revise Association</h3>
        <hr />
        <asp:GridView runat="server" ID="gridReviseAssociation" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" OnRowDataBound="gridReviseAssociation_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="accountName" HeaderText="Liability Account" SortExpression="Liability" />
                    <asp:TemplateField HeaderText="Project Managers">
                        <ItemTemplate>
                            <div class="input-control select full-size">
                                <asp:DropDownList runat="server" ID="cmbProjectManagers">
                                </asp:DropDownList>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
        <asp:Button runat="server" ID="btnReviseAssociation" CssClass="button primary full-size" Text="Revise Association for Selected Accounts" OnClick="btnReviseAssociation_Click" />
    </div>
</asp:Content>
