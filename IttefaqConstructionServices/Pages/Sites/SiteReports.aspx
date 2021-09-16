<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Sites/SitesMaster.master" AutoEventWireup="true" CodeBehind="SiteReports.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Sites.SiteReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Site Reports</h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan2">
        <asp:Button runat="server" ID="btnActiveSites" CssClass="button primary full-size" Text="Info - Active Sites" OnClick="btnActiveSites_Click" />
        <asp:Button runat="server" ID="btnClosedSites" CssClass="button primary full-size" Text="Info - Closed Sites" OnClick="btnClosedSites_Click" />
        <asp:Button runat="server" ID="btnMaterialsConsumption" CssClass="button primary full-size" Text="Material Consumption" OnClick="btnMaterialsConsumption_Click" />
    </div>

    <div class="cell colspan3">
        <asp:Panel runat="server" ID="pnlPriActiveSites" Visible="false">
            <h3>Active Sites</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbActiveSites" OnSelectedIndexChanged="cmbActiveSites_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlPriClosedSites" Visible="false">
            <h3>Closed Sites</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbClosedSites" OnSelectedIndexChanged="cmbClosedSites_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlPriSiteInfo" Visible="false">
            <div id="pDiv1">
                <h2><%= Session["companyName"].ToString() %></h2>
                <h2>Sites Cover Page</h2>
                <h3>Summary (as Db-Cr):</h3>
                <hr />
                <asp:GridView runat="server" ID="gridPriSitesInfo" AllowPaging="True" PageSize="500" AutoGenerateColumns="False" data-role="datatable" data-seraching="true" CssClass="table striped hovered border bordered" OnRowCommand="gridPriSitesInfo_RowCommand" OnRowDataBound="gridPriSitesInfo_RowDataBound" ShowFooter="True">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Site Name" SortExpression="Name" />
                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Status", "{0:0,0.00}") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="viewSecDetails" Text='<%# Bind("Status", "{0:0,0.00}") %>' CommandName="view" CommandArgument='<%# Bind("id") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="btnPntAccStmnt" CssClass="button primary full-size" Text="Print Cover Page" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv1')" />
            <hr />
        </asp:Panel>
        <asp:Panel ID="pnlMaterialsConsumption" runat="server" Visible="false">
            <h3>Sites</h3>
            <hr />
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbSites" OnSelectedIndexChanged="cmbSites_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <asp:Button runat="server" ID="btnDetailedMaterialConsumption" CssClass="button primary full-size" Text="Detailed Material Consumption" OnClick="btnDetailedMaterialConsumption_Click" />
            <hr />
        </asp:Panel>
    </div>

    <div class="cell colspan6">
        <asp:Panel runat="server" ID="pnlMaterialsConsumptionReport" Visible="false">
            <div id="pDiv4">
                <h2><%= Session["companyName"].ToString() %></h2>
                <h3><%=cmbSites.SelectedItem.ToString() %>: Details</h3>
                <h4>Materials Consumption Report:</h4>
                <hr />
                <asp:GridView runat="server" ID="gridMatConsumption" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="accountName" HeaderText="Account" SortExpression="accountName" />
                        <asp:BoundField DataField="quantity" HeaderText="Quantity" DataFormatString="{0:0,0.00}" SortExpression="quantity" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UoM" HeaderText="UoM" SortExpression="UoM" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="amount" HeaderText="Total Amount" DataFormatString="{0:0,0.00}" SortExpression="amount" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="averageRate" HeaderText="Average Rate" DataFormatString="{0:0,0.00}" SortExpression="averageRate" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="Button3" CssClass="button primary full-size" Text="Print Material Consumption Report" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv4')" />
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlMaterialsConsumptionDetailedReport" Visible="false">
            <div id="pDiv20">
                <h2><%= Session["companyName"].ToString() %></h2>
                <h3><%=cmbSites.SelectedItem.ToString() %>: Details</h3>
                <h4>Materials Consumption Detailed Report:</h4>
                <hr />
                <asp:GridView runat="server" ID="gridMatConsumptionDetailed" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" DataFormatString="{0:dd-MM-yyyy}" />
                        <asp:BoundField DataField="accountName" HeaderText="Account" SortExpression="accountName" />
                        <asp:BoundField DataField="quantity" HeaderText="Quantity" DataFormatString="{0:0,0.00}" SortExpression="quantity" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UoM" HeaderText="UoM" SortExpression="UoM" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="debit" HeaderText="Debit" DataFormatString="{0:0,0.00}" SortExpression="debit" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="credit" HeaderText="Credit" DataFormatString="{0:0,0.00}" SortExpression="credit" >
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="Button4" CssClass="button primary full-size" Text="Print Material Consumption Report" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv20')" />
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSecSiteInfo" Visible="false">
            <div id="pDiv2">
                <h2><%= Session["companyName"].ToString() %></h2>
                <h3><%=Session["siteName"] %>: Details</h3>
                <hr />
<%--                <asp:GridView runat="server" ID="gridSecSitesInfo" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="true" AutoGenerateColumns="False" OnRowCommand="gridSecSitesInfo_RowCommand" OnRowDataBound="gridSecSitesInfo_RowDataBound" ShowFooter="True">--%>
                <asp:GridView runat="server" ID="gridSecSitesInfo" CssClass="table striped hovered border bordered" AutoGenerateColumns="False" OnRowCommand="gridSecSitesInfo_RowCommand" OnRowDataBound="gridSecSitesInfo_RowDataBound" ShowFooter="True">
                    <Columns>
                        <asp:BoundField DataField="Account" HeaderText="Account" SortExpression="Account" />
                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Status", "{0:0,0.00}") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="viewTertiaryDetails" Text='<%# Bind("Status", "{0:0,0.00}") %>' CommandName="viewTer" CommandArgument='<%# Eval("AccID")  + " ; " + Eval("SiteID") %>'>View</asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details" Visible="false" SortExpression="AccID">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SiteID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details" Visible="false" SortExpression="AccID">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("AccID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="Button1" CssClass="button primary full-size" Text="Print Site Details" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv2')" />
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTerSiteInfo" Visible="false">
            <div id="pDiv3">
                <h2><%= Session["companyName"].ToString() %></h2>
                <h3><%=Session["siteName"] %>: <%=Session["siteAccountName"].ToString() %></h3>
                <hr />
                <asp:GridView runat="server" ID="gridTerSitesInfo" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="true" AutoGenerateColumns="False" OnRowDataBound="gridTerSitesInfo_RowDataBound" ShowFooter="True" OnRowCommand="gridTerSitesInfo_RowCommand">

                    <Columns>
                        <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" SortExpression="date" ItemStyle-Wrap="false" />
                        <asp:TemplateField HeaderText="TransID" SortExpression="transactionID">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("transactionID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="viewTransaction" Text='<%# Bind("transactionID") %>' CommandName="viewTransaction" CommandArgument='<%# Bind("transactionID") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                        <asp:BoundField DataField="chequeNumber" HeaderText="Cheque #" SortExpression="chequeNumber" Visible="False" />
                        <asp:BoundField DataField="quantity" HeaderText="Qty" SortExpression="quantity">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UoM" HeaderText="UoM" SortExpression="UoM" />
                        <asp:BoundField DataField="rate" HeaderText="Rate" SortExpression="rate">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Debit" SortExpression="debitAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("debitAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDebit" runat="server" Text='<%# Bind("debitAmount", "{0:0,0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Credit" SortExpression="creditAmount">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("creditAmount") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("creditAmount", "{0:0,0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="Button2" CssClass="button primary full-size" Text="Print Item Details" OnClick="btnPntTrialBalance_Click" OnClientClick="printDiv('pDiv3')" />
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTransaction" Visible="false">
            <h3>Transaction Details:</h3>
            <hr />
            <asp:GridView runat="server" ID="gridTransaction" CssClass="table striped hovered border bordered"  AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="date" HeaderText="Date" dataformatstring="{0:dd-MM-yyyy}" SortExpression="date" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="accountName" HeaderText="Account Name" SortExpression="accountName" />
                    <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    <asp:BoundField DataField="chequeNumber" HeaderText="Cheque" SortExpression="chequeNumber" />
                    <asp:BoundField DataField="debitAmount" HeaderText="Debit" DataFormatString="{0:0,0.00}" SortExpression="debitAmount">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="creditAmount" HeaderText="Credit" DataFormatString ="{0:0,0.00}" SortExpression="creditAmount">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT tblGenAccountsTransactions.date, tblGenAccounts.accountName, tblGenAccountsTransactions.description, tblGenAccountsTransactions.chequeNumber, tblGenAccountsTransactions.quantity, tblGenAccountsTransactions.UoM, tblGenAccountsTransactions.rate, tblGenAccountsTransactions.debitAmount, tblGenAccountsTransactions.creditAmount FROM tblGenAccountsTransactions INNER JOIN tblGenAccounts ON tblGenAccountsTransactions.accountID = tblGenAccounts.id"></asp:SqlDataSource>
        </asp:Panel>
    </div>
</asp:Content>