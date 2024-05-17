import { Component, EventEmitter, Input, Output } from "@angular/core";
import { NzCardComponent, NzCardMetaComponent } from "ng-zorro-antd/card";
import { NzIconDirective } from "ng-zorro-antd/icon";
import { NzTagComponent } from "ng-zorro-antd/tag";
import { SharedModule } from "@modules/shared/shared.module";
import { Article } from "@models/article/article";
import { NzModalService } from "ng-zorro-antd/modal";
import { ArticleApiService } from "@services/article-api.service";
import { NzMessageService } from "ng-zorro-antd/message";
import { NzI18nPipe, NzI18nService } from "ng-zorro-antd/i18n";

@Component({
  selector: 'sar-admin-article-card',
  standalone: true,
  imports: [
    NzCardComponent,
    NzCardMetaComponent,
    NzIconDirective,
    NzTagComponent,
    SharedModule,
    NzI18nPipe
  ],
  templateUrl: './admin-article-card.component.html',
  styleUrl: './admin-article-card.component.scss'
})
export class AdminArticleCardComponent {
  @Input() style?: string;
  @Input() article!: Article;
  @Output() articleChange = new EventEmitter<void>();

  constructor(
    private readonly i18nService: NzI18nService,
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
      return this.i18nService.translate('userArticleCard.onProcessing');
    } else if (this.article.status === 1) {
      return this.i18nService.translate('userArticleCard.published');
    } else {
      return this.i18nService.translate('userArticleCard.declined');
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
      nzTitle: this.i18nService.translate("adminArticleCard.sureApproval"),
      nzContent: this.i18nService.translate("adminArticleCard.cannotBeUndone"),
      nzOnOk: () => this.approve()
    });
  }

  onDecline() {
    this.modalService.confirm({
      nzTitle: this.i18nService.translate("adminArticleCard.sureDeviation"),
      nzContent: this.i18nService.translate("adminArticleCard.cannotBeUndone"),
      nzOnOk: () => this.decline()
    });
  }

  approve() {
    if (!this.article.id) {
      this.messageService.error(this.i18nService.translate("commonErrors.articleIdNotPresent"));
      return;
    }

    this.articleService
      .approveArticle(this.article.id)
      .subscribe({
        next: () => {
          this.messageService.success(this.i18nService.translate("adminArticleCard.articleApproved"));
          this.articleChange.emit();
        },
        error: (error) => {
          this.messageService.error(error.message ?? error);
        }
      });
  }

  decline() {
    if (!this.article.id) {
      this.messageService.error(this.i18nService.translate("commonErrors.articleIdNotPresent"));
      return;
    }

    this.articleService
      .declineArticle(this.article.id)
      .subscribe({
        next: () => {
          this.messageService.success("adminArticleCard.articleDeclined");
          this.articleChange.emit();
        },
        error: (error) => {
          this.messageService.error(error.message ?? error);
        }
      });
  }
}
