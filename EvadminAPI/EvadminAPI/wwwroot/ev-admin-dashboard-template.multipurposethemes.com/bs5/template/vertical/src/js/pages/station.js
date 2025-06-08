
$(function () {

       


        var options = {
          series: [{
            name: "Usage",
          data: [10, 25, 35 ]
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
          categories: ['< 3hrs', '3-6 hrs', '> 6 hrs'
          ],
        }

        };

        var chart = new ApexCharts(document.querySelector("#bar-chart"), options);
        chart.render();
        
        
    });





var options = {
    chart: {
      height: 149,
      type: "area",
      toolbar: {
            show: false
          }
    },
    dataLabels: {
      enabled: false

    },

    stroke: {
      curve: "smooth"
    },
    series: [{
      name: "Revenue",
      data: [11, 32, 45, 32, 34, 11, 32, 41, 15, 25, 19, 45]
    }],
    colors: ["#4d7cff"],
    xaxis: {
      
      categories: ['Jan' , 'Feb' , 'Mar' , 'Apr' , 'Jun' , 'Jul' ,
        'Aug' , 'Sep' , 'Oct' , 'Nov' , 'Dec'
      ],
    },
    tooltip: {
      x: {
        format: "dd/MM/yy HH:mm"
      },
    }
  }
  var chart = new ApexCharts(
    document.querySelector("#apexcharts-area"),
    options
  );
  chart.render();

         