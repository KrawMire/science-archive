create table if not exists "auth".roles (
  id          uuid          primary key,
  name        varchar(255)  not null,
  description varchar(255)  not null
);

create index if not exists "idx__roles__name"
  on "auth".roles(name);