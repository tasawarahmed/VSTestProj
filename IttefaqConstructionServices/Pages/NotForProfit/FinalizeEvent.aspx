<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" Inherits="IttefaqConstructionServices.Pages.NotForProfit.FinalizeEvent" CodeBehind="FinalizeEvent.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Finalize an Event:&nbsp<small>Finalize events after happening here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
        <h3>Details</h3>
        <hr />
    <div class="cell colspan2">
        <h4>General</h4>
        <hr />
        <label>Event Name</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbEvent" OnSelectedIndexChanged="cmbEvent_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <label>Event Date</label>
            <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
                <asp:TextBox runat="server" placeholder="Date" ID="txtDate"></asp:TextBox>
            </div>
        <label>Event Place</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Place of Event" ID="txtPlace"></asp:TextBox>
        </div>
        <label>Remarks</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Remarks" ID="txtRemarks"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnEventFinalize" CssClass="button primary full-size" Text="Finalize" OnClick="btnEventFinalize_Click" />
    </div>
    <div class="cell colspan3">
        <h4>Financials</h4>
        <hr />

        <label>Benefits Per Head</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Benefits Per Head" ID="txtBenefitsPerHead"></asp:TextBox>
        </div>
        <label>Other Benefits Given</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Non Monetary Benefits" ID="txtOtherBenefits"></asp:TextBox>
        </div>
        <h5>Direct Expenses</h5>
        <label>Total Benefits to Beneficiaries</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Total Benefits Given" ID="txtBenefits"></asp:TextBox>
        </div>
        <hr />
        <h5>Administrative Expenses</h5>
        <asp:GridView AllowSorting="true" ID="gridExpenses" Width="98%" CssClass="table striped hovered border bordered"  AutoGenerateColumns="False" runat="server">
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
        <hr />
        <h5>Allocation of Expenses</h5>
        <asp:GridView AllowSorting="true" ID="gridBanks" Width="98%" CssClass="table striped hovered border bordered"  AutoGenerateColumns="False" runat="server">
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
        </div>

    <div class="cell colspan6">
        <h4>Beneficiaries Involved (Write A for Absent)</h4>
        <hr />
        <asp:GridView AllowSorting="true" ID="Gridview1" Width="98%" CssClass="table striped hovered border bordered"  AutoGenerateColumns="False" runat="server">
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
                <asp:TemplateField Visible="True" HeaderText="Address">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="City">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("City") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="Benefit Given">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Benefit") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtBenefit" runat="server" CssClass="input-control text full-size align-right" Text='<%# Bind("Benefit" , "{0:n}") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="Other Benefits">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text=""></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtNMonetary" runat="server" CssClass="input-control text full-size" Text=""></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
