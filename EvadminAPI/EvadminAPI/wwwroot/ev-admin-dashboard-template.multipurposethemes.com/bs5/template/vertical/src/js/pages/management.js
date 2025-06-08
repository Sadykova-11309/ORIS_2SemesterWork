
$(function () {

       


  var options = {
          series: [1900, 70, 30],
      labels: ['Public [1900]', 'Private [70]', 'Personal [30]'],
          chart: {
          width:328,
          
          type: 'donut',
        },
        dataLabels: {
          enabled: false
        },
        responsive: [{
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              show: false
            }
          }
        }],
    colors:['#04a08b', '#0000FF', '#FFA500'],
        legend: {
          position: 'left',
          height: 250,
        }
        };

        var chart = new ApexCharts(document.querySelector("#charts_widget_2_chart"), options);
        chart.render();



        var options = {
          series: [1900, 500, 350, 250],
      labels: ['Dallas [1900]', 'Texas [500]', 'Tokyo [350]', 'Others [250]'],
          chart: {
          width:328,
          
          type: 'donut',
        },
        dataLabels: {
          enabled: false
        },
        responsive: [{
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              show: false
            }
          }
        }],
    colors:['#008000', '#ff5733', '#6ff76b', '#ca2e3a'],
        legend: {
          position: 'left',
          height: 250,
        }
        };

        var chart = new ApexCharts(document.querySelector("#charts_widget_2_chart2"), options);
        chart.render();
  
        
    });

var options = {
    chart: {
      height: 250,
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
      name: "Sessions",
      data: [11, 32, 45, 32, 34, 52, 41]
    }],
    colors: ["#4d7cff"],
    xaxis: {
      type: "datetime",
      categories: ["2024-09-19T00:00:00", "2024-09-19T01:30:00", "2024-09-19T02:30:00", "2024-09-19T03:30:00", "2024-09-19T04:30:00", "2024-09-19T05:30:00",
        "2024-09-19T06:30:00"
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



 var options = {
          series: [{
            name: "Revenue",
          data: [1124, 2548, 3265, 1458, 2569, 2365, 2369, 3658, 4582, 1478, 2369, 2589 ]
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
          categories: ['April', 'May', 'June', 'July', 'August', 'September', 'October',
            'November', 'December', 'January', 'February', 'March'
          ],
        }

        };

        var chart = new ApexCharts(document.querySelector("#bar-chart"), options);
        chart.render();
      
  

  var options = {
          series: [{
            name: "Energy Usage",
            data: [10, 41, 35, 51, 49, 62, 69, 91, 148]
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
          curve: 'straight'
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
          series: [48.7, 38.9, 12.4],
          chart: {
          width: 424,
          type: 'pie',
        },
        labels: ['Public [48.7%]', 'Private [38.9%]', 'Personal  [12.4%]'],
        responsive: [{
          breakpoint: 480,
          options: {
            chart: {
              width: 300
            },
            legend: {
              position: 'bottom'
            }
          }
        }]
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();





        //  var options = {
        //    series: [18.3, 37.5, 29.3, 15.0],
        //  chart: {
        //    width: 420,
        //   type: 'pie',
        //  },
        //  labels: [ 'Dallas [18.3%]', 'Texas [37.5%]', 'Tokyo [29.3%]', 'Others [15.00%]'],
        //  responsive: [{
        //   breakpoint: 480,
        //    options: {
        //     chart: {
        //      width: 300
        //      },
        //     legend: {
        //        position: 'bottom'
        //      }
        //    }
        // }]
        // };

        // var chart = new ApexCharts(document.querySelector("#chart2"), options);
        //  chart.render();



var basicpieChart = echarts.init(document.getElementById('basic-pie'));
        var option = {
            // Add title
                

                // Add tooltip
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },

                // Add legend
                legend: {
                    orient: 'vertical',
                    x: 'left',
                    data: ['Public', 'Private', 'Personal']
                },

                // Add custom colors
                color: ['#689f38', '#38649f', '#389f99'],

                // Display toolbox
                toolbox: {
                    show: true,
                    orient: 'vertical',
                    feature: {
                        mark: {
                            show: true,
                            title: {
                                mark: 'Markline switch',
                                markUndo: 'Undo markline',
                                markClear: 'Clear markline'
                            }
                        },
                        dataView: {
                            show: true,
                            readOnly: false,
                            title: 'View data',
                            lang: ['View chart data', 'Close', 'Update']
                        },
                        magicType: {
                            show: true,
                            title: {
                                pie: 'Switch to pies',
                                funnel: 'Switch to funnel',
                            },
                            type: ['pie', 'funnel'],
                            option: {
                                funnel: {
                                    x: '25%',
                                    y: '20%',
                                    width: '50%',
                                    height: '70%',
                                    funnelAlign: 'left',
                                    max: 1548
                                }
                            }
                        },
                        restore: {
                            show: true,
                            title: 'Restore'
                        },
                        saveAsImage: {
                            show: true,
                            title: 'Same as image',
                            lang: ['Save']
                        }
                    }
                },

                // Enable drag recalculate
                calculable: true,

                // Add series
                series: [{
                    name: 'Charge Points',
                    type: 'pie',
                    radius: '70%',
                    center: ['50%', '57.5%'],
                    data: [
                        {value: 50, name: 'Public'},
                        {value: 30, name: 'Private'},
                        {value: 20, name: 'Personal'}
                    ]
                }]
        };
    
    basicpieChart.setOption(option);


    var basicpieChart = echarts.init(document.getElementById('basic-pie2'));
        var option = {
            // Add title
                

                // Add tooltip
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },

                // Add legend
                legend: {
                    orient: 'vertical',
                    x: 'left',
                    data: ['Dallas', 'Texas', 'Tokyo', 'Others']
                },

                // Add custom colors
                color: ['#057637', '#cd2105', '#1145f7', '#fa950c'],

                // Display toolbox
                toolbox: {
                    show: true,
                    orient: 'vertical',
                    feature: {
                        mark: {
                            show: true,
                            title: {
                                mark: 'Markline switch',
                                markUndo: 'Undo markline',
                                markClear: 'Clear markline'
                            }
                        },
                        dataView: {
                            show: true,
                            readOnly: false,
                            title: 'View data',
                            lang: ['View chart data', 'Close', 'Update']
                        },
                        magicType: {
                            show: true,
                            title: {
                                pie: 'Switch to pies',
                                funnel: 'Switch to funnel',
                            },
                            type: ['pie', 'funnel'],
                            option: {
                                funnel: {
                                    x: '25%',
                                    y: '20%',
                                    width: '50%',
                                    height: '70%',
                                    funnelAlign: 'left',
                                    max: 1548
                                }
                            }
                        },
                        restore: {
                            show: true,
                            title: 'Restore'
                        },
                        saveAsImage: {
                            show: true,
                            title: 'Same as image',
                            lang: ['Save']
                        }
                    }
                },

                // Enable drag recalculate
                calculable: true,

                // Add series
                series: [{
                    name: 'Location',
                    type: 'pie',
                    radius: '70%',
                    center: ['50%', '57.5%'],
                    data: [
                        {value: 50, name: 'Dallas'},
                        {value: 30, name: 'Texas'},
                        {value: 20, name: 'Tokyo'},
                        {value: 20, name: 'Others'}
                    ]
                }]
        };
    
    basicpieChart.setOption(option);