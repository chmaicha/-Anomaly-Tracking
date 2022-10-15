import { LvfAppModule } from "src/app/views/core.domain/_shared/enums/lvf-app-module";

/**
 * @class Module dependency
 */
export class ModuleDependency {

    module: string;
    url: string;
    steps: ModuleDependencyStep[];

    /**
     * @constructor
     * Creates an new instance of ModuleDependency.
     */
    constructor(moduleTitle: string, moduleUrl: string) {
        this.module = moduleTitle;
        this.url = moduleUrl;
        this.steps = [];
    }

    get inProgress(): boolean {
        return this.steps.length > 0;
    }

    push(entry: ModuleDependencyStep) {
        this.steps.push(entry);
    }

    pop(): ModuleDependencyStep {
        if (this.steps.length == 0) {
            throw 'No url to pop';
        }

        return this.steps.pop();
    }

    last(): ModuleDependencyStep {
        if (this.steps.length == 0) {
            return null;
        }

        return this.steps[this.steps.length - 1];
    }
}

/**
 * @class Module dependency item
 */
export class ModuleDependencyStep {

    module: string;
    action: string;
    url: string;
    lvfAppModule : LvfAppModule
}