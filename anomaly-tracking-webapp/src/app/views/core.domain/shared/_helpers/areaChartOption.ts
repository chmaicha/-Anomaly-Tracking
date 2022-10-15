import { id } from '@swimlane/ngx-datatable';
import { Options } from 'highcharts';

export const areaChartOptions: 
Options = {
  chart: {
    styledMode: true,
    // height: (9 / 20) * 100 + '%', // 16:9 ratio
 
  
  },
  plotOptions: {
    series: {
      marker: {
        enabled: false,
      },
    },
  },

  legend: {
    enabled: false,
  },
  credits: {
    enabled: false,
  },
  title: {
    text: "Anomaly's chart 2021/2022",
  },
  yAxis: {
    visible: true,
  },
  xAxis: {
    visible: true,
    categories: [
      'jan',
      'feb',
      'mar',
      'apr',
      'may',
      'jun',
      'jul',
      'aut',
      'sep',
      'oct',
      'nov',
      'dec',
    ],
  },
  defs: {
    gradient0: {
      tagName: 'linearGradient',
      id: 'gradient-0',
      x1: 0,
      y1: 0,
      x2: 0,
      y2: 1,
      children: [
        {
          tagName: 'stop',
          offset: 0,
        },
        {
          tagName: 'stop',
          offset: 0,
        },
      ],
    },
  } as any,
  series: [
    {
      color: 'red',
      type: 'areaspline',
      keys: ['y', 'selected'],
      data: [
        [29.9, false],
        [71, 5, false],
        [106, false],
        [144.5, false],
        [176.0, false],
        [135.6, false],
        [148.5, false],
        [216.4, false],
        [194.1, false],
        [95.6, false],
        [300, false],
      ],
    },
  ],
};
