import { Component } from "@angular/core";
import { AuthorUser } from "@models/user/author-user";

@Component({
  selector: "app-authors-page",
  templateUrl: "./authors-page.component.html",
  styleUrls: ["./authors-page.component.scss"],
})
export class AuthorsPageComponent {
  authors: AuthorUser[] = [
    {
      id: "dksabkdjnajnjdkjkasjda",
      name: "Jane Doe",
      description: "Data scientist",
      articlesCount: 10,
    },
    {
      id: "dksabkdjnajnjdkjkasjda",
      name: "Jane Doe",
      description: "Data scientist",
      articlesCount: 10,
    },
    {
      id: "dksabkdjnajnjdkjkasjda",
      name: "Jane Doe",
      description: "Data scientist",
      articlesCount: 10,
    },
    {
      id: "dksabkdjnajnjdkjkasjda",
      name: "Jane Doe",
      description: "Data scientist",
      articlesCount: 10,
    },
    {
      id: "dksabkdjnajnjdkjkasjda",
      name: "Jane Doe",
      description: "Data scientist",
      articlesCount: 10,
    },
  ];
}
