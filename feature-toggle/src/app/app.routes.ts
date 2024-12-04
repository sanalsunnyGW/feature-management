import { Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './user/signup/signup.component';
import { LogComponent } from './log/log.component';
import { authGuard } from './guards/auth.guard';
import { adminGuard } from './guards/admin.guard';



export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full'},
    {
        path: 'user',
        component: UserComponent,
        children: [
            { path: 'login', component: LoginComponent },
            { path: 'signup', component: SignupComponent }
        ]

    },
    { path: 'home', component: HomeComponent, canActivate: [authGuard] },
    { path: 'log', component: LogComponent, canActivate: [authGuard, adminGuard] }

];
