import { Options } from 'highcharts';

export const donutChartOptions: Options = {
  chart: {
    type: 'pie',
    plotShadow: false,
  },
  credits: {
    enabled: false,
  },
  plotOptions: {
    pie: {
      innerSize: '90%',
      borderWidth: 40,
      borderColor: null,
      slicedOffset: 20,
      dataLabels: {
        connectorWidth: 0,
      },
    },
  },
  title: {
    verticalAlign: 'middle',
    floating: true,
    text: 'All Users',
  },
  legend: {
    enabled: false,
  },
  series: [
    {
      type: 'pie',
      data: [
        { name: '', y: 1, color: '#eeeeee' },
        { name: '', y: 2, color: '#393e46' },
        { name: '', y: 3, color: '#00adb5' },
        { name: '', y: 4, color: '#eeeeee' },
        { name: '', y: 5, color: '#506ef9' },
      ],
    },
  ],
};