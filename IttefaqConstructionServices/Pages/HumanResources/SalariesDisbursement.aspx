<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HumanResources/HumanResourcesMaster.master" AutoEventWireup="true" CodeBehind="SalariesDisbursement.aspx.cs" Inherits="IttefaqConstructionServices.Pages.HumanResources.SalariesDisbursement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Salaries and Distributions:&nbsp<small>Monthly Staff Salaries Distributions.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan5">
        <label>Salary Month</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Salary Month & Year e.g. 08-2017" ID="txtAmount"></asp:TextBox>
        </div>
        <hr />
        <label>General Accounts Salary Account</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbGenSalAccount"></asp:DropDownList>
        </div>
        <hr />
        <label>Site Accounts Salary Account</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbSiteSalAccount"></asp:DropDownList>
        </div>
        <hr />
        <h3>Payment Accounts</h3>
        <hr />
            <asp:GridView runat="server" ID="gridPriSitesInfo" AutoGenerateColumns="False" CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true" OnRowDataBound="gridPriSitesInfo_RowDataBound" ShowFooter="True" >
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Account Name" SortExpression="Name" />
                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Status", "{0:0,0.00}") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("Status", "{0:0,0.00}") %>' ></asp:Label>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount Used">
                        <ItemTemplate>
                            <div class="input-control text full-size">
                                <asp:TextBox runat="server" placeholder="Amount Used" ID="txtAmount"></asp:TextBox>
                            </div>
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
                </Columns>
            </asp:GridView>
        <hr />
        <asp:Button runat="server" ID="btnCreate" CssClass="button primary full-size" Text="Pay Salaries" OnClick="btnCreate_Click"  />
        <hr />
    </div>
    <div class="cell colspan6">
            <h3>Existing Staff:</h3>
            <asp:GridView runat="server" ID="gridStaff" CssClass="table striped hovered border bordered"  data-role="datatable" data-seraching="true"  AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStaffID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Staff" SortExpression="Staff">
                        <EditItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Staff") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStaffName" runat="server" Text='<%# Bind("Staff") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salary" SortExpression="currentSalary">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("currentSalary") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTotalSalary" runat="server" Text='<%# Bind("currentSalary", "{0:0,0.00}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount Paid">
                        <ItemTemplate>
                            <div class="input-control text full-size">
                                <asp:TextBox runat="server" placeholder="Amount Paid" ID="txtSalary" Text='<%# Bind("currentSalary", "{0:0,0.00}") %>'></asp:TextBox>
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT staffName + ' - ' + staffFather AS Staff, id, currentSalary FROM tblGenStaff ORDER BY Staff"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT allocationPlaceName FROM tblGenStaffAllocationPlaces"></asp:SqlDataSource>
    </div>
</asp:Content>
