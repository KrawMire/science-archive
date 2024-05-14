import { Component, OnInit } from "@angular/core";
import { AuthApiService } from "@services/auth-api.service";

@Component({
  selector: 'sar-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.scss']
})
export class ProfilePageComponent implements OnInit {
  constructor(
    private readonly authApiService: AuthApiService
  ) {}

  ngOnInit(): void {
    this.authApiService.getMe();
  }
}
