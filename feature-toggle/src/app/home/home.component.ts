import { Component, EventEmitter, Output } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { FeatureCardComponent } from '../feature-card/feature-card.component';
import { FeatureStatus, FeatureType } from '../enum/feature.enum';
import { FormsModule } from '@angular/forms';
import { IselectedFilters } from '../interface/feature.interface';
import { features } from 'process';
import { FeatureService } from '../feature.service';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NavbarComponent, FeatureCardComponent, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  rtCheckboxSate: boolean = false;
  searchBarInput: string = '';
  //selectedFilters: string[] = [];
  selectedFilters2: IselectedFilters = {
    featureFilter: null,
    releaseFilter: null,
    enabledFilter: null,
    disabledFilter: null,
    searchQuery: null
  };
  @Output() applyFiltersEvent = new EventEmitter<IselectedFilters>(); //
  constructor(private featureService: FeatureService) { }

  applyFilters(): void {
    this.selectedFilters2 = {
      featureFilter: null,
      releaseFilter: null,
      enabledFilter: null,
      disabledFilter: null,
      searchQuery: null
    };

    this.selectedFilters2.searchQuery = this.searchBarInput;
    const checkboxes = document.querySelectorAll('.form-check-input');

    checkboxes.forEach((checkbox) => {
      const inputElement = checkbox as HTMLInputElement;
      if (inputElement.checked) {
        switch (inputElement.id) {
          case 'filterAction1':
            this.selectedFilters2.featureFilter = true;
            break;
          case 'filterAction2':
            this.selectedFilters2.releaseFilter = true;
            break;
          case 'filterAction3':
            this.selectedFilters2.enabledFilter = true;
            break;
          case 'filterAction4':
            this.selectedFilters2.disabledFilter = true;
            break;
        }
      }

      else {
        switch (inputElement.id) {
          case 'filterAction1':
            this.selectedFilters2.featureFilter = null;
            break;
          case 'filterAction2':
            this.selectedFilters2.releaseFilter = null;
            break;
          case 'filterAction3':
            this.selectedFilters2.enabledFilter = null;
            break;
          case 'filterAction4':
            this.selectedFilters2.disabledFilter = null;
            break;
        }
      }

    });
    this.applyFiltersEvent.emit(this.selectedFilters2);

  }

  toggleReleaseCheckbox() {
    const releaseCheckbox = document.getElementById('filterAction2') as HTMLInputElement;
    const enabledCheckbox = document.getElementById('filterAction3') as HTMLInputElement;
    const disabledCheckbox = document.getElementById('filterAction4') as HTMLInputElement;

    if (releaseCheckbox.checked) {
      enabledCheckbox.removeAttribute('disabled');
      disabledCheckbox.removeAttribute('disabled');
    }

    else {
      enabledCheckbox.setAttribute('disabled', 'true');
      disabledCheckbox.setAttribute('disabled', 'true');
      enabledCheckbox.checked = false;
      disabledCheckbox.checked = false;
    }
  }
}
