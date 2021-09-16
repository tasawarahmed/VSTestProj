<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard2.aspx.cs" Inherits="IttefaqConstructionServices.Dashboard2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Metro-UI-CSS-master/build/css/metro.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-colors.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-icons.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-responsive.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-rtl.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-schemes.css" rel="stylesheet" />
    <script src="Metro-UI-CSS-master/test/RequireJS/scripts/jquery.js"></script>
    <script src="Metro-UI-CSS-master/test/RequireJS/scripts/metro.js"></script>
    <script src="Metro-UI-CSS-master/docs/js/jquery.dataTables.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="grid">
            <div class="row cells12">
                <ul class="m-menu">
                    <li class="stick bg-black fg-white">
                        <asp:HyperLink runat="server" ID="HyperLink33" Text="" NavigateUrl="~/Default.aspx">
                                <span class="mif-home icon"></span>
                                &nbsp<%=Session["companyAbbr"].ToString() %>
                        </asp:HyperLink>
                    </li>
                    <li class="stick bg-yellow fg-black">
                        <asp:HyperLink runat="server" ID="HyperLink3" Text="" NavigateUrl="~/Dashboard2.aspx">
                                <span class="mif-chart-bars icon"></span>
                                Admin Summary
                        </asp:HyperLink>
                    </li>
                    <%--                    <li class="stick bg-darkCobalt fg-white">
                        <asp:HyperLink runat="server" ID="HyperLink27" NavigateUrl="">
                                <span class="mif-home icon"></span>
                                &nbspHome
                        </asp:HyperLink>
                    </li>--%>
                    <li class="stick bg-darkCobalt fg-white">
                        <a href="#" class="dropdown-toggle"><span class="mif-books icon"></span>&nbspGeneral Accounts</a>
                        <div class="m-menu-container" data-role="dropdown" data-no-close="true">
                            <div class="grid no-margin">
                                <div class="row cells5">
                                    <div class="cell padding10">
                                        <div class="image-container bordered handing ani image-format-hd">
                                            <div class="frame">
                                                <asp:Image runat="server" ID="Image3" Width="150" Height="150" ImageUrl="~/Images/accounts.jpg" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="cell colspan2">
                                        <h2 class="fg-white text-bold text-shadow">General Accounts Management</h2>
                                        <p class="padding20 no-padding-top no-padding-left no-padding-bottom fg-white">
                                            This portion of the application helps you to manage general accounts. General accounts mean the accounts NOT INVOLVING any site accounts. Hence no site related transactions should be made using this section of the application. For any site related transaction go to sites link in the main menu.
                                        </p>
                                    </div>
                                    <div class="cell colspan2">
                                        <ul class="unstyled-list">
                                            <li>
                                                <h3 class="text-shadow">Choose desired task</h3>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink34" NavigateUrl="~/Pages/Accounts/NewAccount.aspx">
                                        <span>New Account</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink35" NavigateUrl="~/Pages/Accounts/Transactions.aspx">
                                        <span>Transactions</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink9" NavigateUrl="~/Pages/Accounts/GeneralTransactions.aspx">
                                        <span>Most Frequent Transactions</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink36" NavigateUrl="~/Pages/Accounts/Status.aspx">
                                        <span>Account Status</span>
                                                </asp:HyperLink>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink37" NavigateUrl="~/Pages/Accounts/Reports.aspx">
                                                    <span>Summary Reports</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink38" NavigateUrl="~/Pages/Accounts/OtherReports.aspx">
                                                    <span>Other Reports</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="~/Pages/Accounts/SiteLiabAssociationWithPM.aspx">
                                                    <span>Site Liab to PMs</span>
                                                </asp:HyperLink>
                                            </li>
                                            <%--                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink10" NavigateUrl="~/Pages/Accounts/ChequesInHandDetails.aspx">
                                                    <span>Cheques in Hand Details</span>
                                                </asp:HyperLink>
                                            </li>--%>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li class="stick bg-darkCobalt fg-white">
                        <a href="#" class="dropdown-toggle"><span class="mif-flow-tree icon"></span>&nbspSites</a>
                        <div class="m-menu-container" data-role="dropdown" data-no-close="true">
                            <div class="grid no-margin">
                                <div class="row cells5">
                                    <div class="cell padding10">
                                        <div class="image-container bordered handing ani image-format-hd">
                                            <div class="frame">
                                                <asp:Image runat="server" ID="Image4" Width="150" Height="150" ImageUrl="~/Images/site.jpg" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="cell colspan2">
                                        <h2 class="fg-white text-bold text-shadow">Site Accounts Management</h2>
                                        <p class="padding20 no-padding-top no-padding-left no-padding-bottom fg-white">
                                            This portion of the application helps you to manage site accounts. Site accounts mean the accounts involving any site related flow of money. Hence no general transactions should be made using this section of the application.
                                        </p>
                                    </div>
                                    <div class="cell colspan2">
                                        <ul class="unstyled-list">
                                            <li>
                                                <h3 class="text-shadow">Choose desired task</h3>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink28" NavigateUrl="~/Pages/Sites/NewSite.aspx">
                                        <span>New Site</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink39" NavigateUrl="~/Pages/Sites/Regions.aspx">
                                        <span>New Region</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink40" NavigateUrl="~/Pages/Sites/subregions.aspx">
                                        <span>Sub Regions</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink44" NavigateUrl="~/Pages/Sites/SiteStatus.aspx">
                                        <span>Site Status</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink11" NavigateUrl="~/Pages/Sites/SiteDemands.aspx">
                                        <span>Site Demands</span>
                                                </asp:HyperLink>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink45" NavigateUrl="~/Pages/Sites/SiteAccounts.aspx">
                                        <span>Site Accounts</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink43" NavigateUrl="~/Pages/Sites/SiteTransactions.aspx">
                                        <span>Site Transactions</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink41" NavigateUrl="~/Pages/Sites/SiteReports.aspx">
                                                    <span>Site Reports</span>
                                                </asp:HyperLink>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <%--                    <li class="stick bg-darkCobalt fg-white">
                        <a href="#" class="dropdown-toggle"><span class="mif-users icon"></span>&nbspHuman Resources</a>
                        <div class="m-menu-container" data-role="dropdown" data-no-close="true">
                            <div class="grid no-margin">
                                <div class="row cells5">
                                    <div class="cell padding10">
                                        <div class="image-container bordered handing ani image-format-hd">
                                            <div class="frame">
                                                <asp:Image runat="server" ID="Image5" Width="150" Height="150" ImageUrl="~/Images/hr.jpg" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="cell colspan2">
                                        <h2 class="fg-white text-bold text-shadow">Human Resources Management</h2>
                                        <p class="padding20 no-padding-top no-padding-left no-padding-bottom fg-white">
                                            This portion of the application helps you to manage your human resources.
                                        </p>
                                    </div>
                                    <div class="cell colspan2">
                                        <ul class="unstyled-list">
                                            <li>
                                                <h3 class="text-shadow">Choose desired task</h3>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl="~/Pages/HumanResources/NewStaffMember.aspx">
                                        <span>New Staff Member</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl="~/Pages/HumanResources/StaffAllocation.aspx">
                                        <span>Staff Allocation</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="~/Pages/HumanResources/AllocationPlaces.aspx">
                                        <span>Staff Allocation Places</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink5" NavigateUrl="~/Pages/HumanResources/SalariesDisbursement.aspx">
                                        <span>Salaries Disbursement</span>
                                                </asp:HyperLink>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink6" NavigateUrl="#">
                                        <span>Page</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink7" NavigateUrl="#">
                                        <span>Page</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink8" NavigateUrl="#">
                                        <span>Page</span>
                                                </asp:HyperLink>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li class="stick bg-darkCobalt fg-white">
                        <a href="#" class="dropdown-toggle"><span class="mif-truck icon"></span>&nbspStore</a>
                        <div class="m-menu-container" data-role="dropdown" data-no-close="true">
                            <div class="grid no-margin">
                                <div class="row cells5">
                                    <div class="cell padding10">
                                        <div class="image-container bordered handing ani image-format-hd">
                                            <div class="frame">
                                                <asp:Image runat="server" ID="Image2" Width="150" Height="150" ImageUrl="~/Images/storage.jpg" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="cell colspan2">
                                        <h2 class="fg-white text-bold text-shadow">Stores Management</h2>
                                        <p class="padding20 no-padding-top no-padding-left no-padding-bottom fg-white">
                                            This portion of the application helps you to manage business inventory. Inventory means furniture, fixture, and tools which are kept by business for smooth running of its operations. Normally those items are contained in inventory who have some long physical life.
                                        </p>
                                    </div>
                                    <div class="cell colspan2">
                                        <ul class="unstyled-list">
                                            <li>
                                                <h3 class="text-shadow">Choose desired task</h3>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink29" NavigateUrl="~/Pages/Store/items.aspx">
                                        <span>New Item</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink42" NavigateUrl="~/Pages/Store/locations.aspx">
                                        <span>New Location</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink46" NavigateUrl="~/Pages/Store/transactions.aspx">
                                        <span>Transactions</span>
                                                </asp:HyperLink>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink48" NavigateUrl="~/Pages/Store/reports.aspx">
                                        <span>Reports</span>
                                                </asp:HyperLink>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>--%>
                    <li class="stick bg-darkCobalt fg-white">
                        <a href="#" class="dropdown-toggle"><span class="mif-tools icon"></span>&nbspTasks</a>
                        <div class="m-menu-container" data-role="dropdown" data-no-close="true">
                            <div class="grid no-margin">
                                <div class="row cells5">
                                    <div class="cell padding10">
                                        <div class="image-container bordered handing ani image-format-hd">
                                            <div class="frame">
                                                <asp:Image runat="server" ID="Image1" Width="150" Height="150" ImageUrl="~/Images/tasks.jpg" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="cell colspan2">
                                        <h2 class="fg-white text-bold text-shadow">Tasks Management</h2>
                                        <p class="padding20 no-padding-top no-padding-left no-padding-bottom fg-white">
                                            This portion of application helps you to do site related maintenance tasks.
                                        </p>
                                    </div>
                                    <div class="cell colspan2">
                                        <ul class="unstyled-list">
                                            <li>
                                                <h3 class="text-shadow">Choose desired task</h3>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink31" NavigateUrl="~/Pages/Configurations/Backup.aspx">
                                        <span>Take Backup</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink12" NavigateUrl="~/Pages/Membership/UserRoles.aspx">
                                        <span>User Role</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink13" NavigateUrl="~/Pages/Membership/NewUser.aspx">
                                        <span>New User</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink14" NavigateUrl="~/Pages/Membership/UpdateUser.aspx">
                                        <span>Update User</span>
                                                </asp:HyperLink>
                                            </li>
                                            <li>
                                                <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl="~/Pages/Membership/Logout.aspx">
                                        <span>Logout</span>
                                                </asp:HyperLink>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="cell colspan1">
                                        <ul class="unstyled-list">
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li class="place-right no-hovered">
                        <div class="input-control text">
                            <span class="mif-user prepend-icon"></span>
                            <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" ReadOnly="true" Text="Simple Text"></asp:TextBox>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="row cells12">
                <div class="cell colspan3">
                    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
                    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
                    <asp:GridView runat="server" ID="GridView1" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Site Name" DataField="Name" />
                            <%--                <asp:BoundField HeaderText="Status" DataField="Debit" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="viewStatus" Text='<%# Bind("Debit", "{0:n}") %>' CommandName="view" CommandArgument='<%# Bind("description") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="cell colspan3">
                    <asp:GridView runat="server" ID="GridView2" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Account Name" DataField="Name" />
                            <%--                <asp:BoundField HeaderText="Balance" DataField="Debit" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:TemplateField HeaderText="Balance" SortExpression="Status">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="viewStatus" Text='<%# Bind("Debit", "{0:n}") %>' CommandName="view" CommandArgument='<%# Bind("description") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="cell colspan3">
                    <asp:GridView runat="server" ID="GridView3" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" OnRowDataBound="GridView3_RowDataBound" OnRowCommand="GridView_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Account Name" DataField="Name" />
                            <asp:TemplateField HeaderText="Balance" SortExpression="Status">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="viewStatus" Text='<%# Bind("Debit", "{0:n}") %>' CommandName="view" CommandArgument='<%# Bind("description") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="cell colspan3">
                    <asp:GridView runat="server" ID="GridView4" CssClass="table striped hovered border bordered" AutoGenerateColumns="false" OnRowDataBound="GridView4_RowDataBound" OnRowCommand="GridView_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Account Name" DataField="Name" />
                            <asp:TemplateField HeaderText="Balance" SortExpression="Status">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Debit", "{0:n}") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="viewStatus" Text='<%# Bind("Debit", "{0:n}") %>' CommandName="view" CommandArgument='<%# Bind("description") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
