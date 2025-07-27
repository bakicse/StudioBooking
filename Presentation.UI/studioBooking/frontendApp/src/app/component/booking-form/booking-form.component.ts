import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { CreateBookingRequest, Booking } from '../models/booking.model'; // Assuming models are in booking.model.ts

@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css']
})
export class BookingFormComponent implements OnInit {
  @Input() studioId: number | undefined;
  @Input() bookingDate: string | undefined;
  @Input() timeSlot: string | undefined;

  @Output() bookingSuccess = new EventEmitter<Booking>();
  @Output() bookingError = new EventEmitter<string>();
  @Output() cancel = new EventEmitter<void>();

  userName: string = '';
  email: string = '';
  loading: boolean = false;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    // Optional: Pre-fill user data if available from a logged-in user context
  }

  onSubmit(): void {
    if (!this.studioId || !this.bookingDate || !this.timeSlot || !this.userName || !this.email) {
      this.bookingError.emit('All fields are required.');
      return;
    }

    this.loading = true;
    const request: CreateBookingRequest = {
      studioId: this.studioId,
      bookingDate: this.bookingDate,
      timeSlot: this.timeSlot,
      userName: this.userName,
      email: this.email
    };

    this.apiService.createBooking(request).subscribe({
      next: (booking) => {
        this.bookingSuccess.emit(booking);
        this.loading = false;
        this.resetForm();
      },
      error: (err) => {
        let errorMessage = 'An unknown error occurred.';
        if (err.error && typeof err.error === 'string') {
          errorMessage = err.error; // Backend often returns simple string error messages
        } else if (err.message) {
          errorMessage = err.message;
        }
        this.bookingError.emit(errorMessage);
        console.error('Booking creation error:', err);
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.resetForm();
    this.cancel.emit();
  }

  private resetForm(): void {
    this.userName = '';
    this.email = '';
  }
}