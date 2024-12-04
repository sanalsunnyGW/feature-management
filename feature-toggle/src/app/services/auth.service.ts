import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { TOKEN_KEY, TOKEN_LENGTH, TOKEN_PAYLOAD } from '../shared/constants';
import { IJwtPayload, ILoginAccept, ILoginReturn, ISignUpAccept, ISignUpReturn } from '../interface/feature.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl: string = environment.apiUrl;
  public userId: number = 0;

  constructor(private router: Router, private http: HttpClient, private toastr: ToastrService) { }

  //for login
  login(data: ILoginAccept): Observable<ILoginReturn> {
    return this.http.post<ILoginReturn>(`${this.baseUrl}/api/Login`, data);
  }

  //for signup
  addUser(data: ISignUpAccept): Observable<ISignUpReturn> {
    return this.http.post<ISignUpReturn>(`${this.baseUrl}/api/User`, data);
  }

  isLoggedIn(): boolean {
    const result: boolean = localStorage.getItem(TOKEN_KEY) != null ? true : false;
    if (result) {
      this.checkExpiry();
    }
    return result;

  }

  saveToken(token: string): void {
    localStorage.setItem(TOKEN_KEY, token);
  }

  deleteToken(): void {
    localStorage.removeItem(TOKEN_KEY);
  }

  decodeToken(): IJwtPayload {
    try {
      const token: string | null = localStorage.getItem(TOKEN_KEY);

      if (!token) {
        throw new Error("Token not found in localStorage.");
      }

      const tokenParts: string[] = token!.split('.');
      if (tokenParts.length !== TOKEN_LENGTH) {
        throw new Error("Invalid token format.");
      }

      const payloadBase64: string = tokenParts[TOKEN_PAYLOAD];
      const payloadJson: IJwtPayload = JSON.parse(window.atob(payloadBase64));
      return payloadJson;
    } catch (error) {
      console.error(error);
      return {} as IJwtPayload;
    }
  }

  checkExpiry(): void {
    const payload: IJwtPayload = this.decodeToken();
    if (payload) {
      const expTime: number = payload.exp;
      const now: number = Date.now() / 1000;
      const diff: number = expTime - (Math.floor(now));

      if (diff <= 0) {
        this.deleteToken();
        this.router.navigate(['/user/login']);
        this.toastr.error('Please login again', 'Session Timeout')

      }
    }
  }

  checkIsAdmin(): boolean {
    const payload: IJwtPayload = this.decodeToken();

    const result: boolean = payload.IsAdmin === "True" ? true : false;

    return result;
  }

  getUserId(): string {
    const payload: IJwtPayload = this.decodeToken();
    return payload.UserID;
  }

}
