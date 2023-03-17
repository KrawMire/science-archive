import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
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
