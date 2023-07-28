CREATE OR REPLACE FUNCTION "func_create_news" (
  "p_id"                UUID,
  "p_title"             VARCHAR(255),
  "p_body"              TEXT,
  "p_user_id"           UUID,
  "p_created_timestamp" TIMESTAMP WITH TIME ZONE
)
RETURNS TABLE (
  "id"            UUID,
  "title"         VARCHAR(255),
  "body"          TEXT,
  "creation_date" TIMESTAMP WITH TIME ZONE
)
LANGUAGE plpgsql
AS $$
BEGIN
  CALL "proc_create_news" (
    "p_id",
    "p_title",
    "p_body",
    "p_user_id",
    "p_created_timestamp"
  );
  
  RETURN QUERY 
  SELECT
    n."id",
    n."title",
    n."body",
    nc."created_timestamp" AS "creation_date"
  FROM "news" AS n
    INNER JOIN "news_creation" AS nc on n.id = nc.news_id
  WHERE n."id" = "p_id";
END;$$