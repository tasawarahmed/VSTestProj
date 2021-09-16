<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnauthorizedAccess.aspx.cs" Inherits="IttefaqConstructionServices.UnauthorizedAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Metro-UI-CSS-master/build/css/metro.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-colors.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-icons.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-responsive.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-rtl.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-schemes.css" rel="stylesheet" />
    <script src="Metro-UI-CSS-master/test/RequireJS/scripts/jquery.js"></script>
    <script src="Metro-UI-CSS-master/test/RequireJS/scripts/metro.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContents" runat="server">
    <h1>You are not authorized to view this page.</h1>
</asp:Content>
