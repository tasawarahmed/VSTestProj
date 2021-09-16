<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Individuals/IndividualsMaster.master" AutoEventWireup="true" CodeBehind="IndividualsAccountStatus.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Individuals.IndividualsAccountStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Change Individual Account Status:&nbsp<small>Close or Reopen Accounts.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <div class="cell colspan5">
        <h3>Close Account</h3>
        <hr />
        <label>Name</label>
        <div class="input-control select full-size">
            <select>
                <option>First Active Account</option>
                <option>Second Active Account</option>
                <option>Third Active Account</option>
            </select>
        </div>
        <label>Date</label>
        <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
            <asp:TextBox runat="server" ID="TextBox2"></asp:TextBox>
        </div>
        <label>Notes</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
        </div>
        <br />
        <button class="button primary full-size">Close This Account</button>
        <hr />
        <h3>Reopen Accounts</h3>
        <hr />
        <label>Name</label>
        <div class="input-control select full-size">
            <select>
                <option>First Deactive Account</option>
                <option>Second Deactive Account</option>
                <option>Third Deactive Account</option>
            </select>
        </div>
        <label>Date</label>
        <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
            <asp:TextBox runat="server" ID="TextBox3"></asp:TextBox>
        </div>
        <label>Notes</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="TextBox4"></asp:TextBox>
        </div>
        <br />
        <button class="button primary full-size">Reopen This Account</button>
        <hr />
    </div>
    <div class="cell colspan5">
        <h3>Existing Active Accounts</h3>
        <hr />
        <asp:GridView runat="server" ID="gridRegions" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="First Column" SortExpression="name" />
                <asp:BoundField DataField="remarks" HeaderText="Second Column" SortExpression="remarks" />
            </Columns>
        </asp:GridView>
        <h3>Existing Closed Accounts</h3>
        <hr />
        <asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="First Column" SortExpression="name" />
                <asp:BoundField DataField="remarks" HeaderText="Second Column" SortExpression="remarks" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT [name], [remarks] FROM [tblSites]"></asp:SqlDataSource>
    </div>
</asp:Content>
