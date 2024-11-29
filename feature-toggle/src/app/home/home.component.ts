import { Component, EventEmitter, Output } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { FeatureCardComponent } from '../feature-card/feature-card.component';
import { FormsModule } from '@angular/forms';
import { IselectedFilters } from '../interface/feature.interface';
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
  ftCheckboxSate: boolean = false;
  rtEnabledCheckboxesState : boolean = false;
  enabledCheckboxSate: boolean = false;
  disabledCheckboxSate: boolean = false;
  searchBarInput: string ='';
  
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

    this.ftCheckboxSate === true ? this.selectedFilters2.featureFilter = true : this.selectedFilters2.featureFilter = null;
    this.rtCheckboxSate === true ? this.selectedFilters2.releaseFilter = true : this.selectedFilters2.releaseFilter = null;
    this.enabledCheckboxSate === true ? this.selectedFilters2.enabledFilter = true : this.selectedFilters2.enabledFilter = null;
    this.disabledCheckboxSate === true ? this.selectedFilters2.disabledFilter = true : this.selectedFilters2.disabledFilter = null;
    this.searchBarInput !== null||'' ? this.selectedFilters2.searchQuery = this.searchBarInput : this.selectedFilters2.searchQuery = null

    this.applyFiltersEvent.emit(this.selectedFilters2);

  }

  rtOptionsCheck(){
    if(!this.rtCheckboxSate){
      this.enabledCheckboxSate = false;
      this.disabledCheckboxSate = false;
    }
  }

  clearRtOptions(){
    this.rtCheckboxSate = false;
    this.enabledCheckboxSate = false;
    this.disabledCheckboxSate = false;
  }

  removeFilters(){
    this.rtCheckboxSate = false;
    this.ftCheckboxSate = false;
    this.rtEnabledCheckboxesState = false;
    this.enabledCheckboxSate = false;
    this.disabledCheckboxSate = false;
    this.searchBarInput =''; 

      this.selectedFilters2 = {
      featureFilter: null,
      releaseFilter: null,
      enabledFilter: null,
      disabledFilter: null,
      searchQuery: null
    };
  }
}
