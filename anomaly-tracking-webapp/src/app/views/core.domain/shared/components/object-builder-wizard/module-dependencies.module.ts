import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { ModuleDependenciesComponent } from './module-dependencies/module-dependencies.component';

@NgModule({
  declarations: [ModuleDependenciesComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    ModuleDependenciesComponent
  ],
})
export class ModuleDependenciesModule { }
