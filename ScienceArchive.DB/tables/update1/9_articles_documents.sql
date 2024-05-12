create table if not exists "article".articles_documents (
  id          uuid          primary key,
  article_id  uuid          not null,
  name        varchar(255)  not null,
  filepath    varchar(255)  not null,
  
  constraint "fk__articles_documents__article_id__articles__id"
    foreign key (article_id)
    references "article".articles(id)
);