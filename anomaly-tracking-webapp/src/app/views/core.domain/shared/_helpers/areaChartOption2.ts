import { id } from '@swimlane/ngx-datatable';
import { Options } from 'highcharts';

export const areaChartOptions2: 
Options = {
  chart: {
    styledMode: true,
    height: (9 / 18) * 100 + '%', // 16:9 ratio
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
    text: 'Area Chart',
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
      color: 'green',
      type: 'areaspline',
      keys: ['y', 'selected'],
      data: [
        [200, false],
        [100, false],
        [90, false],
        [80, false],
        [70, false],
        [60, false],
        [50, false],
        [40, false],
        [30, false],
        [1, false],
        [300, false],
      ],
    },
  ],
};
