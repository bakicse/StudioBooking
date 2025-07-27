import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
// import { environment } from '@env/environment.dev'
import { Category } from '../interfaces/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  apiUrl = `${environment.apiUrl}Category/`;
  constructor(private http: HttpClient) { }

  getCategories() {
    return this.http.get<Category[]>(`${this.apiUrl}GetAll`);
  }
  createCategory(category: any) {
    return this.http.post(`${this.apiUrl}Create`, category);
  }
  updateCategory(category: Category) {
    return this.http.put(`${this.apiUrl}Edit`, category);
  }
  getCategoryByEncryptedId(encryptedId: string) {
    return this.http.get<Category>(`${this.apiUrl}GetById/${encryptedId}`);
  }
}
