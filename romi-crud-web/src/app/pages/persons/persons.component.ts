import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { AuthService } from '../../core/auth.service';
import { DocumentType, Gender, PersonRead, PersonType } from '../../models/api.models';
import { PersonService } from '../../services/person.service';

@Component({
  selector: 'app-persons',
  imports: [ReactiveFormsModule],
  templateUrl: './persons.component.html',
  styleUrl: './persons.component.css'
})
export class PersonsComponent implements OnInit {
  private readonly fb = inject(FormBuilder);
  private readonly api = inject(PersonService);
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  readonly persons = signal<PersonRead[]>([]);
  readonly documentTypes = signal<DocumentType[]>([]);
  readonly personTypes = signal<PersonType[]>([]);
  readonly genders = signal<Gender[]>([]);
  readonly loading = signal(true);
  readonly saving = signal(false);
  readonly editingId = signal<number | null>(null);

  readonly form = this.fb.nonNullable.group({
    firstName: ['', [Validators.required, Validators.maxLength(128)]],
    lastName: ['', [Validators.required, Validators.maxLength(128)]],
    documentNumber: ['', [Validators.required, Validators.maxLength(32)]],
    documentTypeId: [1, [Validators.required]],
    personTypeId: [1, [Validators.required]],
    genderId: [1, [Validators.required]]
  });

  ngOnInit(): void {
    this.reloadAll();
  }

  reloadAll(): void {
    this.loading.set(true);
    forkJoin({
      persons: this.api.getPersons(),
      doc: this.api.getDocumentTypes(),
      pt: this.api.getPersonTypes(),
      g: this.api.getGenders()
    }).subscribe({
      next: ({ persons, doc, pt, g }) => {
        this.persons.set(persons);
        this.documentTypes.set(doc);
        this.personTypes.set(pt);
        this.genders.set(g);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  startCreate(): void {
    this.editingId.set(null);
    this.form.reset({
      firstName: '',
      lastName: '',
      documentNumber: '',
      documentTypeId: this.documentTypes()[0]?.id ?? 1,
      personTypeId: this.personTypes()[0]?.id ?? 1,
      genderId: this.genders()[0]?.id ?? 1
    });
  }

  startEdit(p: PersonRead): void {
    this.editingId.set(p.id);
    this.form.patchValue({
      firstName: p.firstName,
      lastName: p.lastName,
      documentNumber: p.documentNumber,
      documentTypeId: p.documentTypeId,
      personTypeId: p.personTypeId,
      genderId: p.genderId
    });
  }

  cancelForm(): void {
    this.editingId.set(null);
    this.form.reset();
  }

  save(): void {
    if (this.form.invalid || this.saving()) {
      return;
    }
    this.saving.set(true);
    const v = this.form.getRawValue();
    const body = {
      firstName: v.firstName.trim(),
      lastName: v.lastName.trim(),
      documentNumber: v.documentNumber.trim(),
      documentTypeId: Number(v.documentTypeId),
      personTypeId: Number(v.personTypeId),
      genderId: Number(v.genderId)
    };
    const id = this.editingId();
    const done = {
      next: () => {
        this.cancelForm();
        this.reloadList();
      },
      complete: () => this.saving.set(false),
      error: () => this.saving.set(false)
    };

    if (id == null) {
      this.api.createPerson(body).subscribe(done);
    } else {
      this.api.updatePerson(id, body).subscribe(done);
    }
  }

  deletePerson(p: PersonRead): void {
    if (!window.confirm(`¿Eliminar a ${p.firstName} ${p.lastName}?`)) {
      return;
    }
    this.api.deletePerson(p.id).subscribe(() => this.reloadList());
  }

  logout(): void {
    this.auth.logout();
    void this.router.navigate(['/login']);
  }

  forceErrorDemo(): void {
    this.api.forceTestError().subscribe({
      error: () => {
        /* errorInterceptor muestra el mensaje */
      }
    });
  }

  private reloadList(): void {
    this.api.getPersons().subscribe((list) => this.persons.set(list));
  }
}
