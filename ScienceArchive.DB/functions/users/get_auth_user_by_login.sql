DROP FUNCTION IF EXISTS "func_get_auth_user_by_login";

CREATE OR REPLACE FUNCTION "func_get_auth_user_by_login" (
  "p_login"         varchar(255)
)
RETURNS TABLE
(
  "id"            UUID,
  "name"          VARCHAR(100),
  "email"         VARCHAR(50),
  "login"         VARCHAR(30),
  "password"      VARCHAR(255),
  "passwordSalt" VARCHAR(255),
  "rolesIds"      UUID[]
)
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY
  SELECT
      u."id",
      u."name",
      u."email",
      ua."login",
      ua."password",
      ua."password_salt" as "passwordSalt",
      (
          SELECT
              array_agg(ur."role_id")
          FROM "users_roles" AS ur
          WHERE ur."user_id" = u."id"
      )
  FROM "users"              AS u
    INNER JOIN "users_auth" AS ua ON ua."user_id" = u."id"
  WHERE (
    ua."login" = "p_login"
    or
    u."email" = "p_login"
  );
END;$$