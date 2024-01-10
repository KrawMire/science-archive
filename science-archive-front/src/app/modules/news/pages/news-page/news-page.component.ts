import { Component, OnInit } from "@angular/core";
import { BehaviorSubject, map } from "rxjs";
import { News } from "@models/news/news";
import { NewsService } from "@services/news.service";

@Component({
  selector: "sar-news-page",
  templateUrl: "./news-page.component.html",
  styleUrls: ["./news-page.component.scss"],
})
export class NewsPageComponent implements OnInit {
  isLoading = true;
  news$ = new BehaviorSubject<News[]>([]);

  constructor(private readonly newsService: NewsService) {}

  ngOnInit(): void {
    this.newsService
      .getAllNews()
      .pipe(
        map((response) => {
          return response.news.map((singleNews) => {
            singleNews.creationDate = new Date(singleNews.creationDate.toString());
            return singleNews;
          });
        }),
      )
      .subscribe({
        complete: () => (this.isLoading = false),
        next: (news) => this.news$.next(news),
      });
  }
}
