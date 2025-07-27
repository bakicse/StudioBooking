import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { FormsModule } from '@angular/forms'; // Import FormsModule for ngModel

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StudioListComponent } from './component/studio-list/studio-list.component';
import { StudioDetailComponent } from './component/studio-detail/studio-detail.component';
import { BookingListComponent } from './component/booking-list/booking-list.component';
import { BookingFormComponent } from './component/booking-form/booking-form.component';

@NgModule({
  declarations: [
    AppComponent,
    StudioListComponent,
    StudioDetailComponent,
    BookingListComponent,
    BookingFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, // Add HttpClientModule here
    FormsModule // Add FormsModule here
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
