<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" CodeBehind="beneficiariesToEvent.aspx.cs" Inherits="IttefaqConstructionServices.Pages.NotForProfit.beneficiariesToEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
    <script>
        function CheckBoxCount() {
            var gv = document.getElementById("<%= Gridview1.ClientID %>");
            var inputList = gv.getElementsByTagName("input");
            var numChecked = 0;

            for (var i = 0; i < inputList.length; i++) {
                if (inputList[i].type == "checkbox" && inputList[i].checked) {
                    numChecked = numChecked + 1;
                }
            }
            var tb = document.getElementById("<%= txtCount.ClientID %>");
            tb.value = numChecked;
            //alert(numChecked);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Add Beneficiaries to Event:&nbsp<small>Add beneficiaries to event here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />
    <h3>Details</h3>
    <hr />
    <div class="cell colspan3">
        <label>Event Name</label>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" AutoPostBack="true" ID="cmbEvent"></asp:DropDownList>
        </div>
        <label>Area(s)</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtAreas" placeholder="Comma separated places, Blank for all Beneficiaries"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnGetBeneficiaries" CssClass="button primary full-size" Text="Get Beneficiaries" OnClick="btnGetBeneficiaries_Click" />
        <hr />
        <label>Beneficiaries Selected</label>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" ID="txtCount"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnAdd" CssClass="button primary full-size" Text="OK" OnClick="btnAdd_Click" />
    </div>
    <div class="cell colspan8">
        <asp:GridView AllowSorting="true" ID="Gridview1" Width="98%" CssClass="table striped hovered border bordered"  AutoGenerateColumns="False" runat="server">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="cb_count" runat="server" onclick="CheckBoxCount();"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
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
                <asp:TemplateField Visible="True" HeaderText="Address">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
