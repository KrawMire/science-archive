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
  is_confirmed  boolean       not null,
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
  category_id uuid          not null,
  name        varchar(255)  not null,
  description varchar(255)  null,

  constraint "fk_subcategories__category_id"
    foreign key (category_id)
    references "category".categories(id)
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
create table if not exists "auth".users_roles (
  user_id uuid not null,
  role_id uuid not null,

  primary key(user_id, role_id),

  constraint "fk__users_roles__user_id"
    foreign key (user_id)
    references "user".users(id),
  constraint "fk__users_roles__role_id"
    foreign key (role_id)
    references "auth".roles(id)
);

-- -----------------------------------------------------
-- Import data from old tables to new
-- -----------------------------------------------------

insert into "user".users (id, name, email, login, about)
select
    u.id,
    u.name,
    u.email,
    ua.login,
    ''
from users as u
    join users_auth as ua on ua.user_id = u.id;

insert into "auth".users_auth (user_id, is_confirmed, password, password_salt)
select
    ua.user_id,
    false,
    ua.password,
    ua.password_salt
from users_auth as ua;

insert into "news".news (id, author_id, title, body, creation_date, last_updated_date)
select
    n.id,
    nc.author_id,
    n.title,
    n.body,
    nc.created_timestamp,
    null
from news as n
    join news_creation as nc on nc.news_id = n.id;

insert into "category".categories (id, name, description)
select
    c.id,
    c.name,
    c.name
from categories as c;

insert into "category".subcategories (id, category_id, name, description)
select
    s.id,
    s.category_id,
    s.name,
    s.name
from subcategories as s;

insert into "article".articles (id, category_id, title, status, creation_date, description)
select
    a.id,
    ac.subcategory_id,
    a.title,
    av.status,
    acr.created_timestamp,
    a.description
from articles as a
    join articles_categories as ac on ac.article_id = a.id
    join articles_verification as av on av.article_id = a.id
    join articles_creation as acr on acr.article_id = a.id;

insert into "article".articles_documents (id, article_id, name, filepath)
select
    gen_random_uuid(),
    ad.article_id,
    'Linked article file',
    ad.document_path
from articles_documents as ad;

insert into "article".users_articles (user_id, article_id, role)
select
    aa.author_id,
    aa.article_id,
    0
from articles_authors as aa;

-- -----------------------------------------------------
-- Insert necessary data
-- -----------------------------------------------------

INSERT INTO auth.claims (id, description, value)
VALUES
    ('ae8d6f00-07a5-4a7e-b26a-ecef373d5216', 'Description for VIEW_NOT_VERIFIED_ARTICLES', 'VIEW_NOT_VERIFIED_ARTICLES'),
    ('24ed5057-487d-4887-8ee8-7342a04c7695', 'Description for VIEW_DECLINED_ARTICLES', 'VIEW_DECLINED_ARTICLES'),
    ('718c32b7-122b-4a62-a635-ef485f8d84cc', 'Description for APPROVE_ARTICLES', 'APPROVE_ARTICLES'),
    ('9aab0940-4cf0-4f53-a5b2-8f8fe46fc64f', 'Description for DECLINE_ARTICLES', 'DECLINE_ARTICLES'),
    ('f5934e5e-cc42-45b9-8151-094066968214', 'Description for ACCESS_ADMIN_PAGE', 'ACCESS_ADMIN_PAGE');