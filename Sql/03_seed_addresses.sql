INSERT INTO addresses
VALUES
    (1, '123 Anywhere St.', 'C/O Brandon Atkinson', 'Richmond', 'VA', '23236');
INSERT INTO addresses
VALUES
    (2, '456 Somewhere St.', 'C/O John Doe', 'Chesterfield', 'VA', '23456');
INSERT INTO addresses
VALUES
    (3, '789 Nowhere St.', 'C/O Jane Doe', 'Midlothian', 'VA', '23456');

ALTER SEQUENCE addresses_id_seq RESTART WITH 4;