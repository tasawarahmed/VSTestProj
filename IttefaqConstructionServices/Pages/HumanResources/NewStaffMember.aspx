<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/HumanResources/HumanResourcesMaster.master" AutoEventWireup="true" CodeBehind="NewStaffMember.aspx.cs" Inherits="IttefaqConstructionServices.Pages.HumanResources.NewStaffMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>New Staff Member:&nbsp<small>Add new staff member here.</small></h1>
    <hr />
    <asp:Label runat="server" ID="lblSuccess" Text="Label Sucess" CssClass="tag success" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblWarning" Text="Label Warning" CssClass="tag alert" Visible="false"></asp:Label>
    <br />

    <div class="cell colspan5">
        <h3>New Staff Member</h3>
        <hr />
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Staff Member" ID="txtStaffName"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Staff Father Name" ID="txtStaffFather"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Salary of Staff Member" ID="txtSalary"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Address of Staff Member" ID="txtStaffAddress"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="ID Card Number" ID="txtStaffIDCard"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Contact Number of Staff" ID="txtStaffContact"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Email of Staff" ID="txtStaffEmail"></asp:TextBox>
        </div>
        <hr />
        <asp:Button runat="server" ID="btnCreate" CssClass="button primary full-size" Text="OK" OnClick="btnCreate_Click" />
        <hr />
    </div>
    <div class="cell colspan5">
        <h3>Update Staff Member</h3>
        <hr />
        <div class="input-control select full-size">
            <asp:DropDownList runat="server"  ID="cmbStaffMember" AutoPostBack="true" OnSelectedIndexChanged="cmbStaffMember_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Staff Member" ID="txtUpdateStaffName"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Name of Staff Father Name" ID="txtUpdateFather"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Salary of Staff Member" ID="txtUpdateSalary"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Address of Staff Member" ID="txtUpdateAddress"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="ID Card Number" ID="txtUpdateIDCard"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Contact Number of Staff" ID="txtUpdateContact"></asp:TextBox>
        </div>
        <div class="input-control text full-size">
            <asp:TextBox runat="server" placeholder="Email of Staff" ID="txtUpdateEmail"></asp:TextBox>
        </div>
        <hr />
        <asp:Button runat="server" ID="btnUpdate" CssClass="button primary full-size" Text="OK" OnClick="btnUpdate_Click" />
        <asp:SqlDataSource runat="server" ID="my" ConnectionString="<%$ ConnectionStrings:ICSConnectionString %>" SelectCommand="SELECT staffName + ' - ' + staffFather AS Staff, id FROM tblGenStaff" ></asp:SqlDataSource>
        <hr />
    </div>
</asp:Content>
