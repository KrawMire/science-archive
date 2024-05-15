import { Component, Input } from "@angular/core";
import { News } from "@models/news/news";
import { NzCardComponent, NzCardMetaComponent } from "ng-zorro-antd/card";
import { NzButtonComponent } from "ng-zorro-antd/button";
import { RouterLink } from "@angular/router";
import { SharedModule } from "@modules/shared/shared.module";

@Component({
  selector: 'sar-admin-news-card',
  standalone: true,
  imports: [
    NzCardComponent,
    NzCardMetaComponent,
    NzButtonComponent,
    RouterLink,
    SharedModule
  ],
  templateUrl: './admin-news-card.component.html',
  styleUrl: './admin-news-card.component.scss'
})
export class AdminNewsCardComponent {
  @Input() style?: string;
  @Input() news!: News;
}
