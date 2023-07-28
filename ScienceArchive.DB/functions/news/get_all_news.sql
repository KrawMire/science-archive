CREATE OR REPLACE FUNCTION "func_get_all_news" ()
RETURNS TABLE (
  "id"            UUID,
  "title"         VARCHAR(255),
  "body"          TEXT,
  "creation_date" TIMESTAMP WITH TIME ZONE
)
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY
  SELECT
    n."id",
    n."title",
    n."body",
    nc."created_timestamp" AS "creation_date"
  FROM "news"                  AS n
    INNER JOIN "news_creation" AS nc on n.id = nc.news_id;
END;$$