import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { Studio } from '../../models/studio.model';
import { Booking, AvailableTimeSlot} from '../../models/booking.model'; // Import Booking model

@Component({
  selector: 'app-studio-detail',
  templateUrl: './studio-detail.component.html',
  styleUrls: ['./studio-detail.component.css'],
  standalone: false,
})
export class StudioDetailComponent implements OnInit {
  studio: Studio | undefined;
  selectedDate: string = new Date().toISOString().split('T')[0]; // Default to today
  availableTimeSlots: AvailableTimeSlot[] = [];
  errorMessage: string | null = null;
  loading: boolean = false;
  availabilityLoading: boolean = false;

  // For booking form interaction
  showBookingForm: boolean = false;
  selectedTimeSlot: string | null = null;
  bookingSuccessMessage: string | null = null;
  bookingErrorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const studioId = Number(params.get('id'));
      if (studioId) {
        this.loadStudioDetails(studioId);
        this.getAvailability(studioId, this.selectedDate);
      }
    });
  }

  loadStudioDetails(id: number): void {
    this.loading = true;
    this.apiService.getStudioById(id).subscribe({
      next: (data) => {
        this.studio = data;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load studio details.';
        console.error('Error loading studio details:', err);
        this.loading = false;
      }
    });
  }

  onDateChange(): void {
    if (this.studio) {
      this.getAvailability(this.studio.id, this.selectedDate);
    }
  }

  getAvailability(studioId: number, date: string): void {
    this.availabilityLoading = true;
    this.errorMessage = null;
    this.apiService.getAvailableTimeSlots(studioId, date).subscribe({
      next: (data) => {
        this.availableTimeSlots = data;
        this.availabilityLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load availability for this date.';
        console.error('Error loading availability:', err);
        this.availabilityLoading = false;
      }
    });
  }

  bookTimeSlot(slot: AvailableTimeSlot): void {
    this.showBookingForm = true;
    this.selectedTimeSlot = `${slot.startTime.slice(0, 5)}-${slot.endTime.slice(0, 5)}`;
    this.bookingSuccessMessage = null; // Clear previous messages
    this.bookingErrorMessage = null;
  }

  onBookingSuccess(booking: Booking): void {
    this.bookingSuccessMessage = `Booking successful! ID: ${booking.id}, Studio: ${booking.studioName}, Time: ${booking.timeSlot}`;
    this.showBookingForm = false; // Hide form on success
    this.getAvailability(this.studio!.id, this.selectedDate); // Refresh availability
  }

  onBookingError(error: string): void {
    this.bookingErrorMessage = `Booking failed: ${error}`;
  }

  cancelBookingForm(): void {
    this.showBookingForm = false;
    this.selectedTimeSlot = null;
    this.bookingSuccessMessage = null;
    this.bookingErrorMessage = null;
  }
}