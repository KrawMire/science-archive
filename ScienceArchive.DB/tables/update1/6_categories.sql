create table if not exists "category".categories (
  id          uuid          primary key,
  name        varchar(255)  not null,
  description varchar(255)  null
);

create index if not exists "idx__categories__name"
  on "category".categories(name)