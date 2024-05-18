create table if not exists "article".users_articles (
  user_id     uuid      not null,
  article_id  uuid      not null,
  role        smallint  not null,
  
  primary key (user_id, article_id),
  
  constraint "fk__users_articles__user_id__users__id"
    foreign key (user_id)
    references "user".users(id),
  constraint "fk__users_articles__article_id__articles__id"
    foreign key (article_id)
    references "article".articles(id)
);