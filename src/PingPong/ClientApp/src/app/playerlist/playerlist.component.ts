import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Observable } from 'rxjs';

import { MatSnackBar } from '@angular/material';

import { PlayerClientService, Player, SkillLevel } from '../services/playerclient.service';

@Component({
  selector: 'app-playerlist',
  templateUrl: './playerlist.component.html',
  styleUrls: ['./playerlist.component.css']
})
export class PlayerlistComponent implements OnInit, AfterViewInit {
  skillLevels = SkillLevel;
  playerList$: Observable<Player[]>;
  constructor(private snackBar: MatSnackBar, private client: PlayerClientService) { }

  ngOnInit() {
  }

  ngAfterViewInit() {
    this.loadPlayerData();
  }

  remove(player: Player) {
    const confirmMessage = `Are you sure you would like to remove ${player.firstName} ${player.lastName}?`;
    this.snackBar.open(confirmMessage, 'OK', {
      duration: 5000
    }).onAction().subscribe(() => {
      this.client.delete(player.playerId).subscribe(() => {
        this.snackBar.open('Player removed successfully', null, {
          duration: 3000
        }).afterDismissed().subscribe(() => {
          this.playerList$.subscribe();
        });
      });
    });
  }

  loadPlayerData() {
    this.playerList$ = this.client.getAll();
  }
}
