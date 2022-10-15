import { Component, HostListener, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { LvfAppModule } from 'src/app/views/core.domain/_shared/enums/lvf-app-module';
import { SearchFilterBase } from 'src/app/views/core.domain/_shared/filters/search-filter-base';
import { AppModuleCategory } from 'src/app/views/core.domain/_shared/models/application/app-module-category';
import { AppModuleCategoryItem } from 'src/app/views/core.domain/_shared/models/application/app-module-category-item';
import { IResponse } from 'src/app/views/core.domain/_shared/models/common/iexception';
import { ContextService } from 'src/app/views/core.domain/_shared/services/common/context.service';
import { FilterService } from 'src/app/views/core.domain/_shared/services/common/filter.service';
import { IPricingInfo, Pricing } from 'src/app/views/stockagevehicule.domain/_shared/models/pricing/pricing';
import { PricingService } from 'src/app/views/stockagevehicule.domain/_shared/services/pricing.service';
import { IMenuItem, NavigationService } from '../../../../../views/core.domain/_shared/services/common/navigation.service';
import { Utils } from '../../../../../views/core.domain/_shared/_helpers/utils';

@Component({
	selector: 'app-sidebar-large',
	templateUrl: './sidebar-large.component.html',
	styleUrls: ['./sidebar-large.component.scss']
})
export class SidebarLargeComponent implements OnInit {

	selectedItem: IMenuItem;
	nav: IMenuItem[];

	public filter: SearchFilterBase;
	public showAlert: boolean;
	categories: AppModuleCategory[];
	categoryItems: AppModuleCategoryItem[];

	constructor(
		private filterService: FilterService,
		public router: Router,
		public navService: NavigationService,
		private contextService: ContextService
	) { }

	ngOnInit() {
		this.showAlert = false;
		this.filter = this.filterService.getFilterBase(false, false);
		this.categories = this.contextService.data.AppModuleNavigation.Categories;
	}

	selectItem(item) {
		this.navService.sidebarState.childnavOpen = true;
		this.selectedItem = item;
		this.setActiveMainItem(item);
	}
	
	closeChildNav() {
		this.navService.sidebarState.childnavOpen = false;
		this.setActiveFlag();
	}

	onClickChangeActiveFlag(item) {
		this.setActiveMainItem(item);
	}

	setActiveMainItem(item) {
		this.categories.map(c => c.Items.forEach(item => {
			item.active = false;
		}));
		item.active = true;
	}

	setActiveFlag() {
		// if (window && window.location) {
		// 	const activeRoute = window.location.hash || window.location.pathname;
		// 	this.nav.forEach(item => {
		// 		item.active = false;
		// 		if (activeRoute.indexOf(item.state) !== -1) {
		// 			this.selectedItem = item;
		// 			item.active = true;
		// 		}
		// 		if (item.sub) {
		// 			item.sub.forEach(subItem => {
		// 				subItem.active = false;
		// 				if (activeRoute.indexOf(subItem.state) !== -1) {
		// 					this.selectedItem = item;
		// 					item.active = true;
		// 				}
		// 				if (subItem.sub) {
		// 					subItem.sub.forEach(subChildItem => {
		// 						if (activeRoute.indexOf(subChildItem.state) !== -1) {
		// 							this.selectedItem = item;
		// 							item.active = true;
		// 							subItem.active = true;
		// 						}
		// 					});
		// 				}
		// 			});
		// 		}
		// 	});
		// }
	}

	updateSidebar() {
		if (Utils.isMobile()) {
			this.navService.sidebarState.sidenavOpen = false;
			this.navService.sidebarState.childnavOpen = false;
		} else {
			this.navService.sidebarState.sidenavOpen = true;
		}
	}

	@HostListener('window:resize', ['$event'])
	onResize(event) {
		this.updateSidebar();
	}

	// selectedItem: IMenuItem;
	// nav: IMenuItem[];

	// public filter: SearchFilterBase;
	// public pricies: Pricing[];
	// public showAlert: boolean;

	// constructor(
	// 	private pricingService: PricingService,
	// 	private filterService: FilterService,
	// 	public router: Router,
	// 	public navService: NavigationService
	// ) { }

	// ngOnInit() {
	// 	this.showAlert = false;
	// 	this.filter = this.filterService.getFilterBase(false, false);

	// 	this.updateSidebar();
	// 	// CLOSE SIDENAV ON ROUTE CHANGE
	// 	this.router.events.pipe(filter(event => event instanceof NavigationEnd))
	// 		.subscribe((routeChange) => {
	// 			this.closeChildNav();
	// 			if (Utils.isMobile()) {
	// 				this.navService.sidebarState.sidenavOpen = false;
	// 			}
	// 		});

	// 	this.navService.menuItems$
	// 		.subscribe((items) => {
	// 			this.nav = items;
	// 			this.setActiveFlag();
	// 			this.onAlertPrincing();
	// 		});
	// }

	// hasAlert(item: IMenuItem) {
	// 	switch (item.lvfAppModule) {
	// 		case LvfAppModule.PRICING:
	// 			return this.showAlert;

	// 		default:
	// 			return false;
	// 	}
	// }

	// selectItem(item) {
	// 	this.navService.sidebarState.childnavOpen = true;
	// 	this.selectedItem = item;
	// 	this.setActiveMainItem(item);
	// }
	// closeChildNav() {
	// 	this.navService.sidebarState.childnavOpen = false;
	// 	this.setActiveFlag();
	// }

	// onClickChangeActiveFlag(item) {
	// 	this.setActiveMainItem(item);
	// }
	// setActiveMainItem(item) {
	// 	this.nav.forEach(item => {
	// 		item.active = false;
	// 	});
	// 	item.active = true;
	// }

	// setActiveFlag() {
	// 	if (window && window.location) {
	// 		const activeRoute = window.location.hash || window.location.pathname;
	// 		this.nav.forEach(item => {
	// 			item.active = false;
	// 			if (activeRoute.indexOf(item.state) !== -1) {
	// 				this.selectedItem = item;
	// 				item.active = true;
	// 			}
	// 			if (item.sub) {
	// 				item.sub.forEach(subItem => {
	// 					subItem.active = false;
	// 					if (activeRoute.indexOf(subItem.state) !== -1) {
	// 						this.selectedItem = item;
	// 						item.active = true;
	// 					}
	// 					if (subItem.sub) {
	// 						subItem.sub.forEach(subChildItem => {
	// 							if (activeRoute.indexOf(subChildItem.state) !== -1) {
	// 								this.selectedItem = item;
	// 								item.active = true;
	// 								subItem.active = true;
	// 							}
	// 						});
	// 					}
	// 				});
	// 			}
	// 		});
	// 	}
	// }

	// updateSidebar() {
	// 	if (Utils.isMobile()) {
	// 		this.navService.sidebarState.sidenavOpen = false;
	// 		this.navService.sidebarState.childnavOpen = false;
	// 	} else {
	// 		this.navService.sidebarState.sidenavOpen = true;
	// 	}
	// }

	// @HostListener('window:resize', ['$event'])
	// onResize(event) {
	// 	this.updateSidebar();
	// }

	// onAlertPrincing() {
	// 	//Get pricies to show alert on those who will expire soon
	// 	this.pricingService.getPricings(this.filter)
	// 		.subscribe((response: IResponse<IPricingInfo[]>) => {
	// 			if (response.IsSuccessful) {
	// 				this.pricies = response.Data ? response.Data.map(info => new Pricing(info)) : [];
	// 				let price = this.pricies.find(p => p.UpcomingExpiration);
	// 				this.showAlert = price != null;
	// 			}
	// 		});
	// }

}
