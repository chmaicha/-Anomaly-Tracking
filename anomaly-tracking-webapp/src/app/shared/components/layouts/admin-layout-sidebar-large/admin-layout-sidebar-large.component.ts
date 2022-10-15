import { Component, OnInit, ViewChild } from '@angular/core';
import { NavigationService } from '../../../../views/core.domain/_shared/services/common/navigation.service';
import { SearchService } from 'src/app/shared/services/search.service';
import { Router, RouteConfigLoadStart, ResolveStart, RouteConfigLoadEnd, ResolveEnd } from '@angular/router';
import { PerfectScrollbarDirective } from 'ngx-perfect-scrollbar';
import { MediaDownloadService } from 'src/app/views/core.domain/_shared/components/media/media-download/media-download.service';

@Component({
  selector: 'app-admin-layout-sidebar-large',
  templateUrl: './admin-layout-sidebar-large.component.html',
  styleUrls: ['./admin-layout-sidebar-large.component.scss']
})
export class AdminLayoutSidebarLargeComponent implements OnInit
{
 
  moduleLoading: boolean;
  @ViewChild(PerfectScrollbarDirective, { static: true }) perfectScrollbar: PerfectScrollbarDirective;

  constructor(
    public navService: NavigationService,
    public searchService: SearchService,
    public fileDownloadService: MediaDownloadService,
    private router: Router
  ) { }

  ngOnInit()
  {
    this.router.events.subscribe(event =>
    {
      if (event instanceof RouteConfigLoadStart || event instanceof ResolveStart)
      {
        this.moduleLoading = true;
      }
      if (event instanceof RouteConfigLoadEnd || event instanceof ResolveEnd)
      {
        this.moduleLoading = false;
      }
    });
  }
}
