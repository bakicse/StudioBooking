export interface CreateBookingRequest {
  studioId: number;
  bookingDate: string; // YYYY-MM-DD format
  timeSlot: string;    // HH:MM-HH:MM format
  userName: string;
  email: string;
}

export interface Booking {
  id: number;
  studioId: number;
  studioName: string;
  bookingDate: string; // YYYY-MM-DD format
  timeSlot: string;
  userName: string;
  email: string;
}

export interface AvailableTimeSlot {
  startTime: string; // HH:MM:SS format
  endTime: string;   // HH:MM:SS format
}