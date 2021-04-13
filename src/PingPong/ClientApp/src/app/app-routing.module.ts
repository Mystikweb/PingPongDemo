import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PlayerlistComponent } from './playerlist/playerlist.component';
import { PlayerdetailsComponent } from './playerdetails/playerdetails.component';

const routes: Routes = [
  { path: '', redirectTo: '/player', pathMatch: 'full' },
  { path: 'player', component: PlayerlistComponent },
  { path: 'player/:playerId', component: PlayerdetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
