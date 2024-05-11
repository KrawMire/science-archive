create table if not exists "user".users (
  id    uuid          primary key,
  name  varchar(255)  not null,
  email varchar(255)  not null,
  login varchar(255)  not null,
  about text          null
);

create index if not exists "idx__users__login"
  on "user".users(login);

create index if not exists "idx__users__email"
  on "user".users(email);