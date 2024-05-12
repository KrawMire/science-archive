create table if not exists "article".articles (
  id            uuid          primary key,
  category_id   uuid          not null,
  title         varchar(255)  not null,
  status        smallint      not null,
  creation_date timestamp     not null,
  description   text          null,
  
  constraint "fk__articles__category_id"
    foreign key (category_id)
    references "category".subcategories(id)
);

create index if not exists "idx__articles__title"
  on "article".articles(title);