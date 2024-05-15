import { Component, OnInit } from "@angular/core";
import { BehaviorSubject, map } from "rxjs";
import { News } from "@models/news/news";
import { NewsApiService } from "@services/news-api.service";
import { NzMessageService } from "ng-zorro-antd/message";
import { Title } from "@angular/platform-browser";

@Component({
  selector: "sar-news-page",
  templateUrl: "./news-page.component.html",
  styleUrls: ["./news-page.component.scss"],
})
export class NewsPageComponent implements OnInit {
  isLoading$ = new BehaviorSubject<boolean>(true);
  news$ = new BehaviorSubject<News[]>([]);

  constructor(
    private readonly newsService: NewsApiService,
    private readonly message: NzMessageService,
    titleService: Title) {
    titleService.setTitle("Science Archive - News");
  }

  ngOnInit(): void {
    this.newsService
      .getAllNews()
      .pipe(
        map((response) => {
          return response.news.map((singleNews) => {
            singleNews.creationDate = new Date(singleNews.creationDate?.toString() ?? new Date());
            return singleNews;
          });
        }),
      )
      .subscribe({
        complete: () => (this.isLoading$.next(false)),
        next: (news) => this.news$.next(news),
        error: (error) => {
          this.isLoading$.next(false);
          this.news$.next([]);
          console.log(error);
          this.message.error("Unhandled error occurred.");
        },
      });
  }
}
