<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HumanResources/HumanResourcesMaster.master" AutoEventWireup="true" CodeBehind="StaffAllocation.aspx.cs" Inherits="IttefaqConstructionServices.Pages.HumanResources.StaffAllocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Staff Member Allocation:&nbsp<small>Allocate Staff Member to Various Places.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan5">
        <h3>Select Staff Member</h3>
        <hr />
        <div class="input-control select full-size">
            <asp:DropDownList runat="server"  ID="cmbStaffMember" AutoPostBack="true" OnSelectedIndexChanged="cmbStaffMember_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <hr />
    </div>
    <div class="cell colspan5">
            <h3>Current Allocation / Update Allocation:</h3>
            <asp:GridView runat="server" ID="gridAllocationPlaces" CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true"  AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Place Name" HeaderText="Place Name" SortExpression="Place Name" />
                    <asp:BoundField DataField="Place Type" HeaderText="Place Type" SortExpression="Place Type" />
                    <asp:TemplateField HeaderText="% Time Allocated">
                        <ItemTemplate>
                            <div class="input-control text full-size">
                                <asp:TextBox runat="server" placeholder="% Time Allocated e.g. 30" ID="txtAllocatedTime"></asp:TextBox>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        <asp:Button runat="server" ID="btnCreate" CssClass="button primary full-size" Text="OK" OnClick="btnCreate_Click"  />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT id, allocationPlaceName AS [Place Name], allocationPlaceType AS [Place Type] FROM tblGenStaffAllocationPlaces"></asp:SqlDataSource>
    </div>
</asp:Content>
