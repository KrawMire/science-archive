DROP FUNCTION IF EXISTS "func_get_news_by_id";

CREATE OR REPLACE FUNCTION "func_get_news_by_id" (
  "p_id" UUID
)
RETURNS TABLE (
  "id"                UUID,
  "authorId"          UUID,
  "title"             VARCHAR(255),
  "body"              TEXT,
  "creationDate"      TIMESTAMP
)
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY
    SELECT
      n."id",
      nc."author_id"         AS "authorId",
      n."title",
      n."body",
      nc."created_timestamp" AS "creationDate"
    FROM "news"                  AS n
      INNER JOIN "news_creation" AS nc on n.id = nc.news_id
    WHERE n."id" = "p_id";
END;$$