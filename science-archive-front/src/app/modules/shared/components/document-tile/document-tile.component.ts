import { ChangeDetectionStrategy, Component, Input } from "@angular/core";
import { ArticleDocument } from "@models/article/article";
import { documentExtension, documentKind, documentUrl } from "@modules/shared/utils/article-utils";

@Component({
  selector: "sar-document-tile",
  template: `
    <div class="sa-doc">
      <span class="sa-doc__ext" [class.sa-doc__ext--data]="kind === 'data'" [class.sa-doc__ext--other]="kind === 'other'">
        {{ extension || 'file' }}
      </span>
      <div class="sa-doc__body">
        <a class="sa-doc__name" [href]="url" target="_blank" rel="noopener noreferrer" [title]="document.name">{{ document.name }}</a>
        <span class="sa-mono">{{ ('documents.kind.' + kind) | nzI18n }}</span>
      </div>
      <a
        *ngIf="action === 'download'; else openAction"
        class="sa-doc__action"
        [href]="url"
        [attr.download]="document.name"
        [attr.aria-label]="('documents.download' | nzI18n) + ' ' + document.name"
      >
        <sar-icon name="download" [size]="18" />
      </a>
      <ng-template #openAction>
        <a class="sa-btn sa-btn--ghost sa-btn--sm" [href]="url" target="_blank" rel="noopener noreferrer">
          {{ 'documents.open' | nzI18n }}
        </a>
      </ng-template>
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DocumentTileComponent {
  @Input({ required: true }) document!: ArticleDocument;
  @Input() action: "download" | "open" = "download";

  get extension(): string {
    return documentExtension(this.document);
  }

  get kind(): string {
    return documentKind(this.document);
  }

  get url(): string {
    return documentUrl(this.document);
  }
}
