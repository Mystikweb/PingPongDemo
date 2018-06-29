import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PlayerlistComponent } from './playerlist/playerlist.component';

const routes: Routes = [
  { path: '', component: PlayerlistComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
