CREATE TABLE users
(
    id serial PRIMARY KEY,
    fname VARCHAR (50) NOT NULL,
    lname VARCHAR (50) NOT NULL
);

CREATE TABLE addresses
(
    id serial PRIMARY KEY,
    address1 VARCHAR (150) NOT NULL,
    address2 VARCHAR (150) NOT NULL,
    city VARCHAR (50) NOT NULL,
    state VARCHAR (2) NOT NULL,
    zip VARCHAR (5) NOT NULL
);