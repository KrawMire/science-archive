DROP FUNCTION IF EXISTS "func_delete_role";

CREATE OR REPLACE FUNCTION "func_delete_role" (
  "p_id" UUID
)
RETURNS UUID
LANGUAGE plpgsql
AS $$
BEGIN
  CALL "proc_delete_role"("p_id");
  RETURN "p_id";
END;$$