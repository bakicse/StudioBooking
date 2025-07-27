export interface Coordinates {
  latitude: number;
  longitude: number;
}

export interface Location {
  city: string;
  area: string;
  address: string;
  coordinates: Coordinates;
}

export interface Contact {
  phone: string;
  email: string;
}

export interface Availability {
  openTime: string; // Using string to match TimeOnly format (HH:MM:SS)
  closeTime: string; // Using string to match TimeOnly format (HH:MM:SS)
}

export interface Studio {
  id: number;
  name: string;
  type: string;
  description: string;
  pricePerHour: number;
  currency: string;
  rating: number;
  amenities: string[];
  images: string[];
  location: Location;
  contact: Contact;
  availability: Availability;
}