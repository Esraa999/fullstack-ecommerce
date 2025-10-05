import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { AuthResult, LoginDto, RegisterDto, User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/api/auth`;
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(private http: HttpClient) {
    const token = localStorage.getItem('accessToken');
    this.isLoggedInSubject.next(!!token);
  }

  login(credentials: LoginDto): Observable<AuthResult> {
    return this.http.post<AuthResult>(`${this.apiUrl}/login`, credentials).pipe(
      tap(result => {
        if (result.success) {
          this.storeTokens(result.accessToken!, result.refreshToken!);
          this.isLoggedInSubject.next(true);
        }
      }),
      catchError(this.handleError)
    );
  }

  register(user: RegisterDto): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/register`, user).pipe(
      catchError(this.handleError)
    );
  }

  // Fixed: Renamed method to avoid conflict with getter
  refreshAccessToken(rt: string): Observable<AuthResult> {
    return this.http.post<AuthResult>(`${this.apiUrl}/refresh`, { refreshToken: rt }).pipe(
      tap(result => {
        if (result.success) {
          this.storeTokens(result.accessToken!, result.refreshToken!);
        }
      }),
      catchError(this.handleError)
    );
  }

  logout(): void {
    localStorage.clear();
    this.currentUserSubject.next(null);
    this.isLoggedInSubject.next(false);
  }

  private storeTokens(accessToken: string, refreshToken: string): void {
    localStorage.setItem('accessToken', accessToken);
    localStorage.setItem('refreshToken', refreshToken);
  }

  get accessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  get refreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  private handleError(error: any) {
    console.error('Auth error:', error);
    return throwError(() => new Error(error.error?.message || 'Auth failed'));
  }
}