CREATE TABLE IF NOT EXISTS students(
  id     bigserial    PRIMARY KEY,
  name   varchar(500) NOT NULL,
  email  varchar(256) NOT NULL UNIQUE
);
