import { Component, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { User } from "@models/user/user";
import { AuthService } from "@modules/auth/services/auth.service";

@Component({
  selector: 'sar-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit {
  user$ = new BehaviorSubject<User | null>(null);

  constructor(
    private readonly authService: AuthService,
  ) {}

  ngOnInit(): void {
    const user = this.authService.getCurrentUser();

    if (!user) {
      window.location.assign("/auth");
      return;
    }

    this.user$.next(user);
  }
}
