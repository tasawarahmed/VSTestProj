<%@ Page Culture="ur-PK" Language="C#" AutoEventWireup="true" CodeBehind="Test2.aspx.cs" Inherits="IttefaqConstructionServices.Test2" %>

<!doctype html>
<html lang="en">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">

    <title>Hello, world!</title>
</head>
<body>

    <!-- Optional JavaScript; choose one of the two! -->

    <!-- Option 1: jQuery and Bootstrap Bundle (includes Popper) -->
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>

    <!-- Option 2: jQuery, Popper.js, and Bootstrap JS
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js" integrity="sha384-9/reFTGAW83EW2RDu2S0VKaIzap3H66lZH81PoYlFhbGU+6BZp6G7niu735Sk7lN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.min.js" integrity="sha384-w1Q4orYjBQndcko6MimVbzY0tgp4pWB4lZ7lr30WKz0vr/aWKhXdBNmNb5D92v7s" crossorigin="anonymous"></script>
    -->
    <div class="row row-cols-2 row-cols-md-4">
        <div class="col mb-4">
            <div class="card text-black bg-primary mb-3 shadow">
                <div class="card-header">Sites </div>
                <div class="card-body bg-white">
                    <h5 class="card-title">Customers' Advances:</h5>
<%--                    <p class="card-text text-right">Rs. <%= advancesFromCustomers.ToString("n", CultureInfo.CurrentCulture) %></p>--%>
                    <p class="card-text text-right"><%=aFC %></p>
                    <h5 class="card-title">Receivables:</h5>
                    <p class="card-text text-right"><%=rFS %></p>
                    <a href="#" class="btn btn-primary btn-block">Details >></a>

                </div>
            </div>
        </div>
        <div class="col mb-4">
            <div class="card text-black bg-secondary mb-3 shadow">
                <div class="card-header">PMs and Site Liabilities</div>
                <div class="card-body bg-white">
                    <h5 class="card-title">Advances to PMs:</h5>
                    <p class="card-text text-right">8,211,125</p>
                    <h5 class="card-title">Site Liabilities:</h5>
                    <p class="card-text text-right">99,504,611</p>
                    <a href="#" class="btn btn-secondary btn-block">Details >></a>
                </div>
            </div>
        </div>
        <div class="col mb-4">
            <div class="card text-black bg-success mb-3 shadow">
                <div class="card-header">Suppliers</div>
                <div class="card-body bg-white">
                    <h5 class="card-title">Advances:</h5>
                    <p class="card-text text-right">82,359,459</p>
                    <h5 class="card-title">Receivables:</h5>
                    <p class="card-text text-right">102,450,611</p>
                    <a href="#" class="btn btn-success btn-block">Details >></a>
                </div>
            </div>
        </div>
        <div class="col mb-4">
            <div class="card text-black bg-danger mb-3 shadow">
                <div class="card-header">Personal Ledgers</div>
                <div class="card-body bg-white">
                    <h5 class="card-title">Advances:</h5>
                    <p class="card-text text-right">82,359,459</p>
                    <h5 class="card-title">Receivables:</h5>
                    <p class="card-text text-right">102,450,611</p>
                    <a href="#" class="btn btn-danger btn-block">Details >></a>
                </div>
            </div>
        </div>
        <div class="col mb-4">
            <div class="card text-black bg-warning mb-3 shadow">
                <div class="card-header">Cash & Bank</div>
                <div class="card-body bg-white">
                    <h5 class="card-title">Cash in Hand:</h5>
                    <p class="card-text text-right">82,359,459</p>
                    <h5 class="card-title">Cash at Bank:</h5>
                    <p class="card-text text-right">102,450,611</p>
                    <a href="#" class="btn btn-warning btn-block">Details >></a>
                </div>
            </div>
        </div>
        <div class="col mb-4">
            <div class="card text-black bg-info mb-3 shadow">
                <div class="card-header">Income & Expenses</div>
                <div class="card-body bg-white">
                    <h5 class="card-title">Incomes:</h5>
                    <p class="card-text text-right">82,359,459</p>
                    <h5 class="card-title">Expenses:</h5>
                    <p class="card-text text-right">102,450,611</p>
                    <a href="#" class="btn btn-info btn-block">Details >></a>
                </div>
            </div>
        </div>
        <div class="col mb-4">
            <div class="card text-warning bg-dark mb-3 shadow">
                <div class="card-header">T&P & Store</div>
                <div class="card-body bg-white">
                    <h5 class="card-title">Total T&P</h5>
                    <p class="card-text text-right">82,359,459</p>
                    <h5 class="card-title">Total Store</h5>
                    <p class="card-text text-right">82,359,459</p>
                    <a href="#" class="btn btn-dark btn-block">Details >></a>
                </div>
            </div>
        </div>
        <div class="col mb-4">
            <div class="card text-black bg-black-50 mb-3 shadow">
                <div class="card-header">Assets & Capital Accounts</div>
                <div class="card-body bg-white">
                    <h5 class="card-title">Total Assets:</h5>
                    <p class="card-text text-right">82,359,459</p>
                    <h5 class="card-title">Total Capital:</h5>
                    <p class="card-text text-right">82,359,459</p>
                    <a href="#" class="btn btn-info btn-block">Details >></a>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
