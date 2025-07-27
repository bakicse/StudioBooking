import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { CreateBookingRequest, Booking, AvailableTimeSlot } from '../../models/booking.model'; // Assuming models are in booking.model.ts
import { Studio } from '../../models/studio.model'; // Assuming models are in booking.model.ts
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-booking-form',
  standalone: false, // This line is implicitly false, so it's usually omitted
  // imports: [CommonModule, FormsModule], // FIX: REMOVE THIS IMPORTS ARRAY for non-standalone components
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css']
})
export class BookingFormComponent implements OnInit {
  // @Input properties for when used from StudioDetailComponent
  @Input() studioId: number | undefined;
  @Input() bookingDate: string | undefined;
  @Input() timeSlot: string | undefined;

  @Output() bookingSuccess = new EventEmitter<Booking>();
  @Output() bookingError = new EventEmitter<string>();
  @Output() cancel = new EventEmitter<void>();

  // New properties for direct access
  studios: Studio[] = [];
  selectedStudioId: number | undefined;
  selectedBookingDate: string = new Date().toISOString().split('T')[0]; // Default to today
  availableTimeSlots: AvailableTimeSlot[] = [];
  selectedTimeSlotForDirectBooking: string | undefined; // For dropdown selection
  availabilityLoading: boolean = false;

  userName: string = '';
  email: string = '';
  loading: boolean = false;
  formErrorMessage: string | null = null; // Separate error message for the form itself

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    // If studioId is NOT provided (i.e., direct access), load all studios
    if (!this.studioId) {
      this.loadAllStudios();
    } else {
      // If studioId IS provided (from StudioDetail), set it for the form
      this.selectedStudioId = this.studioId;
      this.selectedBookingDate = this.bookingDate || new Date().toISOString().split('T')[0];
      this.selectedTimeSlotForDirectBooking = this.timeSlot; // Use the input timeSlot
      this.getAvailability(); // Get availability based on input studio/date
    }
  }

  loadAllStudios(): void {
    this.apiService.getAllStudios().subscribe({
      next: (data) => {
        this.studios = data;
        // Optional: Pre-select the first studio if available
        if (this.studios.length > 0) {
          this.selectedStudioId = this.studios[0].id;
          this.getAvailability(); // Load availability for the first studio
        }
      },
      error: (err) => {
        this.formErrorMessage = 'Failed to load studios for selection.';
        console.error('Error loading studios:', err);
      }
    });
  }

  onStudioChange(): void {
    this.getAvailability();
  }

  onDateChange(): void {
    this.getAvailability();
  }

  getAvailability(): void {
    if (this.selectedStudioId && this.selectedBookingDate) {
      this.availabilityLoading = true;
      this.formErrorMessage = null;
      this.apiService.getAvailableTimeSlots(this.selectedStudioId, this.selectedBookingDate).subscribe({
        next: (data) => {
          this.availableTimeSlots = data;
          this.availabilityLoading = false;
          // Reset selected time slot if current one is no longer available
          if (!this.availableTimeSlots.some(slot => `${slot.startTime.slice(0,5)}-${slot.endTime.slice(0,5)}` === this.selectedTimeSlotForDirectBooking)) {
              this.selectedTimeSlotForDirectBooking = undefined;
          }
        },
        error: (err) => {
          this.formErrorMessage = 'Failed to load availability for selected studio and date.';
          console.error('Error loading availability:', err);
          this.availabilityLoading = false;
        }
      });
    } else {
      this.availableTimeSlots = [];
      this.selectedTimeSlotForDirectBooking = undefined;
    }
  }

  onSubmit(): void {
    // Determine which timeSlot to use based on how the component is accessed
    const finalTimeSlot = this.timeSlot || this.selectedTimeSlotForDirectBooking;
    const finalStudioId = this.studioId || this.selectedStudioId;
    const finalBookingDate = this.bookingDate || this.selectedBookingDate;

    if (!finalStudioId || !finalBookingDate || !finalTimeSlot || !this.userName || !this.email) {
      this.formErrorMessage = 'All fields (Studio, Date, Time Slot, Name, Email) are required.';
      return;
    }

    this.loading = true;
    this.formErrorMessage = null; // Clear previous form errors

    const request: CreateBookingRequest = {
      studioId: finalStudioId,
      bookingDate: finalBookingDate,
      timeSlot: finalTimeSlot,
      userName: this.userName,
      email: this.email
    };

    this.apiService.createBooking(request).subscribe({
      next: (booking) => {
        this.bookingSuccess.emit(booking);
        this.loading = false;
        this.resetForm();
        // If accessed directly, also reset studio/date/timeSlot selections
        if (!this.studioId) {
            this.selectedStudioId = undefined;
            this.selectedBookingDate = new Date().toISOString().split('T')[0];
            this.selectedTimeSlotForDirectBooking = undefined;
            this.availableTimeSlots = [];
        }
      },
      error: (err) => {
        let errorMessage = 'An unknown error occurred.';
        if (err.error && typeof err.error === 'string') {
          errorMessage = err.error; // Backend often returns simple string error messages
        } else if (err.error && err.error.errors) { // For validation errors from ASP.NET Core
            errorMessage = Object.values(err.error.errors).flat().join('; ');
        }
        else if (err.message) {
          errorMessage = err.message;
        }
        this.bookingError.emit(errorMessage); // Emit to parent if used as input
        this.formErrorMessage = errorMessage; // Show on form if direct access
        console.error('Booking creation error:', err);
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.resetForm();
    this.cancel.emit();
    this.formErrorMessage = null;
    // If accessed directly, you might want to navigate away or just clear the form
    if (!this.studioId) {
        this.selectedStudioId = undefined;
        this.selectedBookingDate = new Date().toISOString().split('T')[0];
        this.selectedTimeSlotForDirectBooking = undefined;
        this.availableTimeSlots = [];
    }
  }

  private resetForm(): void {
    this.userName = '';
    this.email = '';
    this.loading = false;
  }
}