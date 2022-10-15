// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  caching: {
    enabled: false,
    inMemory: false
  },
  appUID : '77eda5e9-4e11-40ac-99dd-9a0fdce4c9df',
  coreApiUrl: 'http://localhost:100',
  apiUrl: 'http://localhost:220',
  webAppUrl: 'http://localhost:4200/#/',
  mediaSrcFilePath: 'C:\\inetpub\\stockagevehicule\\static.stockagevehicule.fr\\temp\\',
  mediaDestFilePath: 'C:\\inetpub\\stockagevehicule\\static.stockagevehicule.fr\\files\\',
  staticFilesUrl: 'http://localhost:90/static.stockagevehicule.fr/files',
  lastInterationDate: 3600,
  style: {
    homePageLogo: 'pedretti/homepage-logo.svg',
    headerLogo: 'pedretti/header-logo.svg',
    loadingPage: 'pedretti/loading-page-logo.png'
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
