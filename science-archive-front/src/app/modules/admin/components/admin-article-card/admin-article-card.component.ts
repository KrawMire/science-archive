import { Component, EventEmitter, Input, Output } from "@angular/core";
import { NzCardComponent, NzCardMetaComponent } from "ng-zorro-antd/card";
import { NzIconDirective } from "ng-zorro-antd/icon";
import { NzTagComponent } from "ng-zorro-antd/tag";
import { SharedModule } from "@modules/shared/shared.module";
import { Article } from "@models/article/article";
import { NzModalService } from "ng-zorro-antd/modal";
import { ArticleApiService } from "@services/article-api.service";
import { NzMessageService } from "ng-zorro-antd/message";

@Component({
  selector: 'sar-admin-article-card',
  standalone: true,
  imports: [
    NzCardComponent,
    NzCardMetaComponent,
    NzIconDirective,
    NzTagComponent,
    SharedModule
  ],
  templateUrl: './admin-article-card.component.html',
  styleUrl: './admin-article-card.component.scss'
})
export class AdminArticleCardComponent {
  @Input() style?: string;
  @Input() article!: Article;
  @Output() articleChange = new EventEmitter<void>();

  constructor(
    private readonly messageService: NzMessageService,
    private readonly modalService: NzModalService,
    private readonly articleService: ArticleApiService,
  ) {}

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

  onApprove() {
    this.modalService.confirm({
      nzTitle: "Are you sure about this article approval?",
      nzContent: "This cannot be undone",
      nzOnOk: () => this.approve()
    });
  }

  onDecline() {
    this.modalService.confirm({
      nzTitle: "Are you sure about this article deviation?",
      nzContent: "This cannot be undone",
      nzOnOk: () => this.decline()
    });
  }

  approve() {
    if (!this.article.id) {
      this.messageService.error("Something went wrong!");
      return;
    }

    this.articleService
      .approveArticle(this.article.id)
      .subscribe({
        next: () => {
          this.messageService.success("Article was successfully approved");
          this.articleChange.emit();
        },
        error: (error) => {
          this.messageService.error(error.message ?? error);
        }
      });
  }

  decline() {
    if (!this.article.id) {
      this.messageService.error("Something went wrong!");
      return;
    }

    this.articleService
      .declineArticle(this.article.id)
      .subscribe({
        next: () => {
          this.messageService.success("Article was successfully declined");
          this.articleChange.emit();
        },
        error: (error) => {
          this.messageService.error(error.message ?? error);
        }
      });
  }
}
