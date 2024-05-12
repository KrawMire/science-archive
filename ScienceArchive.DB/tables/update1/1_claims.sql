create table if not exists "auth".claims (
  id          uuid         primary key,
  value       varchar(100) not null,
  description varchar(255) null
);

create index if not exists "idx__claims__value"
  on "auth".claims (value);