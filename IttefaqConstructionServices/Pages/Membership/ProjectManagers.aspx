<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectManagers.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Membership.ProjectManagers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Managers Data Entry Page</title>
    <link href="../../Metro-UI-CSS-master/build/css/metro-colors.css" rel="stylesheet" />
    <link href="../../Metro-UI-CSS-master/build/css/metro-icons.css" rel="stylesheet" />
    <link href="../../Metro-UI-CSS-master/build/css/metro-responsive.css" rel="stylesheet" />
    <link href="../../Metro-UI-CSS-master/build/css/metro-rtl.css" rel="stylesheet" />
    <link href="../../Metro-UI-CSS-master/build/css/metro-schemes.css" rel="stylesheet" />
    <link href="../../Metro-UI-CSS-master/build/css/metro.css" rel="stylesheet" />
    <script src="../../Metro-UI-CSS-master/test/RequireJS/scripts/jquery.js"></script>
    <script src="../../Metro-UI-CSS-master/test/RequireJS/scripts/metro.js"></script>
    <script src="../../Metro-UI-CSS-master/docs/js/jquery.dataTables.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="grid">
            <div class="row cells12">
                <h1>Project Manager Expenses:&nbsp<small>Record Your Expenses Here.</small></h1>
                <hr />
                <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
                <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
                <br />
            </div>
            <div class="row cells12">
                <div class="cell colspan4">
                    <h3>Expense Details:</h3>
                    <hr />
                    <div class="input-control text full-size">
                        <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
                    </div>
                    <label>Site Names</label>
                    <div class="input-control select full-size">
                        <asp:DropDownList runat="server" ID="cmbSites"></asp:DropDownList>
                    </div>
                    <label>Expenses</label>
                    <div class="input-control select full-size">
                        <asp:DropDownList runat="server" ID="cmbExpenses"></asp:DropDownList>
                    </div>
                    <div class="input-control text full-size">
                        <asp:TextBox runat="server" placeholder="Amount in Rs." ID="txtAmount"></asp:TextBox>
                    </div>
                    <div class="input-control text full-size">
                        <asp:TextBox runat="server" placeholder="Brief Description" ID="txtDescription"></asp:TextBox>
                    </div>
                    <asp:Button runat="server" ID="btnRecord" CssClass="button primary full-size" Text="Enter" OnClick="btnRecord_Click" />
                </div>
                <div class="cell colspan8">
                    <h3>Transaction Details:</h3>
                    <hr />
                    <asp:GridView runat="server" ID="gridTransaction" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="siteName" HeaderText="Site" SortExpression="siteName" />
                            <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" SortExpression="date" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Name" HeaderText="Account Name" SortExpression="Name" />
                            <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                            <asp:BoundField DataField="Debit" HeaderText="Debit" DataFormatString="{0:n}" SortExpression="debitAmount">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Credit" HeaderText="Credit" DataFormatString="{0:n}" SortExpression="creditAmount">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oprator" HeaderText="Operator" SortExpression="operator" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
