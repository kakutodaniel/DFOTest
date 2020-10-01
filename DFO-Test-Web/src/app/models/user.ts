export interface IUser {
  id: number;
  name: string;
  age: number;
  address: string;
  hashId: string;
  creationDate: string;
  updateDate: string;
  getName(): string;
}

export class User implements IUser {
  getName(): string {
    return this.name;
  }
  id: number;
  name: string;
  age: number;
  address: string;
  hashId: string;
  creationDate: string;
  updateDate: string;
}
