create table if not exists "auth".users_auth (
  user_id       uuid          primary key,
  password      varchar(255)  not null,
  password_salt varchar(255)  not null,
  
  constraint "fk__users_auth__user_id__users__id"
    foreign key (user_id)
    references "user".users(id)
);