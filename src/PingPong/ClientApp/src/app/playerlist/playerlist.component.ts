import { Component, OnInit, AfterViewInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

import { PlayerClientService, Player, SkillLevel } from '../services/playerclient.service';

@Component({
  selector: 'app-playerlist',
  templateUrl: './playerlist.component.html',
  styleUrls: ['./playerlist.component.css']
})
export class PlayerlistComponent implements OnInit, AfterViewInit {
  requestRunning = false;
  skillLevels = SkillLevel;
  playerList: Player[];
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
      this.requestRunning = true;
      this.client.delete(player.playerId).subscribe(() => {
        this.snackBar.open('Player removed successfully', null, {
          duration: 1000
        }).afterDismissed().subscribe(() => {
          this.loadPlayerData();
        });
      });
    });
  }

  loadPlayerData() {
    this.requestRunning = true;
    this.client.getAll().subscribe((data: Player[]) => {
      this.playerList = data;
      this.requestRunning = false;
    });
  }
}
