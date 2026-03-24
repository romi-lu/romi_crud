import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthApiService } from '../../services/auth-api.service';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  private readonly fb = inject(FormBuilder);
  private readonly authApi = inject(AuthApiService);
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  readonly busy = signal(false);

  readonly form = this.fb.nonNullable.group({
    username: ['admin', [Validators.required]],
    password: ['admin123', [Validators.required]]
  });

  submit(): void {
    if (this.form.invalid || this.busy()) {
      return;
    }
    this.busy.set(true);
    const { username, password } = this.form.getRawValue();
    this.authApi
      .login({ username, password })
      .pipe(finalize(() => this.busy.set(false)))
      .subscribe({
        next: (res) => {
          if (!res?.token) {
            window.alert('La API respondió sin token. Revisa la consola de red (F12).');
            return;
          }
          this.auth.setToken(res.token);
          void this.router.navigate(['/personas']);
        }
      });
  }
}
