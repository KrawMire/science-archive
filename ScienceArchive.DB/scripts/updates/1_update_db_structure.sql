-- -----------------------------------------------------
-- Create DB schemas
-- -----------------------------------------------------
create schema "article";
create schema "category";
create schema "news";
create schema "notification";
create schema "user";
create schema "auth";

-- -----------------------------------------------------
-- Create new tables
-- -----------------------------------------------------
create table if not exists "auth".claims (
  id          uuid         primary key,
  value       varchar(100) not null,
  description varchar(255) null
);

create index if not exists "idx__claims__value"
  on "auth".claims (value);

-- -----------------------------------------------------
create table if not exists "auth".roles (
  id          uuid          primary key,
  name        varchar(255)  not null,
  description varchar(255)  not null
);

create index if not exists "idx__roles__name"
  on "auth".roles(name);

-- -----------------------------------------------------
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

-- -----------------------------------------------------
create table if not exists "auth".users_auth (
  user_id       uuid          primary key,
  password      varchar(255)  not null,
  password_salt varchar(255)  not null,
  
  constraint "fk__users_auth__user_id__users__id"
    foreign key (user_id)
     references "user".users(id)
);

-- -----------------------------------------------------
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

-- -----------------------------------------------------
create table if not exists "category".categories (
  id          uuid          primary key,
  name        varchar(255)  not null,
  description varchar(255)  null
);

create index if not exists "idx__categories__name"
  on "category".categories(name);

-- -----------------------------------------------------
create table if not exists "category".subcategories (
  id          uuid          primary key,
  name        varchar(255)  not null,
  description varchar(255)  null
);

create index if not exists "idx__subcategories__name"
  on "category".subcategories(name);

-- -----------------------------------------------------
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

-- -----------------------------------------------------
create table if not exists "article".articles_documents (
  id          uuid          primary key,
  article_id  uuid          not null,
  name        varchar(255)  not null,
  filepath    varchar(255)  not null,
  
  constraint "fk__articles_documents__article_id__articles__id"
    foreign key (article_id)
      references "article".articles(id)
);

-- -----------------------------------------------------
create table if not exists "auth".roles_claims (
  claim_id uuid not null,
  role_id  uuid not null,
  
  primary key (claim_id, role_id),
  
  constraint "fk__roles_claims__claim_id__claims__id"
    foreign key (claim_id)
      references "auth".claims(id),
  constraint "fk__roles_claims__role_id__roles__id"
    foreign key (role_id)
      references "auth".roles(id)
);

-- -----------------------------------------------------
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


-- -----------------------------------------------------
-- Import data from old tables to new
-- -----------------------------------------------------
