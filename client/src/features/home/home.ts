import { Component, Input, input, signal } from '@angular/core';
import { RegisterComponent } from "../account/register/register.component";
import { User } from '../../types/user';

@Component({
  selector: 'app-home',
  imports: [RegisterComponent],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  protected registerMode = signal(false);
  @Input({required: true}) membersFromApp: User[] = [];

  showRegister() {
    this.registerMode.set(true);
  }
}
