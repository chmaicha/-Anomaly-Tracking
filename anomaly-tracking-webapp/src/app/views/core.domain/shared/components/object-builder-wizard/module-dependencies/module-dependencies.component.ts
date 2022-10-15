import { Component, OnInit } from '@angular/core';
import { ModuleDependencyService } from 'src/app/views/core.domain/_shared/services/common/module-dependency.service';
import { ModuleDependencyStep, ModuleDependency } from './module.dependency';

@Component({
  selector: 'app-module-dependencies',
  templateUrl: './module-dependencies.component.html',
  styleUrls: ['./module-dependencies.component.scss']
})
export class ModuleDependenciesComponent implements OnInit {

  public step: ModuleDependencyStep;

  constructor(
    private dependencyService: ModuleDependencyService
  ) {
    this.step = this.dependencyService.get().last();
  }

  ngOnInit() { }
}
