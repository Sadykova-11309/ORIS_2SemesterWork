
$(function () {

  

  //     new Chartist.Pie('.ct-gauge-chart', {
  //   series: [110, 90, 25]
  // }, {
  //   donut: true,
  //   donutWidth: 100,
  //   startAngle: 270,
  //   total: 450,
  //   low:0,
  //   showLabel: false

  // });

      




      var options = {
          series: [{
          name: 'Energy Usage',
          data: [54, 65, 23, 83, 65, 100]
        }],
          chart: {
          type: 'bar',
          height: 288
        },
        plotOptions: {
          bar: {
            borderRadius: 6,
            borderRadiusApplication: 'end',
            horizontal: true,
          }
        },
        colors: ["#0052cc"],
        dataLabels: {
          enabled: false
        },
        xaxis: {
          
        }
        };

        var chart = new ApexCharts(document.querySelector("#bar-chart"), options);
        chart.render();



        var options = {
          series: [{
          name: 'Energy Usage',
          data: [60, 65, 23, 45, 65, 10]
        }],
          chart: {
          type: 'bar',
          height: 288
        },
        plotOptions: {
          bar: {
            borderRadius: 6,
            borderRadiusApplication: 'end',
            horizontal: true,
          }
        },
        colors: ["#0052cc"],
        dataLabels: {
          enabled: false
        },
        xaxis: {
          
        }
        };

        var chart = new ApexCharts(document.querySelector("#bar-chart2"), options);
        chart.render();





        var options = {
          series: [{
          name: 'Energy Usage',
          data: [100, 65, 23, 53, 48, 30]
        }],
          chart: {
          type: 'bar',
          height: 288
        },
        plotOptions: {
          bar: {
            borderRadius: 6,
            borderRadiusApplication: 'end',
            horizontal: true,
          }
        },
        colors: ["#0052cc"],
        dataLabels: {
          enabled: false
        },
        xaxis: {
          
        }
        };

        var chart = new ApexCharts(document.querySelector("#bar-chart3"), options);
        chart.render();



        var options = {
          series: [110, 90, 25],
          chart: {
          width: 380,
          type: 'pie',
        },
        labels: ['Available', 'In Use', 'Unavailable'],
        responsive: [{
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: 'bottom'
            }

          }
        }]
        };

        var chart = new ApexCharts(document.querySelector("#pie-chart"), options);
        chart.render();






        var options = {
          series: [{
            name: 'Reality Sales',
            data: [400, 448, 540, 1200, 1380]
          }],
          chart: {
          height: 200,
          type: 'bar',
          toolbar: {
            show: false
          },
        },
        plotOptions: {
          bar: {
            borderRadius: 10,  
            horizontal:true,
            dataLabels: {
              position: 'top', // top, center, bottom
            },
          }
        },
        colors: ["#0d6efd" , "#FFAA05"],
        stroke: {
          show: true,
          width: 30,
          colors: ['transparent']
        },
        dataLabels: {
          enabled: false,
          formatter: function (val) {
            return val + "%";
          },
          offsetY: -20,
          style: {
            fontSize: '12px',
            colors: ["#304758" , "#FFAA05"]
          }
        },
        legend: {
            show: false,
        },
        grid: {
            show: false,
          },
        xaxis: {
          categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep"],
          position: 'bottom',
          labels: {
            show: true,
          },
          axisBorder: {
            show: false
          },
          axisTicks: {
            show: false
          },
          crosshairs: {
            fill: {
              type: 'gradient',
              gradient: {
                colorFrom: '#D8E3F0',
                colorTo: '#BED1E6',
                stops: [0, 100],
                opacityFrom: 0.4,
                opacityTo: 0.5,
              }
            }
          },
          tooltip: {
            enabled: false,
          }
        },
        yaxis: {
          axisBorder: {
            show: false
          },
          axisTicks: {
            show: false,
          },
          labels: {
            show: false,
            formatter: function (val) {
              return val + "%";
            }
          }
        
        },
        title: {
          text: '',
          floating: false,
          offsetY: 330,
          align: 'center',
          style: {
            color: '#444'
          }
        }
        };

        var chart = new ApexCharts(document.querySelector("#Reality-chart"), options);
        chart.render();

        




// var options = {
//           series: [76],
//           chart: {
//           type: 'radialBar',
//           offsetY: -20,
//           sparkline: {
//             enabled: true
//           }
//         },
//         plotOptions: {
//           radialBar: {
//             startAngle: -90,
//             endAngle: 90,
//             track: {
//               background: "#e7e7e7",
//               strokeWidth: '97%',
//               margin: 5, // margin is in pixels
//               dropShadow: {
//                 enabled: true,
//                 top: 5,
//                 left: 0,
//                 color: '#999',
//                 opacity: 1,
//                 blur: 2
//               }
//             },
//             dataLabels: {
//               name: {
//                 show: false
//               },
//               value: {
//                 offsetY: -20,
//                 fontSize: '22px'
//               }
//             }
//           }
//         },
//         grid: {
//           padding: {
//             top: -10
//           }
//         },
//         fill: {
//           type: 'gradient',
//           gradient: {
//             shade: 'light',
//             shadeIntensity: 0.4,
//             inverseColors: false,
//             opacityFrom: 1,
//             opacityTo: 1,
//             stops: [0, 50, 53, 91]
//           },
//         },
//         labels: ['Average Results'],
//         };

//         var chart = new ApexCharts(document.querySelector("#chart"), options);
//         chart.render();





    var analyticsBarChartOptions = {
    chart: {
      height: 268,
      type: 'bar',
      toolbar: {
      show: false
      }
    },
    plotOptions: {
      bar: {
      horizontal: false,
      columnWidth: '45%',
      borderRadius: 3
      },
    },
    dataLabels: {
      enabled: false
    },
    colors: ['#7047ee'],
    series: [{
      name: 'Cost',
      data: [5, 10, 14 , 20, 8]
    }],
    xaxis: {
      categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May'],
      axisBorder: {
      show: false
      },
      axisTicks: {
      show: false
      },
      labels: {
      style: {
        colors: '#333333'
      }
      }
    },
    yaxis: {

      min: 0,
      max: 20,
      tickAmount: 3,
      labels: {
      style: {
        color: '#333333'
      }
      }
    },
    legend: {
      show: false, 
    },
    tooltip: {
      y: {
      formatter: function (val) {
        return "$ " + val + " thousands"
      }
      }
    }
    }

    var analyticsBarChart = new ApexCharts(
    document.querySelector("#staff_turnover"),
    analyticsBarChartOptions
    );

    analyticsBarChart.render();
   
      

  
}); // End of use strict


Morris.Donut({
        element: 'donut-chart',
        data: [{
            label: "Available",
            value: 145,

        }, {
            label: "In Use",
            value: 65
        }, {
            label: "Unavailable",
            value: 15
        }],
        resize: true,
        colors:['#51ce8a', '#4d7cff', '#f2426d']
    });
