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