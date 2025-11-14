import { Component, input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterCreds, User } from '../../../types/user';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  protected creds = {} as RegisterCreds;
  membersFromHome = input.required<User[]>();

  register() {
    console.log(this.creds);
  }

  cancel() {
    console.log("Cancelled!");
  }
}
