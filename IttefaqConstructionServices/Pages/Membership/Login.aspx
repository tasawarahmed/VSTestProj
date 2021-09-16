<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IttefaqConstructionServices.Pages.Membership.Login" %>

<!DOCTYPE HTML>
<!-- saved from url=(0043)https://metroui.org.ua/templates/login.html -->
<!DOCTYPE html PUBLIC "" "">
<html>
<head>
    <meta content="IE=11.0000" http-equiv="X-UA-Compatible">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="description" content="Metro, a sleek, intuitive, and powerful framework for faster and easier web development for Windows Metro Style.">
    <meta name="keywords" content="HTML, CSS, JS, JavaScript, framework, metro, front-end, frontend, web development">
    <meta name="author" content="Sergey Pimenov and Metro UI CSS contributors">
    <link href="../favicon.ico" rel="shortcut icon" type="image/x-icon">
    <title>Login form</title>
    <script src="../../CSS/jquery.js"></script>
    <script src="../../CSS/jquery-2.1.3.min.js"></script>
    <script src="../../CSS/metro.js"></script>
    <link href="../../CSS/metro.css" rel="stylesheet" />
    <link href="../../CSS/metro-icons.css" rel="stylesheet" />
    <link href="../../CSS/metro-responsive.css" rel="stylesheet" />

    <style>
        .login-form {
            width: 25rem;
            height: 22.75rem;
            position: fixed;
            top: 50%;
            margin-top: -11.375rem;
            left: 50%;
            margin-left: -12.5rem;
            background-color: #ffffff;
            opacity: 0;
            -webkit-transform: scale(.8);
            transform: scale(.8);
        }
    </style>

    <script>

        /*
        * Do not use this is a google analytics fro Metro UI CSS
        * */
        if (window.location.hostname !== 'localhost') {

            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
                    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

            ga('create', 'UA-58849249-3', 'auto');
            ga('send', 'pageview');

        }


        $(function () {
            var form = $(".login-form");

            form.css({
                opacity: 1,
                "-webkit-transform": "scale(1)",
                "transform": "scale(1)",
                "-webkit-transition": ".5s",
                "transition": ".5s"
            });
        });
    </script>

    <meta name="GENERATOR" content="MSHTML 11.00.9600.18739">
</head>
<body class="bg-darkTeal">
    <div class="login-form padding20 block-shadow">
        <form id="form1" runat="server">
            <h1 class="text-light">Login</h1>
            <hr>
            <br>
            <div class="input-control text full-size" data-role="input">
                <label for="user_login">
                    User 
name:</label>
                <asp:TextBox runat="server" ID="userName"></asp:TextBox>
<%--                <input name="user_login" id="user_login" type="text">--%>
                <button class="button helper-button clear"><span class="mif-cross"></span></button>
            </div>
            <br>
            <br>
            <div class="input-control password full-size" data-role="input">
                <label for="user_password">
                    User 
password:</label>
                <asp:TextBox runat="server" ID="password" TextMode="Password"></asp:TextBox>
<%--                <input name="user_password" id="user_password" type="password">--%>
                <button class="button helper-button reveal">
                    <span
                        class="mif-looks"></span>
                </button>
            </div>
            <br>
            <br>
            <asp:Button runat="server" ID="btnLogin" CssClass="button primary full-size" Text="Login" OnClick="btnLogin_Click" />
            <asp:Button runat="server" ID="btnCancel" CssClass="button danger full-size" Text="Cancel" OnClick="btnCancel_Click" />
            <asp:Label runat="server" ID="lblResults"></asp:Label>
        </form>
    </div>
</body>
</html>
