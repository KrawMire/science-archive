create table if not exists "category".subcategories (
  id          uuid          primary key,
  name        varchar(255)  not null,
  description varchar(255)  null
);

create index if not exists "idx__subcategories__name"
  on "category".subcategories(name)