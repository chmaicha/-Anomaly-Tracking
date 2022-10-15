import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { ToastrModule } from 'ngx-toastr';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SearchModule } from './search/search.module';
import { SharedComponentsModule } from './shared-components.module';
import { SharedDirectivesModule } from '../directives/shared-directives.module';
import { SharedPipesModule } from '../pipes/shared-pipes.module';
import { TranslateModule } from '@ngx-translate/core';
// Pipes


@NgModule({
  exports: [
    CommonModule,
    TranslateModule,
    SharedPipesModule,
    SharedDirectivesModule,
  ],
  imports: [
    CommonModule,
    PerfectScrollbarModule,
    SearchModule,
    ToastrModule.forRoot(),
    NgbModule,
    SharedComponentsModule,
    SharedDirectivesModule,
    SharedPipesModule,
    RouterModule,
    TranslateModule,
  ]
})
export class SharedModule { }
