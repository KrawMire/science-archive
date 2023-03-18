import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainComponent implements OnInit {
  constructor(
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.userService
      .getAllUsers()
      .subscribe({
        next: (response) => {
          if (response.success) {
            console.log(response.data);
          } else {
            alert(response.error);
          }
        },
        error: (error) => {
          alert(error);
        }
      })

  }
}
