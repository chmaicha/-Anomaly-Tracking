import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { NgxGaugeModule } from "ngx-gauge";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutModule } from '@angular/cdk/layout';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSortModule } from '@angular/material/sort';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ChartModule } from 'angular-highcharts';
import { QRCodeModule } from 'angularx-qrcode';
import { HighchartsChartModule } from 'highcharts-angular';
import { NgChartsModule } from 'ng2-charts';
import { AlertConfig } from 'ngx-bootstrap/alert';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { AnomalyDeclarationComponent } from './views/core.domain/shared/components/anomaly-declaration/anomaly-declaration.component';
import { FacePositionComponent } from './views/core.domain/shared/components/face-position/face-position.component';
import { FacePosition1Component } from './views/core.domain/shared/components/face-position1/face-position1.component';
import { FacePosition2Component } from './views/core.domain/shared/components/face-position2/face-position2.component';
import { HeaderComponent } from './views/core.domain/shared/components/header/header.component';
import { SidebarComponent } from './views/core.domain/shared/components/sidebar/sidebar.component';
import { ToggleSidebarComponent } from './views/core.domain/shared/components/sidebar/toggle-sidebar/toggle-sidebar.component';
import { AnomalyHistoryComponent } from './views/supervisor.domain/anomaly-history/anomaly-history.component';
import { DashComponent } from './views/supervisor.domain/dash/dash.component';
import { GraphsComponent } from './views/supervisor.domain/graphs/graphs.component';
import { ProjectManagementComponent } from './views/supervisor.domain/poject-management/project-management/project-management.component';
import { ProcessAnomalytypeListComponent } from './views/supervisor.domain/process-anomalytype-list/process-anomalytype-list.component';
import { ProcessManagementComponent } from './views/supervisor.domain/process-management/process-management.component';
import { ProjectMoldListComponent } from './views/supervisor.domain/project-mold-list/project-mold-list.component';
import { SettingDialogueComponent } from './views/supervisor.domain/setting-dialogue/setting-dialogue.component';
import { UserManagementComponent } from './views/supervisor.domain/user-management/user-management.component';
import { ContextService } from './views/core.domain/shared/services/common/context.service';
import { AuthentificationComponent } from './views/authentification.domain/authentification/authentification.component';
// import bn-ng-idle service
// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}

const appRoutes: Routes = [
  { path: 'user-management', component: UserManagementComponent },
  { path: '', component: AuthentificationComponent },
  { path: 'anomaly-history', component: AnomalyHistoryComponent },
  { path: 'graphs', component: GraphsComponent },
  { path: 'project-management', component: ProjectManagementComponent },
  { path: 'anomaly-declaration', component: AnomalyDeclarationComponent },
  { path: 'face-position', component: FacePositionComponent },
  { path: 'process-management', component: ProcessManagementComponent },
  // { path: 'project-management', component: ProjectManagementComponent },
  { path: 'project-management/:id', component: ProjectMoldListComponent },
  { path: 'process-management/:id', component: ProcessAnomalytypeListComponent},
];

@NgModule({
  declarations: [
    SidebarComponent,
    AppComponent,
    AuthentificationComponent,
    AnomalyHistoryComponent,
    GraphsComponent,
    UserManagementComponent,
    DashComponent,
    HeaderComponent,
    ToggleSidebarComponent,
    SettingDialogueComponent,
    FacePositionComponent,
    AnomalyDeclarationComponent,
    FacePosition1Component,
    FacePosition2Component,
    ProcessManagementComponent,
    ProjectManagementComponent,
    ProjectMoldListComponent,
    ProcessAnomalytypeListComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MDBBootstrapModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatInputModule,
    MatFormFieldModule,
    QRCodeModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    FormsModule,
    FlexLayoutModule,
    MatTableModule,
    MatListModule,
    MatPaginatorModule,
    MatSortModule,
    MatToolbarModule,
    MatDialogModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    NgChartsModule,
    MatToolbarModule,
    MatChipsModule,
    MatStepperModule,
    MatRadioModule,
    ChartModule,
    HighchartsChartModule,
    NgxGaugeModule,
    QRCodeModule ,
    ToastrModule.forRoot({
      positionClass :'toast-bottom-right'
    }),

    TranslateModule.forRoot(
      {
        loader: {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
        }
      }
    ),
  ],
  providers: [AlertConfig, BsDatepickerConfig, BsDropdownConfig,BsModalService,ContextService],
  bootstrap: [AppComponent],
})
export class AppModule {}
