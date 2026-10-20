import { Component, OnInit } from '@angular/core';
import { AsyncPipe, NgIf } from "@angular/common";
import { RouterLink, RouterLinkActive, RouterOutlet } from "@angular/router";
import { NzI18nPipe } from "ng-zorro-antd/i18n";
import { SharedModule } from "@modules/shared/shared.module";
import { ModerationStoreService } from "@modules/admin/services/moderation-store.service";

@Component({
  selector: 'sar-admin-page',
  standalone: true,
  imports: [
    AsyncPipe,
    NgIf,
    RouterLink,
    RouterLinkActive,
    RouterOutlet,
    SharedModule,
    NzI18nPipe
  ],
  templateUrl: './admin-page.component.html',
  styleUrl: './admin-page.component.scss'
})
export class AdminPageComponent implements OnInit {
  readonly pendingCount$ = this.moderationStore.pendingCount$;

  constructor(private readonly moderationStore: ModerationStoreService) {}

  ngOnInit(): void {
    this.moderationStore.refresh();
  }
}
