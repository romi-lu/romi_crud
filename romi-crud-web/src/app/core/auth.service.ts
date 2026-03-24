import { Injectable, computed, signal } from '@angular/core';

const STORAGE_KEY = 'romi_jwt';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly _token = signal<string | null>(this.readStored());

  readonly token = this._token.asReadonly();
  readonly isAuthenticated = computed(() => !!this._token());

  setToken(token: string | null): void {
    this._token.set(token);
    if (token) {
      localStorage.setItem(STORAGE_KEY, token);
    } else {
      localStorage.removeItem(STORAGE_KEY);
    }
  }

  logout(): void {
    this.setToken(null);
  }

  private readStored(): string | null {
    if (typeof localStorage === 'undefined') {
      return null;
    }
    return localStorage.getItem(STORAGE_KEY);
  }
}
