import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { FeatureService } from '../feature.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(FeatureService);
  const router = inject(Router);

  if (authService.isLoggedIn()) {
    return true;
  }
  else {
    router.navigate(['/user/login'])
    return false;
  }
};
