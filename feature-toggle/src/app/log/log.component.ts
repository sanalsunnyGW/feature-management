import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { NavbarComponent } from '../navbar/navbar.component';
import { OnInit } from '@angular/core';
import { IPaginationLog } from '../interface/feature.interface';
import { FeatureService } from '../feature.service';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-log',
  standalone: true,
  imports: [NavbarComponent, MatTableModule, DatePipe, FormsModule],
  templateUrl: './log.component.html',
  styleUrl: './log.component.scss'
})
export class LogComponent implements OnInit {
  pageNumber: number = 0;
  searchBarInput: string = '';
  isLoading: boolean = true ;

  dataSource: IPaginationLog = {
    pageSize: 0,
    currentPage: 0,
    totalCount: 0,
    totalPages: 0,
    logs: []
  };

  constructor(private featureService: FeatureService, private toastr: ToastrService) { }
  ngOnInit(): void {
    this.fetchPaginatedLog();
  }

  displayedColumns: string[] = ['serialNo', 'UserId', 'Username', 'FeatureId', 'FeatureName', 'BusinessId', 'BusinessName', 'Date', 'Time', 'Action'];

  fetchPaginatedLog() {
    this.featureService.getLog(this.pageNumber, this.searchBarInput).subscribe({
      next: (response: IPaginationLog) => {
        this.dataSource = response;
        this.isLoading = false;
        if (this.dataSource.logs.length === 0) {
          this.toastr.warning('No Logs');
        }
      },
      error: (err) => {
        this.toastr.error('"Error while fetching Logs', 'Error');
      }
    });
  }


  goToPage(page: number) {
    if (page >= 0 && page <= this.dataSource.totalPages) {
      this.pageNumber = page;
      this.fetchPaginatedLog();
    }
  }

  nextPage() {
    if (this.pageNumber < this.dataSource.totalPages - 1) {
      this.pageNumber++;
      this.fetchPaginatedLog();
    }
  }

  previousPage() {
    if (this.pageNumber > 0) {
      this.pageNumber--;
      this.fetchPaginatedLog();
    }
  }

  resetPageNo() {
    this.pageNumber = 0
  }


  downloadTrigger() {
    this.featureService.downloadLogs().subscribe({
      next: (response_blob) => {
        const timestamp = Date.now();
        const a = document.createElement('a');
        const objectUrl = URL.createObjectURL(response_blob);
        a.href = objectUrl;
        a.download = `feature-logs_${timestamp}.csv`;
        a.click();
        URL.revokeObjectURL(objectUrl);

        this.toastr.success('Download successful!', 'Download');

      },
      error: (error) => {
        this.toastr.error('Error downloading logs', 'Error');
      }
    });
  }
}
