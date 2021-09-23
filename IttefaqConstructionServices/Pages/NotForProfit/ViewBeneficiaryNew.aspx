<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/NotForProfit/NotForProfit.master" AutoEventWireup="true" CodeBehind="ViewBeneficiaryNew.aspx.cs" Inherits="IttefaqConstructionServices.Pages.NotForProfit.ViewBeneficiaryNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Search and View Beneficiaries:&nbsp<small>Search Beneficiries Here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan6">
        <h4>Search</h4>
        <hr />
        <asp:GridView runat="server" ID="gridPhotos" AutoGenerateColumns="False" CssClass="table striped hovered border bordered" data-role="datatable" data-seraching="false" OnRowCommand="gridPhotos_RowCommand">
            <Columns>
                <%--                <asp:TemplateField HeaderText="Photos">
                    <ItemTemplate>
                        <img id="dbImage" runat="server" src="data:image/jpeg; base64, " alt="Photo" width="50" height="50" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
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
                        <asp:LinkButton runat="server" ID="lblName" Text='<%# Bind("Name") %>' CommandName="view" CommandArgument='<%# Bind("id") %>'></asp:LinkButton>
                        <%--                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="CNIC Number">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("cnic") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCNIC" runat="server" Text='<%# Bind("cnic") %>'></asp:Label>
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
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>

    </div>
    <div class="cell colspan5">
        <h4>Registration Details</h4>
        <hr />
        <asp:Panel ID="pnlDetails" runat="server" Visible="false">
            <div id="pDiv1">
                <h1><%=Session["companyName"].ToString() %></h1>
                <h2>Beneficiary Registration Form</h2>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 85%">
                            <h3><%= beneficiary.benefName %></h3>
                            <h4><%= beneficiary.address %></h4>
                            <h4>Phone: <%= beneficiary.contact %> &nbsp CNIC: <%= beneficiary.cnic %></h4>
                            <h4><%= beneficiary.city %></h4>
                            <h4><%= beneficiary.primeDisability %></h4>
                        </td>
                        <td style="width: 15%">
                            <div class="image-container rounded">
                                <div class="frame">
                                    <img id="myImage" runat="server" src="data:image/jpeg; base64, " alt="Photo" width="125" height="200" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 25%">
                            <strong>Date of Birth:</strong>

                        </td>
                        <td style="width: 25%">
                            <%= dob %>
                        </td>
                        <td style="width: 25%">
                            <strong>Education:</strong>

                        </td>
                        <td style="width: 25%">
                            <%= beneficiary.education %>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <strong>Monthly Income:</strong>

                        </td>
                        <td style="width: 25%">
                            <%= income %>
                        </td>
                        <td style="width: 25%">
                            <strong>Income Source:</strong>
                        </td>
                        <td style="width: 25%">
                            <%= beneficiary.incomeSource %>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <strong>House Status:</strong>

                        </td>
                        <td style="width: 25%">
                            <%= houseStatus %>
                        </td>
                        <td style="width: 25%">
                            <strong>Family Members:</strong>

                        </td>
                        <td style="width: 25%">
                            <%= beneficiary.totalFamily %>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 25%">
                            <strong>Special Issues:</strong>
                        </td>
                        <td style="width: 75%">
                            <%= beneficiary.specialIssue %>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%">
                            <strong>Remarks:</strong>
                        </td>
                        <td style="width: 75%">
                            <%= beneficiary.remarks %>
                        </td>
                    </tr>
                </table>
                <asp:Panel runat="server" ID="pnlFamilyDetails" Visible="false">
                    <h3>Family Details:</h3>
                    <asp:GridView runat="server" ID="gridFamily" AutoGenerateColumns="False" CssClass="table striped hovered border bordered">
                        <Columns>
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
                                <ItemStyle HorizontalAlign="Right" />
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
                </asp:Panel>

            </div>
            <asp:Button runat="server" ID="Button1" CssClass="button primary full-size" Text="Print Beneficiary Form" OnClientClick="printDiv('pDiv1')" />
        </asp:Panel>
        <h4>Benefits Details</h4>
        <hr />
        <asp:Panel runat="server" ID="pnlBenefits" Visible ="false">
                    <asp:GridView runat="server" ID="gridBenefits" AutoGenerateColumns="False" CssClass="table striped hovered border bordered">
                        <Columns>
                            <asp:TemplateField HeaderText="Event Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("event") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("event") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("date") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAge" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Monetary Benefits">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("money") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRelationship" runat="server" Text='<%# Bind("money") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Other Benefits">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("others") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEducation" runat="server" Text='<%# Bind("others") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
        </asp:Panel>


    </div>
</asp:Content>
