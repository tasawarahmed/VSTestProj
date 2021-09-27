<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" CodeBehind="Webcam.aspx.cs" Inherits="IttefaqConstructionServices.Pages.NotForProfit.Webcam" %>

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
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="CNIC" ClientIDMode="Static" ID="txtCNIC" MaxLength="13" onkeypress="return isNumberKey(event)"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnGetBeneficiary" CssClass="button primary full-size" Text="Get Beneficiary" OnClick="btnGetBeneficiary_Click" />
        <hr />
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbTitle">
                <asp:ListItem>Select One</asp:ListItem>
                <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                <asp:ListItem Value="Miss">Miss</asp:ListItem>
                <asp:ListItem Value="Not to Mention">Not to Mention</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Beneficiary" ID="txtBeneficiaryName"></asp:TextBox>
        </div>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbEducation">
                <asp:ListItem>Select One</asp:ListItem>
                <asp:ListItem Value="None">None</asp:ListItem>
                <asp:ListItem Value="Primary">Primary</asp:ListItem>
                <asp:ListItem Value="Middle">Middle</asp:ListItem>
                <asp:ListItem Value="Matric">Matric</asp:ListItem>
                <asp:ListItem Value="Inter">Inter</asp:ListItem>
                <asp:ListItem Value="Post Inter Diploma">Post Inter Diploma</asp:ListItem>
                <asp:ListItem Value="Masters">Masters</asp:ListItem>
                <asp:ListItem Value="Post Masters Diploma">Post Masters Diploma</asp:ListItem>
                <asp:ListItem Value="Above Masters">Above Masters</asp:ListItem>
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
                <asp:ListItem Value="Badrashi">Badrashi</asp:ListItem>
                <asp:ListItem Value="Lahore">Lahore</asp:ListItem>
                <asp:ListItem Value="Peshawar">Peshawar</asp:ListItem>
                <asp:ListItem Value="Nowshera">Nowshera</asp:ListItem>
                <asp:ListItem Value="Malakand">Malakand</asp:ListItem>
                <asp:ListItem Value="Mardan">Mardan</asp:ListItem>
                <asp:ListItem Value="Charsada">Charsada</asp:ListItem>
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
                            <asp:ListItem Value="Own House">Own House</asp:ListItem>
                            <asp:ListItem Value="Rented House">Rented House</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 50%">
                    <div class="input-control select full-size">
                        <asp:DropDownList runat="server" ID="cmbIstehqaqBasis">
                            <asp:ListItem>Select One</asp:ListItem>
                            <asp:ListItem Value="Orphan">Orphan</asp:ListItem>
                            <asp:ListItem Value="Widow">Widow</asp:ListItem>
                            <asp:ListItem Value="Divorced">Divorced</asp:ListItem>
                            <asp:ListItem Value="Handicapped">Handicapped</asp:ListItem>
                            <asp:ListItem Value="Prisoner">Prisoner</asp:ListItem>
                            <asp:ListItem Value="Drug Addict">Drug Addict</asp:ListItem>
                            <asp:ListItem Value="Nadaar">Nadaar</asp:ListItem>
                            <asp:ListItem Value="Other - Mention">Other - Mention</asp:ListItem>
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
        <div class="input-control text-area full-size">
            <asp:TextBox runat="server" Rows="2" placeholder="Remarks" ID="txtRemarks"></asp:TextBox>
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
        <table>
            <tr>
                <td style="width: 50%">
                    <div id="webcam"></div>
                    <br />
                </td>
                <td style="width: 50%">
                    <div class="align-center">
                        <img id="myImage" alt="Photo" width="100" height="150" />
                    </div>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnCameraStart" CssClass="button primary full-size" Text="Start Camera" />
                </td>
                <td>
                    <div class="input-control file full-size" data-role="input">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <button class="button"><span class="mif-folder"></span></button>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                                <input type="button" id="btnCapture" value="Capture Photo" class="button primary full-size" />

<%--                    <asp:Button runat="server" ID="btnCapture" ClientIDMode="Static" CssClass="button primary full-size" Text="Capture Photo" />--%>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnSubmitPhoto" CssClass="button primary full-size" Text="Submit Photo" OnClick="btnSubmitPhoto_Click" />
                </td>
            </tr>
        </table>

        <hr />
        <h4>For Office Use</h4>
        <hr />
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Office File Reference" ID="txtFileReference"></asp:TextBox>
        </div>
        <div class="input-control select full-size">
            <asp:DropDownList runat="server" ID="cmbBenefStatus">
                <asp:ListItem>Select One</asp:ListItem>
                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                <asp:ListItem Value="Approved">Approved</asp:ListItem>
                <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                <asp:ListItem Value="Suspended">Suspended</asp:ListItem>
            </asp:DropDownList>
        </div>
        <hr />
        <asp:Button runat="server" ID="btnAddBeneficiary" CssClass="button primary full-size" Text="Save" OnClick="btnAddBeneficiary_Click" />
        <hr />
    </div>
    <%--    <script type='text/javascript'>
        $(document).ready(function () {
            $("#txtCNIC").mask("99999-9999999-9");
        });

    </script>--%>
    <script src="WebCam.js"></script>
    <script type="text/javascript">
        $(function () {
            Webcam.set({
                width: 100,
                height: 150,
                image_format: 'jpeg',
                jpeg_quality: 90
            });
            Webcam.attach('#webcam');
            $("#btnCapture").click(function () {
                Webcam.snap(function (data_uri) {
                    $("#myImage")[0].src = data_uri;
                    //$("#btnUpload").removeAttr("disabled");
                });
            });
            $("#btnUpload").click(function () {
                $.ajax({
                    type: "POST",
                    url: "CS.aspx/SaveCapturedImage",
                    data: "{data: '" + $("#myImage")[0].src + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) { }
                });
            });
        });
    </script>
</asp:Content>
