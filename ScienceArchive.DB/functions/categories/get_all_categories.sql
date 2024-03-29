DROP FUNCTION IF EXISTS "func_get_all_categories";

CREATE OR REPLACE FUNCTION "func_get_all_categories" ()
RETURNS TABLE (
  "id"            UUID,
  "name"          VARCHAR(255),
  "subcategories" JSONB  
)
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY 
    SELECT
      c."id",
      c."name",
      (
        SELECT
          jsonb_agg(
            json_build_object(
              'id',   sc."id",
              'name', sc."name"
            )
          )
        FROM "subcategories" as sc
        WHERE sc."category_id" = c."id"
      )
    FROM "categories" as c;
END;$$
