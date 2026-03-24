import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  DocumentType,
  Gender,
  PersonRead,
  PersonType,
  PersonWrite
} from '../models/api.models';

@Injectable({ providedIn: 'root' })
export class PersonService {
  private readonly http = inject(HttpClient);
  private readonly base = `${environment.apiUrl}/api`;

  getPersons(): Observable<PersonRead[]> {
    return this.http.get<PersonRead[]>(`${this.base}/persons`);
  }

  createPerson(body: PersonWrite): Observable<PersonRead> {
    return this.http.post<PersonRead>(`${this.base}/persons`, body);
  }

  updatePerson(id: number, body: PersonWrite): Observable<void> {
    return this.http.put<void>(`${this.base}/persons/${id}`, body);
  }

  deletePerson(id: number): Observable<void> {
    return this.http.delete<void>(`${this.base}/persons/${id}`);
  }

  getDocumentTypes(): Observable<DocumentType[]> {
    return this.http.get<DocumentType[]>(`${this.base}/catalogs/document-types`);
  }

  getPersonTypes(): Observable<PersonType[]> {
    return this.http.get<PersonType[]>(`${this.base}/catalogs/person-types`);
  }

  getGenders(): Observable<Gender[]> {
    return this.http.get<Gender[]>(`${this.base}/catalogs/genders`);
  }

  forceTestError(): Observable<unknown> {
    return this.http.get(`${this.base}/testerror/force`);
  }
}
