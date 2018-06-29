import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PlayerClientService } from './playerclient.service';

@NgModule({
  imports: [
    CommonModule
  ],
  providers: [
    PlayerClientService
  ]
})
export class ServicesModule { }
