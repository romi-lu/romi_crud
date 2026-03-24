export interface DocumentType {
  id: number;
  code: string;
  name: string;
}

export interface PersonType {
  id: number;
  code: string;
  name: string;
}

export interface Gender {
  id: number;
  code: string;
  name: string;
}

export interface PersonRead {
  id: number;
  firstName: string;
  lastName: string;
  documentNumber: string;
  documentTypeId: number;
  documentTypeName: string;
  personTypeId: number;
  personTypeName: string;
  genderId: number;
  genderName: string;
}

export interface PersonWrite {
  firstName: string;
  lastName: string;
  documentNumber: string;
  documentTypeId: number;
  personTypeId: number;
  genderId: number;
}
