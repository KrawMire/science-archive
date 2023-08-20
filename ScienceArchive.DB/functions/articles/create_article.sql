CREATE OR REPLACE FUNCTION "func_create_article" (
  "p_id"                 UUID,
  "p_category_id"        UUID,
  "p_title"              VARCHAR(255),
  "p_description"        TEXT,
  "p_creation_date"      TIMESTAMP WITH TIME ZONE,
  "p_authors_ids"        UUID[],
  "p_documents"          JSONB
)
RETURNS TABLE (
  "id"                UUID,
  "categoryId"        UUID,
  "title"             VARCHAR(255),
  "description"       TEXT,
  "creationDate"      TIMESTAMP WITH TIME ZONE,
  "authorsIds"        UUID[],
  "documents"         JSONB
)
LANGUAGE plpgsql
AS $$
BEGIN 
  CALL "proc_create_article" (
    "p_id",
    "p_category_id",
    "p_authors_ids",
    "p_title",
    "p_creation_date",
    "p_description",
    "p_documents"
  );
  
  RETURN QUERY
    SELECT *
    FROM func_get_article_by_id("p_id");
END;$$