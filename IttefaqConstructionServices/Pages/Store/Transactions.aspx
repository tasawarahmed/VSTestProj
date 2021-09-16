<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Store/StoreMaster.master" AutoEventWireup="true" CodeBehind="Transactions.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Store.Transactions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Routine Transactions:&nbsp<small>Enter routine transactions here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan2">
        <asp:Button runat="server" ID="btnReceiveItem" CssClass="button primary full-size" Text="Receive" OnClick="btnReceiveItem_Click" />
        <asp:Button runat="server" ID="btnSaleItem" CssClass="button primary full-size" Text="Sales" OnClick="btnSaleItem_Click" />
        <asp:Button runat="server" ID="btnInternalIssuance" CssClass="button primary full-size" Text="Internal Issuance" OnClick="btnInternalIssuance_Click"/>
        <asp:Button runat="server" ID="btnWastage" CssClass="button primary full-size" Text="Wastage" OnClick="btnWastage_Click"/>
    </div>

    <div class="cell colspan2">
        <asp:Panel runat="server" ID="pnlDate" Visible="false">
<%--            <label>Date</label>--%>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlCategories" Visible="false">
            <label>Categories</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbCategories" AutoPostBack="true" OnSelectedIndexChanged="cmbCategories_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSubCategories" Visible="false">
            <label>Sub Categories</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSubCategories"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlParties" Visible="false">
                        <label>Suppliers, Cash and Banks</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSuppliers"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlPurchasesAccount" Visible="false">
            <label>Purchase Account</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbInventory"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlWastageAccount" Visible="false">
            <label>Wastage (Expense) Account</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbWastageAccount"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlLocation1" Visible="false">
            <label>Source Location</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbSourceLocation" AutoPostBack="True" OnSelectedIndexChanged="cmbSourceLocation_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlLocation2" Visible="false">
            <label>Destination Location</label>
            <div class="input-control select full-size">
                <asp:DropDownList runat="server" ID="cmbDestinationLocation"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSimpleQty" Visible="false">
<%--        <label>Address</label>--%>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtQuantity" placeholder="Qunatity"></asp:TextBox>
        </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDetails" Visible="false">
<%--        <label>Address</label>--%>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtAmount" placeholder="Amount"></asp:TextBox>
        </div>
<%--        <label>Address</label>--%>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtChequeNumber" placeholder="Cheque Number"></asp:TextBox>
        </div>
<%--        <label>Address</label>--%>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtDescription" placeholder="Description"></asp:TextBox>
        </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlReceiveItem" Visible="false">
        <asp:Button runat="server" ID="btnReceiveItem1" CssClass="button primary full-size" Text="Receive" OnClick="btnReceiveItem1_Click"/>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSaleItem" Visible="false">
        <asp:Button runat="server" ID="btnSaleItem1" CssClass="button primary full-size" Text="Sale" OnClick="btnSaleItem1_Click"/>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlInternalIssuance" Visible="false">
        <asp:Button runat="server" ID="btnInternalIssuance1" CssClass="button primary full-size" Text="Internal Issuance" OnClick="btnInternalIssuance1_Click"/>
            <hr />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlWastage" Visible="false">
        <asp:Button runat="server" ID="btnWastage1" CssClass="button primary full-size" Text="Record Wastage" OnClick="btnWastage1_Click"/>
            <hr />
        </asp:Panel>
    </div>

    <div class="cell colspan6">
        <asp:Panel runat="server" ID="pnlInventoryPurchase" Visible="false">
            <asp:GridView runat="server" ID="gridInventorySalesAndPurchase" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                    <asp:BoundField DataField="Item Name" HeaderText="Item Name" SortExpression="Item Name" />
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <div class="input-control text full-size" >
                                <asp:TextBox ID="txtQty" placeholder="Quantity of item purchased" runat="server"></asp:TextBox>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <div class="input-control text full-size" >
                                <asp:TextBox ID="txtAmount" placeholder="Total Amount" runat="server"></asp:TextBox>
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlInventorySale" Visible="false">
            <asp:GridView runat="server" ID="gridInventorySale" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                    <asp:BoundField DataField="Item Name" HeaderText="Item Name" SortExpression="Item Name" />
                    <asp:BoundField DataField="Receipts" HeaderText="Receipts" ReadOnly="True" SortExpression="Receipts">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Issues" HeaderText="Issues" ReadOnly="True" SortExpression="Issues">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Quantity in Hand" SortExpression="Quantity in Hand">
                        <EditItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("[Quantity in Hand]") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblQtyInHand" runat="server" Text='<%# Bind("[Quantity in Hand]") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <div class="input-control text full-size" >
                                <asp:TextBox ID="txtQty" placeholder="Quantity of item purchased" runat="server"></asp:TextBox>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <div class="input-control text full-size" >
                                <asp:TextBox ID="txtAmount" placeholder="Total Amount" runat="server"></asp:TextBox>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlInventoryGenIssuance" Visible="false">
            <asp:GridView runat="server" ID="gridInventoryIssuance" CssClass="table striped hovered border bordered" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="id" InsertVisible="False" SortExpression="id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category" />
                    <asp:BoundField DataField="Item Name" HeaderText="Item Name" SortExpression="Item Name" />
                    <asp:BoundField DataField="Receipts" HeaderText="Receipts" ReadOnly="True" SortExpression="Receipts">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Issues" HeaderText="Issues" ReadOnly="True" SortExpression="Issues">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Quantity in Hand" SortExpression="Quantity in Hand">
                        <EditItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("[Quantity in Hand]") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblQtyInHand" runat="server" Text='<%# Bind("[Quantity in Hand]") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <div class="input-control text full-size" >
                                <asp:TextBox ID="txtQty" placeholder="Quantity of item purchased" runat="server"></asp:TextBox>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div> 
</asp:Content>