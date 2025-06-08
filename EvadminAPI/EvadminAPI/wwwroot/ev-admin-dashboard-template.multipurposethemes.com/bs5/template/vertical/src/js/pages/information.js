
$(function () {


      
  var options = {
          series: [11, 5, 3 ,2],
      labels: ['Available(11)', 'Charging(5)', 'Offline(3)' , 'Coming Soon(2)'],
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
    colors:['#04a08b', '#0000FF', '#FFA500','#808080'],
        legend: {
          position: 'right',
          height: 250,
        }
        };

        var chart = new ApexCharts(document.querySelector("#charts_widget_2_chart"), options);
        chart.render();
  
}); // End of use strict


var options = {
          series: [{
            name: "Electricity Consumption",
            data: [10, 41, 35, 25, 19, 22]
        }],
          chart: {
          height: 87,
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
          categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
        }
        };

        var chart = new ApexCharts(document.querySelector("#chart"), options);
        chart.render();


      

       var options = {
          series: [76],
          chart: {
          type: 'radialBar',
          offsetY: -20,
          sparkline: {
            enabled: true
          }
        },
        plotOptions: {
          radialBar: {
            startAngle: -90,
            endAngle: 90,
            track: {
              background: "#e7e7e7",
              strokeWidth: '97%',
              margin: 5, // margin is in pixels
              dropShadow: {
                enabled: true,
                top: 2,
                left: 0,
                color: '#999',
                opacity: 1,
                blur: 2
              }
            },
            dataLabels: {
              name: {
                show: false
              },
              value: {
                offsetY: -20,
                fontSize: '22px'
              }
            }
          }
        },
        grid: {
          padding: {
            top: -20
          }
        },
        fill: {
          type: 'gradient',
          gradient: {
            shade: 'light',
            shadeIntensity: 0.4,
            inverseColors: false,
            opacityFrom: 1,
            opacityTo: 1,
            stops: [0, 50, 53, 91]
          },
        },
        labels: ['Average Results'],
        };

        var chart = new ApexCharts(document.querySelector("#chart2"), options);
        chart.render();




         var options = {
          series: [{
            name: "Electricity Usage",
            data: [10, 41, 35, 151, 49, 62, 169, 91, 148]
        }],
          chart: {
          height: 149,
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



        var basicpieChart = echarts.init(document.getElementById('basic-pie'));
        var option = {
            // Add title
                title: {
                    text: 'A site user access source',
                    subtext: 'Purely Fictitious',
                    x: 'center'
                },

                // Add tooltip
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },

                // Add legend
                legend: {
                    orient: 'vertical',
                    x: 'left',
                    data: ['Direct Access', 'Mail Marketing', 'Union ad', 'Video ad', 'Search Engine']
                },

                // Add custom colors
                color: ['#689f38', '#38649f', '#389f99', '#ee1044', '#ff8f00'],

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
                    name: 'Marketing',
                    type: 'pie',
                    radius: '70%',
                    center: ['50%', '57.5%'],
                    data: [
                        {value: 335, name: 'Direct Access'},
                        {value: 310, name: 'Mail Marketing'},
                        {value: 234, name: 'Union ad'},
                        {value: 135, name: 'Video ad'},
                        {value: 1548, name: 'Search Engine'}
                    ]
                }]
        };