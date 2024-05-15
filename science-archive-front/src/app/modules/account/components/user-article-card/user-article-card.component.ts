import { Component, Input } from "@angular/core";
import { NzCardComponent, NzCardMetaComponent } from "ng-zorro-antd/card";
import { NzTagComponent } from "ng-zorro-antd/tag";
import { SharedModule } from "@modules/shared/shared.module";
import { Article } from "@models/article/article";
import { RouterLink } from "@angular/router";
import { NzIconDirective } from "ng-zorro-antd/icon";

@Component({
  selector: 'sar-user-article-card',
  standalone: true,
  imports: [
    NzCardComponent,
    NzCardMetaComponent,
    NzTagComponent,
    SharedModule,
    RouterLink,
    NzIconDirective
  ],
  templateUrl: './user-article-card.component.html',
  styleUrl: './user-article-card.component.scss'
})
export class UserArticleCardComponent {
  @Input() style?: string;
  @Input() article!: Article;

  getStatusIcon(): string {
    if (this.article.status === 0) {
      return 'sync';
    } else if (this.article.status === 1) {
      return 'check-circle';
    } else {
      return 'close-circle';
    }
  }

  getStatusText(): string {
    if (this.article.status === 0) {
      return 'On processing';
    } else if (this.article.status === 1) {
      return 'Published';
    } else {
      return 'Declined';
    }
  }

  getStatusColor(): string {
    if (this.article.status === 0) {
      return 'processing';
    } else if (this.article.status === 1) {
      return 'success';
    } else {
      return 'error';
    }
  }
}
