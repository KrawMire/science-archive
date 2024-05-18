create table if not exists "category".subcategories (
  id          uuid          primary key,
  category_id uuid          not null,
  name        varchar(255)  not null,
  description varchar(255)  null,
  
  constraint "fk_subcategories__category_id"
    foreign key (category_id)
    references "category".categories(id)
);

create index if not exists "idx__subcategories__name"
  on "category".subcategories(name)