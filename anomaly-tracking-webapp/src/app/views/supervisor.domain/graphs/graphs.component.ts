import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  VERSION,
  Input,
} from '@angular/core';
import { Chart } from 'angular-highcharts';
import { mainContentAnimation } from '../../core.domain/shared/animation';
import { SidebarService } from '../../core.domain/shared/services/sideBar.service';
import { areaChartOptions } from '../../core.domain/shared/_helpers/areaChartOption';
import * as Highcharts from 'highcharts';
import { areaChartOptions2 } from '../../core.domain/shared/_helpers/areaChartOption2';
import { donutChartOptions } from '../../core.domain/shared/_helpers/donutChartOption';
import { NgxGaugeType } from 'ngx-gauge/gauge/gauge';
import { LocalService } from '../../anomalytracking.domain/_shared/services/local.service';
import { Router } from '@angular/router';

declare var require: any;
const More = require('highcharts/highcharts-more');
More(Highcharts);

const Exporting = require('highcharts/modules/exporting');
Exporting(Highcharts);

const ExportData = require('highcharts/modules/export-data');
ExportData(Highcharts);

const Accessibility = require('highcharts/modules/accessibility');
Accessibility(Highcharts);
@Component({
  selector: 'app-graphs',
  templateUrl: './graphs.component.html',
  styleUrls: ['./graphs.component.scss'],
  animations: [mainContentAnimation()],
})
export class GraphsComponent implements OnInit {
  areaChart = new Chart(areaChartOptions);
  areaChart2 = new Chart(areaChartOptions2);
  donutChart=new Chart(donutChartOptions);
chart=true;
chart2=false;



  public options: any = {
    chart: {
      plotBackgroundColor: null,
      plotBorderWidth: null,
      plotShadow: false,
      type: 'pie',
      // height: (9 / 6) * 100 + '%', // 16:9 ratio

    },
    credits:{
      enabled: false
    },
    title: {
      text: 'Anomalies detected By Project in  2021'
    },
    tooltip: {
      pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
    },
    accessibility: {
      point: {
        valueSuffix: '%'
      }
    },
    plotOptions: {
      pie: {
        allowPointSelect: true,
        cursor: 'pointer',
        dataLabels: {
          enabled: false
        },
        showInLegend: true
      }
    },
    series: [{
      name: 'CLIO',
      colorByPoint: true,
      data: [{
        name: 'SEAT',
        y: 61.41,
        sliced: true,
        selected: true
      }, {
        name: 'SKODA',
        y: 11.84
      }, {
        name: 'BMW',
        y: 10.85
      }, {
        name: 'RENAUT',
        y: 4.67
      }, {
        name: 'MERCEDES',
        y: 4.18
      }, {
        name: 'WOLKSWAGEN',
        y: 7.05
      }]
    }]
  };

  sidebarState!: string;
  constructor(private sidebarService: SidebarService,
    private localStore : LocalService,
    private router: Router,

    ) {}
  dynamicGaugeDemoValue = 50;
  anomalyNumber=15582;
  clientNumber=2358;
  usersNumber=56784;
  UserValue=80;
  ClientValue=60;
  AnomalyValue=40;
  topValue=100;
  onUpdateClick() {
    this.dynamicGaugeDemoValue =
    Math.round(Math.random() * 1000)/10;
  }

  ngOnInit() {
    if(this.localStore.getData('CurrentUserName')){
        if(this.localStore.getData('CurrentUserRole') === '1'){
          this.sidebarService.sidebarStateObservable$.subscribe(
            (newState: string) => {
              this.sidebarState = newState;
            }
          );
          Highcharts.chart('container', this.options);
        }else{
          this.router.navigateByUrl('/anomaly-declaration')

        }


    }else{
      this.router.navigateByUrl('/')
    }



  }

  changeChart(){
    this.chart2=true;
    this.chart=false;
  }


}
