import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Studio } from '../../models/studio.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-studio-list',
  templateUrl: './studio-list.component.html',
  styleUrls: ['./studio-list.component.css'],
  standalone: false,
})
export class StudioListComponent implements OnInit {
  studios: Studio[] = [];
  filteredStudios: Studio[] = [];
  searchArea: string = '';
  searchLat: number | null = null;
  searchLng: number | null = null;
  searchRadius: number | null = null;
  errorMessage: string | null = null;
  loading: boolean = false;

  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.loadAllStudios();
  }

  loadAllStudios(): void {
    this.loading = true;
    this.errorMessage = null;
    this.apiService.getAllStudios().subscribe({
      next: (data) => {
        this.studios = data;
        this.filteredStudios = data;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load studios. Please try again later.';
        console.error('Error loading studios:', err);
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.errorMessage = null;
    this.loading = true;

    if (this.searchArea) {
      this.apiService.searchStudiosByArea(this.searchArea).subscribe({
        next: (data) => {
          this.filteredStudios = data;
          this.loading = false;
        },
        error: (err) => {
          this.errorMessage = 'Failed to search studios by area.';
          console.error('Error searching by area:', err);
          this.loading = false;
        }
      });
    } else if (this.searchLat !== null && this.searchLng !== null && this.searchRadius !== null) {
      this.apiService.searchStudiosNearby(this.searchLat, this.searchLng, this.searchRadius).subscribe({
        next: (data) => {
          this.filteredStudios = data;
          this.loading = false;
        },
        error: (err) => {
          this.errorMessage = 'Failed to search studios nearby. Please check coordinates and radius.';
          console.error('Error searching nearby:', err);
          this.loading = false;
        }
      });
    } else {
      this.errorMessage = 'Please enter an area or valid coordinates and radius to search.';
      this.filteredStudios = this.studios; // Reset to all studios if no valid search criteria
      this.loading = false;
    }
  }

  clearSearch(): void {
    this.searchArea = '';
    this.searchLat = null;
    this.searchLng = null;
    this.searchRadius = null;
    this.errorMessage = null;
    this.loadAllStudios(); // Reload all studios
  }

  viewStudioDetails(id: number): void {
    this.router.navigate(['/studios', id]);
  }
}