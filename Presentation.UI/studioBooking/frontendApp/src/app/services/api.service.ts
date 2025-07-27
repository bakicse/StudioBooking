// src/app/services/api.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
// import { environment } from '../../environments/environment';
import { environment } from '../../environments/environment';
import { Studio } from '../models/studio.model'; // Assuming models are in studio.model.ts for simplicity
import { Booking, CreateBookingRequest, AvailableTimeSlot } from '../models/booking.model'; // Assuming models are in studio.model.ts for simplicity

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = environment.apiUrl; // Your backend API base URL

  constructor(private http: HttpClient) { }

  // --- Studio Endpoints ---

  /**
   * GET /api/studios - Get all studios
   */
  getAllStudios(): Observable<Studio[]> {
    return this.http.get<Studio[]>(`${this.apiUrl}/api/studios`);
  }

  /**
   * GET /api/studios/{id} - Get details of a specific studio
   * @param id The ID of the studio
   */
  getStudioById(id: number): Observable<Studio> {
    return this.http.get<Studio>(`${this.apiUrl}/api/studios/${id}`);
  }

  /**
   * GET /api/studios/search?area={area} - Search studios by area
   * @param area The area to search for
   */
  searchStudiosByArea(area: string): Observable<Studio[]> {
    let params = new HttpParams().set('area', area);
    return this.http.get<Studio[]>(`${this.apiUrl}/api/studios/search`, { params });
  }

  /**
   * GET /api/studios/nearby?lat={lat}&lng={lng}&radius={km} - Search studios within a radius
   * @param lat Latitude of the current location
   * @param lng Longitude of the current location
   * @param radius Radius in kilometers
   */
  searchStudiosNearby(lat: number, lng: number, radius: number): Observable<Studio[]> {
    let params = new HttpParams()
      .set('lat', lat.toString())
      .set('lng', lng.toString())
      .set('radius', radius.toString());
    return this.http.get<Studio[]>(`${this.apiUrl}/api/studios/nearby`, { params });
  }

  /**
   * GET /api/studios/{id}/availability?date={date} - Get available time slots for a studio on a specific date
   * @param studioId The ID of the studio
   * @param date The date to check availability (YYYY-MM-DD format)
   */
  getAvailableTimeSlots(studioId: number, date: string): Observable<AvailableTimeSlot[]> {
    let params = new HttpParams().set('date', date);
    return this.http.get<AvailableTimeSlot[]>(`${this.apiUrl}/api/studios/${studioId}/availability`, { params });
  }

  // --- Booking Endpoints ---

  /**
   * POST /api/bookings - Create a new booking
   * @param bookingRequest The booking data
   */
  createBooking(bookingRequest: CreateBookingRequest): Observable<Booking> {
    return this.http.post<Booking>(`${this.apiUrl}/api/bookings`, bookingRequest);
  }

  /**
   * GET /api/bookings - Get all bookings
   */
  getAllBookings(): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${this.apiUrl}/api/bookings`);
  }
}
