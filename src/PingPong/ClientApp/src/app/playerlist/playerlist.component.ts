import { Component, OnInit, AfterViewInit } from '@angular/core';

import { PlayerClientService, Player } from '../services/playerclient.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-playerlist',
  templateUrl: './playerlist.component.html',
  styleUrls: ['./playerlist.component.css']
})
export class PlayerlistComponent implements OnInit, AfterViewInit {
  playerList$: Observable<Player[]>;
  constructor(private client: PlayerClientService) { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.playerList$ = this.client.getAll();
  }
}
