
$(function () {

  var options = {
          series: [{
            name: "Bookings",
            data: [10, 41, 35, 51, 49, 62, 69, 91, 48, 56, 98]
        }],
          chart: {
          height: 250,
          type: 'line',
          zoom: {
            enabled: false
          }
        },
        dataLabels: {
          enabled: false
        },
        stroke: {
          curve: 'smooth'
        },
        
        grid: {
          row: {
            colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
            opacity: 0.5
          },
        },
        xaxis: {
          categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
        }
        };

        var chart = new ApexCharts(document.querySelector("#line-chart"), options);
        chart.render();


        var options = {
          series: [{
          name: "Bookings",
          data: [400, 430, 448, 470, 540, 580, 690, 1100, 1200, 1380]
        }],
          chart: {
          type: 'bar',
          height: 250
        },
        plotOptions: {
          bar: {
            borderRadius: 4,
            borderRadiusApplication: 'end',
            
          }
        },
        dataLabels: {
          enabled: false
        },
        xaxis: {
          categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'July',
            'Aug', 'Sep', 'Oct', 'Nov', 'Dec'
          ],
        }
        };

        var chart = new ApexCharts(document.querySelector("#bar-chart"), options);
        chart.render();

   
  });

      
  