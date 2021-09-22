<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" CodeBehind="Donations.aspx.cs" Inherits="IttefaqConstructionServices.Pages.NotForProfit.Donations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Receive Donations:&nbsp<small>Receive donations here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan4">
        <h3>Details</h3>
        <hr />
        <label>Name of Donor</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbDonor"></asp:DropDownList>
        </div>
        <label>Date</label>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>
        <label>Amount</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Amount of Donation" ID="txtAmount"></asp:TextBox>
        </div>
        <asp:GridView AllowSorting="true" ID="gridAccounts" Width="98%" CssClass="table striped hovered border bordered"  AutoGenerateColumns="False" runat="server">
            <Columns>
                <asp:TemplateField Visible="False" HeaderText="ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("id") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblName" Text='<%# Bind("Name") %>' ></asp:Label>
                        <%--                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="Amount">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text=""></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server" placeholder="0" CssClass="input-control text full-size align-right" Text=""></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button runat="server" ID="btnEventCreate" CssClass="button primary full-size" Text="OK" OnClick="btnEventCreate_Click" />
    </div>
    <div class="cell colspan7">
        <h3>Donations</h3>
        <hr />
        <asp:GridView AllowSorting="True" ID="gridDonations" Width="98%" CssClass="table striped hovered border bordered"  AutoGenerateColumns="False" runat="server" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="Name of Donor" SortExpression="name" />
                <asp:BoundField DataField="address" HeaderText="Address" SortExpression="address" />
                <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" />
                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT tblGenDonors.name, tblGenDonors.address, tblGenDonations.date, tblGenDonations.amount, tblGenDonations.remarks FROM tblGenDonations INNER JOIN tblGenDonors ON tblGenDonations.donorID = tblGenDonors.id ORDER BY tblGenDonations.date DESC"></asp:SqlDataSource>
        </div>
</asp:Content>
