<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" CodeBehind="AddBeneficiries.aspx.cs" Inherits="IttefaqConstructionServices.Pages.NotForProfit.AddBeneficiries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>New Beneficiary:&nbsp<small>Add New Beneficiary Here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan3">
        <h4>Personal Details</h4>
        <hr />
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbTitle">
                <asp:ListItem>Select One</asp:ListItem>
                <asp:ListItem>Mr.</asp:ListItem>
                <asp:ListItem>Mrs.</asp:ListItem>
                <asp:ListItem>Miss</asp:ListItem>
                <asp:ListItem>Not to Mention</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Beneficiary" ID="txtBeneficiaryName"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="CNIC" ClientIDMode="Static" ID="txtCNIC" MaxLength="13" onkeypress="return isNumberKey(event)"></asp:TextBox>
        </div>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbEducation">
                <asp:ListItem>Select One</asp:ListItem>
                <asp:ListItem>None</asp:ListItem>
                <asp:ListItem>Primary</asp:ListItem>
                <asp:ListItem>Middle</asp:ListItem>
                <asp:ListItem>Matric</asp:ListItem>
                <asp:ListItem>Inter</asp:ListItem>
                <asp:ListItem>Post Inter Diploma</asp:ListItem>
                <asp:ListItem>Masters</asp:ListItem>
                <asp:ListItem>Post Masters Diploma</asp:ListItem>
                <asp:ListItem>Above Masters</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Education - Further Details" ID="txtFurtherEduDetails"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Telephonic Contact Details" ID="txtPhone"></asp:TextBox>
        </div>
        <div class="input-control text full-size" data-role="datepicker" data-format="dd-mmmm-yyyy">
            <asp:TextBox runat="server" placeholder="Date of Birth" ID="txtDate"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Address" ID="txtAddress"></asp:TextBox>
        </div>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbCity">
                <asp:ListItem>Select One</asp:ListItem>
                <asp:ListItem>Lahore</asp:ListItem>
                <asp:ListItem>Peshawar</asp:ListItem>
                <asp:ListItem>Nowshera</asp:ListItem>
                <asp:ListItem>Malakand</asp:ListItem>
                <asp:ListItem>Mardan</asp:ListItem>
                <asp:ListItem>Charsada</asp:ListItem>
            </asp:DropDownList>
        </div>

    </div>
    <div class="cell colspan6">
        <h4>Istehqaaq Details</h4>
        <hr />
        <table style="width: 100%">
            <tr>
                <td style="width: 50%">
                    <div class="input-control select full-size">
                        <asp:DropDownList runat="server" ID="cmbHouseStatus">
                            <asp:ListItem>Select One</asp:ListItem>
                            <asp:ListItem>Own House</asp:ListItem>
                            <asp:ListItem>Rented House</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 50%">
                    <div class="input-control select full-size">
                        <asp:DropDownList runat="server" ID="cmbIstehqaqBasis">
                            <asp:ListItem>Select One</asp:ListItem>
                            <asp:ListItem>Orphan</asp:ListItem>
                            <asp:ListItem>Widow</asp:ListItem>
                            <asp:ListItem>Divorced</asp:ListItem>
                            <asp:ListItem>Handicapped</asp:ListItem>
                            <asp:ListItem>Prisoner</asp:ListItem>
                            <asp:ListItem>Drug Addict</asp:ListItem>
                            <asp:ListItem>Nadaar</asp:ListItem>
                            <asp:ListItem>Other - Mention</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-control text full-size">
                        <asp:TextBox runat="server" placeholder="Monthly Income" ID="txtMonthlyIncome" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div class="input-control text full-size">
                        <asp:TextBox runat="server" placeholder="Source of Income" ID="txtIncomeSource"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Any Special Issue" ID="txtSpecialIssue"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Remarks" ID="txtRemarks"></asp:TextBox>
        </div>
        <h4>Family Structure</h4>
        <hr />
        <table style="width: 100%">
            <tr>
                <td style="width: 33%">
                    <div class="input-control text full-size">
                        <asp:TextBox runat="server" placeholder="Name of Family Member" ID="txtFamilyName"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 35%">
                    <div class="input-control select full-size">
                        <asp:DropDownList runat="server" ID="cmbFamilyRelationship">
                            <asp:ListItem>Select One</asp:ListItem>
                            <asp:ListItem>Father</asp:ListItem>
                            <asp:ListItem>Mother</asp:ListItem>
                            <asp:ListItem>Husband</asp:ListItem>
                            <asp:ListItem>Wife</asp:ListItem>
                            <asp:ListItem>Kid</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 33%">
                    <div class="input-control text full-size">
                        <asp:TextBox runat="server" placeholder="Age of Family Member" ID="txtFamilyAge" MaxLength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="input-control text full-size">
                        <asp:TextBox runat="server" placeholder="Education of Family Member" ID="txtEducation"></asp:TextBox>
                    </div>
                </td>
                <td></td>
                <td>
                    <div class="input-control text full-size">
                        <asp:TextBox runat="server" placeholder="Occupation of Family Member" ID="txtFamilyOccupation"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Button runat="server" ID="btnAddMember" CssClass="button primary full-size" Text="Add Member" OnClick="btnAddMember_Click" />
        <hr />
        <asp:Panel runat="server" ID="pnlGrid" Visible="false">
            <asp:GridView runat="server" ID="gridFamily" AutoGenerateColumns="False" CssClass="table striped hovered border bordered">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkChkBox" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Age">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Age") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAge" runat="server" Text='<%# Bind("Age") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Relationship">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Relationship") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRelationship" runat="server" Text='<%# Bind("Relationship") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Education">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Education") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEducation" runat="server" Text='<%# Bind("Education") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Occupation">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Occupation") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOccupation" runat="server" Text='<%# Bind("Occupation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table style="width: 100%">
                <tr>
                    <td style="width: 50%">
                        <asp:Button runat="server" ID="btnEditChecked" CssClass="button success full-size" Text="Edit Selected Member" OnClick="btnEditChecked_Click" />
                    </td>
                    <td style="width: 50%">
                        <asp:Button runat="server" ID="btnDelChecked" CssClass="button danger full-size" Text="Delete Selected Member(s)" OnClick="btnDelChecked_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

    </div>
    <div class="cell colspan2">
        <h4>Photo</h4>
        <hr />
        <img id="myImage" runat="server" src="data:image/jpeg; base64, " alt="Photo" width="250" height="250" />
        <br />
        <div class="input-control file full-size" data-role="input">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <button class="button"><span class="mif-folder"></span></button>
        </div>
        <asp:Button runat="server" ID="btnSubmitPhoto" CssClass="button primary full-size" Text="Submit Photo" OnClick="btnSubmitPhoto_Click" />
        <hr />
        <asp:Button runat="server" ID="btnAddBeneficiary" CssClass="button primary full-size" Text="Add Beneficiary" OnClick="btnAddBeneficiary_Click" />
        <hr />
    </div>
    <%--    <script type='text/javascript'>
        $(document).ready(function () {
            $("#txtCNIC").mask("99999-9999999-9");
        });

    </script>--%>
</asp:Content>