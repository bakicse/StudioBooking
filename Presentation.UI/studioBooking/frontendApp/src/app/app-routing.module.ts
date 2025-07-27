import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudioListComponent } from './studio-list/studio-list.component';
import { StudioDetailComponent } from './studio-detail/studio-detail.component';
import { BookingListComponent } from './booking-list/booking-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/studios', pathMatch: 'full' },
  { path: 'studios', component: StudioListComponent },
  { path: 'studios/:id', component: StudioDetailComponent },
  { path: 'bookings', component: BookingListComponent },
  { path: '**', redirectTo: '/studios' } // Redirect any unknown paths to studios
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
