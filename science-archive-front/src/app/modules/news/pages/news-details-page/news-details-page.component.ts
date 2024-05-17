import { Component, OnInit } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { ActivatedRoute } from "@angular/router";
import { Title } from "@angular/platform-browser";
import { News } from "@models/news/news";
import { NewsApiService } from "@services/news-api.service";

@Component({
  selector: 'sar-news-details-page',
  templateUrl: './news-details-page.component.html',
  styleUrls: ['./news-details-page.component.scss']
})
export class NewsDetailsPageComponent implements OnInit {
  news$ = new BehaviorSubject<News | null>(null);
  isLoading$ = new BehaviorSubject<boolean>(true);
  error$ = new BehaviorSubject<number | null>(null);

  constructor(
    private readonly router: ActivatedRoute,
    private readonly newsService: NewsApiService,
    private readonly titleService: Title) {}

  ngOnInit(): void {
    window.scrollTo(0, 0);
    const newsId = this.router.snapshot.paramMap.get("id");

    if (!newsId) {
      this.error$.next(400);
      return;
    }

    this.newsService
      .getNewsById(newsId)
      .subscribe({
        complete: () => (this.isLoading$.next(true)),
        next: (response) => {
          this.news$.next(response.news);
          this.titleService.setTitle(`Science Archive - ${response.news.title}`);
        },
        error: (error) => {
          this.error$.next(error.status);
          this.isLoading$.next(false);
          this.titleService.setTitle("Unknown news");
        }
      });
  }
}
