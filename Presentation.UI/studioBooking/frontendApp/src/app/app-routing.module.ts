import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudioListComponent } from './component/studio-list/studio-list.component';
import { StudioDetailComponent } from './component/studio-detail/studio-detail.component';
import { BookingListComponent } from './component/booking-list/booking-list.component';
import { BookingFormComponent } from './component/booking-form/booking-form.component'; 
const routes: Routes = [
  { path: '', redirectTo: '/studios', pathMatch: 'full' },
  { path: 'studios', component: StudioListComponent },
  { path: 'studios/:id', component: StudioDetailComponent },
  { path: 'bookings', component: BookingListComponent },
  { path: 'bookings/create', component: BookingFormComponent },
  { path: '**', redirectTo: '/studios' } // Redirect any unknown paths to studios
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
