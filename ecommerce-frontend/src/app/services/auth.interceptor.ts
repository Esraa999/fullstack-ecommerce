import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { AuthResult } from '../models/user.model';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn): Observable<HttpEvent<unknown>> => {
  const authService = inject(AuthService);
  const token = authService.accessToken;

  let authReq = req;
  if (token) {
    authReq = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
  }

  return next(authReq).pipe(
    catchError(error => {
      if (error.status === 401 && authService.refreshToken) {
        // Fixed: Call renamed method with string from getter
        return authService.refreshAccessToken(authService.refreshToken!).pipe(
          switchMap((result: AuthResult) => {
            if (result.success && result.accessToken) {
              const newReq = req.clone({
                setHeaders: { Authorization: `Bearer ${result.accessToken}` }
              });
              return next(newReq);
            }
            authService.logout();
            return throwError(() => error);
          }),
          catchError(refreshError => {
            authService.logout();
            return throwError(() => refreshError);
          })
        );
      }
      return throwError(() => error);
    })
  );
};