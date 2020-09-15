// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

var ctx = document.getElementById("myPieChart");
var myvalues;
var encodedUri;
function createChart(labelsForGraph, values) {

    var myPieChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labelsForGraph,
            datasets: [{
                data: values,
                backgroundColor: ['#7341E6', '#C5ABFF'],
                hoverBackgroundColor: ['#B762FB', '#E3BCFF'],
                hoverBorderColor: "rgba(234, 236, 244, 1)",
            }],
        },
        options: {
            maintainAspectRatio: false,
            tooltips: {
                backgroundColor: "rgb(255,255,255)",
                bodyFontColor: "#858796",
                borderColor: '#dddfeb',
                borderWidth: 1,
                xPadding: 15,
                yPadding: 15,
                displayColors: false,
                caretPadding: 10,
            },
            legend: {
                display: true
            },
            cutoutPercentage: 80,
        },
    });
    myPieChart.render();

    document.getElementById("downloadCSV").addEventListener("click", function () {
        downloadCSV()
    });
    myvalues = values;
    //console.log(myvalues[2]);
    var reg = values[0];
    var notreg = values[1];

    const rows = [
        ["Registered", reg],
        ["Not registered", notreg]
    ];

    let csvContent = "data:text/csv;charset=utf-8,";

    rows.forEach(function (rowArray) {
        let row = rowArray.join(",");
        csvContent += row + "\r\n";
    });

    encodedUri = encodeURI(csvContent);
   
}

function downloadCSV() {

    window.open(encodedUri);
}

