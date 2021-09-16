<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleCharts.aspx.cs" Inherits="IttefaqConstructionServices.GoogleCharts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js" integrity="sha256-9/aliU8dGd2tb6OSsuzixeV4y/faTqgFtohetphbbj0=" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
<%--    <script type="text/javascript" src="https://www.google.com/jsapi?autoload={'modules':[{'name':'visualization','version':'1.1','packages':['corechart']}]}"></script>--%>
        <script>
        var timeout;
        var chartDataSuppLahore; //global variable for holding chart data
        var chartDataSuppKarachi; //global variable for holding chart data
        var chartDataSubContLahore; //global variable for holding chart data
        var chartDataPersonalLedger; //global variable for holding chart data
        //google.load("visualization", "1", { packages: ["corechart"] });
        google.charts.load('current', { 'packages': ['corechart'] });

        //Here we will fill data
        $(document).ready(function () {
            timeout = setInterval(function () {
                if (google.visualization != undefined) {
                    drawSuppliersLahore();
                    drawSuppliersKarachi();
                    drawSubcontractorsLahore();
                    drawPersonalLedgers();
                    clearInterval(timeout);
                }
            }, 300);
        });

        function drawPersonalLedgers() {
            $.ajax({
                url: "GoogleCharts.aspx/getPersonalLedgers",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    chartDataPersonalLedger = data.d;
                },
                error: function () {
                    alert("Error loading data: Please try again");
                }
            }).done(function () {
                drawChart();
            });
        }

        function drawSubcontractorsLahore() {
            $.ajax({
                url: "GoogleCharts.aspx/getSubContractorsLahore",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    chartDataSubContLahore = data.d;
                },
                error: function () {
                    alert("Error loading data: Please try again");
                }
            }).done(function () {
                drawChart();
            });
        }

        function drawSuppliersKarachi() {
            $.ajax({
                url: "GoogleCharts.aspx/getSuppliersKarachi",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    chartDataSuppKarachi = data.d;
                },
                error: function () {
                    alert("Error loading data: Please try again");
                }
            }).done(function () {
                drawChart();
            });
        }

        function drawSuppliersLahore() {
            $.ajax({
                url: "GoogleCharts.aspx/getSuppliersLahore",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    chartDataSuppLahore = data.d;
                    console.log(data);
                },
                error: function () {
                    alert("Error loading data: Please try again");
                }
            }).done(function () {
                drawChart();
            });
        }

        function drawChart() {
            var dataSuppLahore = google.visualization.arrayToDataTable(chartDataSuppLahore);
            var dataSuppKarachi = google.visualization.arrayToDataTable(chartDataSuppKarachi);
            var dataSubContKarachi = google.visualization.arrayToDataTable(chartDataSubContLahore);
            var dataPersonalLedgers = google.visualization.arrayToDataTable(chartDataPersonalLedger);

            var optionsLahore = {
                title: "Suppliers-Lahore",
                pointSize: 10
            };

            var optionsKarachi = {
                title: "Suppliers-Karachi",
                pointSize: 10
            };

            var optionsSubContLahore = {
                title: "SubContractors-Lahore",
                pointSize: 10
            };

            var optionsPersonalLedgers = {
                title: "Personal Ledgers",
                pointSize: 10
            };

            var barChartLahore = new google.visualization.BarChart(document.getElementById('chart_suppLahore'));
            barChartLahore.draw(dataSuppLahore, optionsLahore);

            var barChartKarachi = new google.visualization.BarChart(document.getElementById('chart_suppKarachi'));
            barChartKarachi.draw(dataSuppKarachi, optionsKarachi);

            var barChartSubConLahore = new google.visualization.BarChart(document.getElementById('chart_subContLahore'));
            barChartSubConLahore.draw(dataSubContKarachi, optionsSubContLahore);

            var barChartPersonalLedgers = new google.visualization.BarChart(document.getElementById('chart_personalLedgers'));
            barChartPersonalLedgers.draw(dataPersonalLedgers, optionsPersonalLedgers);

            //var pieChartLahore = new google.visualization.PieChart(document.getElementById('chart_suppLahore'));
            //pieChart.draw(dataSuppLahore, optionsLahore);

            //var pieChartKarachi = new google.visualization.PieChart(document.getElementById('chart_suppKarachi'));
            //pieChartKarachi.draw(dataSuppKarachi, optionsKarachi);

            //var pieChartSubConLahore = new google.visualization.PieChart(document.getElementById('chart_subContLahore'));
            //pieChartSubConLahore.draw(dataSubContKarachi, optionsSubContLahore);

            //var pieChartPersonalLedgers = new google.visualization.PieChart(document.getElementById('chart_personalLedgers'));
            //pieChartPersonalLedgers.draw(dataPersonalLedgers, optionsPersonalLedgers);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td style="width:50%">
                    <div id="chart_suppLahore">
                    </div>
                </td>
                <td style="width:50%">
                    <div id="chart_suppKarachi">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="chart_subContLahore">
                    </div>
                </td>
                <td>
                    <div id="chart_personalLedgers">
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
