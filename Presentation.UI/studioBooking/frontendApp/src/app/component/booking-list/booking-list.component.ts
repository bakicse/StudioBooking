import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { Booking } from '../models/booking.model'; // Assuming models are in booking.model.ts

@Component({
  selector: 'app-booking-list',
  templateUrl: './booking-list.component.html',
  styleUrls: ['./booking-list.component.css']
})
export class BookingListComponent implements OnInit {
  bookings: Booking[] = [];
  errorMessage: string | null = null;
  loading: boolean = false;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.loadAllBookings();
  }

  loadAllBookings(): void {
    this.loading = true;
    this.errorMessage = null;
    this.apiService.getAllBookings().subscribe({
      next: (data) => {
        this.bookings = data;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load bookings. Please try again later.';
        console.error('Error loading bookings:', err);
        this.loading = false;
      }
    });
  }
}