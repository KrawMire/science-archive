DROP FUNCTION IF EXISTS "func_delete_new";

CREATE OR REPLACE FUNCTION "func_delete_news" (
  "p_id" UUID
)
RETURNS UUID
LANGUAGE plpgsql
AS $$
BEGIN
  CALL "proc_delete_news" ("p_id");
  RETURN "p_id";
END;$$