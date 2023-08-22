import { Component, Input } from "@angular/core";
import { AuthorUser } from "@models/user/author-user";
import { Article } from "@models/article/article";

@Component({
  selector: "app-author-card",
  templateUrl: "./author-card.component.html",
  styleUrls: ["./author-card.component.scss"],
})
export class AuthorCardComponent {
  @Input() authorUser: AuthorUser | null = null;
  articles: Article[] = [];
  isArticlesShown: boolean = false;

  toggleShowArticles() {
    this.isArticlesShown = !this.isArticlesShown;

    if (this.isArticlesShown && this.articles.length === 0) {
      this.articles = [
        {
          id: "qiweqwewq-dsadwq-3141ne-321dewqfsaewq",
          authorId: "qiweqwewq-dsadwq-3141ne-321dewqfsaewq",
          creationDate: new Date(),
          description:
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
          title: "Test article",
          documentPath: "/path/to/dir/with/linked/document",
        },
        {
          id: "qiweqwewq-dsadwq-3141ne-321dewqfsaewq",
          authorId: "qiweqwewq-dsadwq-3141ne-321dewqfsaewq",
          creationDate: new Date(),
          description:
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
          title: "Test article",
          documentPath: "/path/to/dir/with/linked/document",
        },
        {
          id: "qiweqwewq-dsadwq-3141ne-321dewqfsaewq",
          authorId: "qiweqwewq-dsadwq-3141ne-321dewqfsaewq",
          creationDate: new Date(),
          description:
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
          title: "Test article",
          documentPath: "/path/to/dir/with/linked/document",
        },
        {
          id: "qiweqwewq-dsadwq-3141ne-321dewqfsaewq",
          authorId: "qiweqwewq-dsadwq-3141ne-321dewqfsaewq",
          creationDate: new Date(),
          description:
            "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
          title: "Test article",
          documentPath: "/path/to/dir/with/linked/document",
        },
      ];
    }
  }
}