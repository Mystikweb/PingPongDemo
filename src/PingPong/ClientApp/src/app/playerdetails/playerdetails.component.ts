import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Player, SkillLevel, PlayerClientService } from '../services/playerclient.service';

@Component({
  selector: 'app-playerdetails',
  templateUrl: './playerdetails.component.html',
  styleUrls: ['./playerdetails.component.css']
})
export class PlayerdetailsComponent implements OnInit {
  @Input() player: Player;
  playerForm: FormGroup;
  skills = new Array();

  constructor(private builder: FormBuilder, private client: PlayerClientService) {

    Object.keys(SkillLevel).forEach(key => {
      if (!isNaN(Number(key))) {
        this.skills.push({ id: key, value: SkillLevel[key] });
      }
    });

    this.createForm();
  }

  ngOnInit() {}

  onSubmit() {
    this.player = this.prepareSavePlayer();
    this.client.create(this.player).subscribe();
  }

  createForm() {
    this.playerForm = this.builder.group({
      firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      email: ['', [Validators.email, Validators.required]],
      skillLevel: ['0', Validators.required],
      age: [null, [Validators.min(1), Validators.max(99)]]
    });
  }

  prepareSavePlayer() {
    const formValue = this.playerForm.value;

    const playerResult: Player = Player.fromJS({
      playerId: this.player !== undefined ? this.player.playerId : 0,
      firstName: formValue.firstName as string,
      lastName: formValue.lastName as string,
      email: formValue.email as string,
      skillLevel: SkillLevel[formValue.skillLevel as number],
      age: formValue.age
    });

    return playerResult;
  }
}
