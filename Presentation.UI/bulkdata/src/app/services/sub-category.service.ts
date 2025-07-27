import { Injectable } from '@angular/core';
import { environment} from '@env/environment.dev'
import { SubCategory} from '../interfaces/sub-category';
import { HttpClient } from '@angular/common/http';
import { SubCategoryInit } from '../interfaces/sub-category-init';
import { DropDownDto } from '../interfaces/base/drop-down-dto';

@Injectable({
  providedIn: 'root'
})
export class SubCategoryService {
  apiUrl = `${environment.apiUrl}SubCategory/`;
  constructor(private http: HttpClient) { }
  
    getSubCategoryInit() {
      return this.http.get<SubCategoryInit>(`${this.apiUrl}Init`);
    }
    
    getSubCategories() {
      return this.http.get<SubCategory[]>(`${this.apiUrl}GetAll`);
    }
    createSubCategory(subCategory: any) {
      return this.http.post(`${this.apiUrl}Create`, subCategory);
    }
    updateSubCategory(subCategory: SubCategory) {
      return this.http.put(`${this.apiUrl}Edit`, subCategory);
    }
    getSubCategoryByEncryptedId(encryptedId: string) {
      return this.http.get<SubCategory>(`${this.apiUrl}GetById/${encryptedId}`);
    }
    getSubCategoryByCategoryId(catId: number) {
      return this.http.get<DropDownDto[]>(`${this.apiUrl}GetDropdownItemsByCatId/${catId}`);
    }
}
