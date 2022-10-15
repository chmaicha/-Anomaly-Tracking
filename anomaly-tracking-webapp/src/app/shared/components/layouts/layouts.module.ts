import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminLayoutSidebarLargeComponent } from './admin-layout-sidebar-large/admin-layout-sidebar-large.component';
import { HeaderSidebarLargeComponent } from './admin-layout-sidebar-large/header-sidebar-large/header-sidebar-large.component';
import { AdminLayoutSidebarCompactComponent } from './admin-layout-sidebar-compact/admin-layout-sidebar-compact.component';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';
import { BlankLayoutComponent } from './blank-layout/blank-layout.component';
import { NgbModule, NgbDropdownModule } from "@ng-bootstrap/ng-bootstrap";
import { RouterModule } from '@angular/router';
import { SharedPipesModule } from '../../../views/core.domain/_shared/pipes/shared-pipes.module';
import { SearchModule } from '../../../views/core.domain/_shared/components/search/search.module';
import { SidebarLargeComponent } from './admin-layout-sidebar-large/sidebar-large/sidebar-large.component';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { SidebarCompactComponent } from './admin-layout-sidebar-compact/sidebar-compact/sidebar-compact.component';
import { HeaderSidebarCompactComponent } from './admin-layout-sidebar-compact/header-sidebar-compact/header-sidebar-compact.component';
import { FooterComponent } from '../../../views/core.domain/_shared/components/footer/footer.component';
import { SharedDirectivesModule } from '../../../views/core.domain/_shared/directives/shared-directives.module';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { FileUploadModule } from '../../../views/core.domain/_shared/components/media/media-upload/media-upload.module';
import { FileDownloadModule } from '../../../views/core.domain/_shared/components/media/media-download/media-download.module';

const components = [
    HeaderSidebarCompactComponent,
    HeaderSidebarLargeComponent,
    SidebarLargeComponent,
    SidebarCompactComponent,
    FooterComponent,
    AdminLayoutSidebarLargeComponent,
    AdminLayoutSidebarCompactComponent,
    AuthLayoutComponent,
    BlankLayoutComponent,
];

@NgModule({
  imports: [
    NgbModule,
    RouterModule,
    FormsModule,
    FileUploadModule,
    FileDownloadModule,
    SearchModule,
    SharedPipesModule,
    SharedDirectivesModule,
    PerfectScrollbarModule,
    TranslateModule,
    CommonModule,
    NgbDropdownModule
  ],
  declarations: components,
  exports: components
})
export class LayoutsModule { }
