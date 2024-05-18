create table if not exists "news".news (
  id                uuid          primary key,
  author_id         uuid          not null,
  title             varchar(255)  not null,
  body              text          not null,
  creation_date     timestamp     not null,
  last_updated_date timestamp     null,
  
  constraint "fk__news__author_id__users__id"
    foreign key (author_id)
    references "user".users (id)
);

create index if not exists "idx__news__title"
  on "news".news(title);