import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from './auth.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const auth = inject(AuthService);

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if (err.status === 0) {
        const onLogin = router.url.includes('/login');
        window.alert(
          onLogin
            ? 'No hay respuesta del servidor. Arranca la API (dotnet run en RomiCrud.Api, perfil http, puerto 5282) y usa `ng serve` con el proxy del proyecto. Sin la API encendida el login no puede funcionar.'
            : 'No se pudo contactar al servidor. Comprueba que la API esté en ejecución (puerto 5282).'
        );
      } else if (err.status === 401) {
        const onLogin = router.url.includes('/login');
        if (onLogin) {
          const body = err.error as { error?: string } | undefined;
          window.alert(body?.error ?? 'Usuario o contraseña incorrectos.');
        } else {
          auth.logout();
          void router.navigate(['/login'], { queryParams: { sesion: 'expirada' } });
        }
      } else if (err.status >= 500) {
        const body = err.error as { error?: string; errorLogId?: number } | undefined;
        const msg = body?.error ?? err.message ?? 'Error del servidor';
        const id = body?.errorLogId != null ? ` (log #${body.errorLogId})` : '';
        console.error('HTTP error', err);
        window.alert(`${msg}${id}`);
      } else if (err.status >= 400) {
        const body = err.error;
        const msg =
          typeof body === 'string'
            ? body
            : (body as { error?: string; title?: string })?.error ??
              (body as { title?: string })?.title ??
              err.message;
        if (msg) {
          window.alert(msg);
        }
      }

      return throwError(() => err);
    })
  );
};
