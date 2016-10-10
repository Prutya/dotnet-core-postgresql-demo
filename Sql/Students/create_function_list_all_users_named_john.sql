CREATE OR REPLACE FUNCTION list_all_students_named_john()
RETURNS TABLE (id bigint, first_name varchar, last_name varchar, email varchar)
AS
$$
  SELECT
    id,
    first_name,
    last_name,
    email
  FROM students
  WHERE first_name = 'John'
$$ LANGUAGE SQL;
