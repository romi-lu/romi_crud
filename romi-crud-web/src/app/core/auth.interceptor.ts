import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { environment } from '../../environments/environment';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const url = req.url;
  const isLogin =
    url.endsWith('/api/auth/login') || url.includes(`${environment.apiUrl}/api/auth/login`);

  if (isLogin || !auth.token()) {
    return next(req);
  }

  return next(
    req.clone({
      setHeaders: { Authorization: `Bearer ${auth.token()}` }
    })
  );
};
