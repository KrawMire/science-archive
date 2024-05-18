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