import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FeatureService } from '../feature.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isAdmin: number | undefined;


  constructor(private router: Router,
    private toastr: ToastrService,
    private featureService: FeatureService
  ) {

    const payload = this.featureService.decodeToken();

    payload.IsAdmin === "True" ? this.isAdmin = 1 : this.isAdmin = 0;
  }

  onLogout() {
    this.featureService.deleteToken();
    this.router.navigate(['/user/login']);
    this.toastr.success('See you later!', 'Logout Successful');
  }
}
