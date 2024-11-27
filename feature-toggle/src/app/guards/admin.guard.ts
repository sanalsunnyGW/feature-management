import { CanActivateFn, Router } from '@angular/router';
import { FeatureService } from '../feature.service';
import { inject } from '@angular/core';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(FeatureService);
  const router = inject(Router);

  let isAdmin : number;
  const payload = authService.decodeToken()

  payload.IsAdmin === "True" ? isAdmin = 1 : isAdmin = 0;

  console.log()
  if (isAdmin) {

    return true;
    
  } else {
    router.navigate(['/home'])
    return false;
  }
  
};
