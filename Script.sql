CREATE DATABASE "RealPlazaDB";


CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(100) NOT NULL UNIQUE,
    passwordhash VARCHAR(200) NOT NULL,
    email VARCHAR(150) NOT NULL,
    birthdate DATE NOT NULL,
    isactive BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO users(username, passwordhash, email, birthdate, isactive)
VALUES ('admin', '123456', 'admin@realplaza.com', '2026-02-10', true);
