-- Mock data for "auth".claims
INSERT INTO "auth".claims (id, value, description)
VALUES ('a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Claim 1', 'This is claim 1'),
       ('b0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Claim 2', 'This is claim 2');

-- Mock data for "auth".roles
INSERT INTO "auth".roles (id, name, description)
VALUES ('a1eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Role 1', 'This is role 1'),
       ('b1eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Role 2', 'This is role 2');

-- Mock data for "user".users
INSERT INTO "user".users (id, name, email, login, about)
VALUES ('a2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'User 1', 'user1@example.com', 'user1', 'About user 1'),
       ('b2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'User 2', 'user2@example.com', 'user2', 'About user 2');


-- Mock data for "auth".users_auth
INSERT INTO "auth".users_auth (user_id, password, password_salt)
VALUES ('a2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'password123', 'salt123'),
       ('b2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'password456', 'salt456');

-- Mock data for "news".news
INSERT INTO "news".news (id, author_id, title, body, creation_date, last_updated_date)
VALUES ('c2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'a2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'News Title 1',
        'This is news body 1', CURRENT_DATE, null),
       ('d2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'b2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'News Title 2',
        'This is news body 2', CURRENT_DATE, null);

-- Mock data for "category".categories
INSERT INTO "category".categories (id, name, description)
VALUES ('e2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Category 1', 'This is category 1'),
       ('f2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Category 2', 'This is category 2');

-- Mock data for "category".subcategories
INSERT INTO "category".subcategories (id, name, description)
VALUES ('12eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Subcategory 1', 'This is subcategory 1'),
       ('22eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Subcategory 2', 'This is subcategory 2');

-- Mock data for "article".articles
INSERT INTO "article".articles (id, category_id, title, status, creation_date, description)
VALUES ('32eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', '12eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Article Title 1', 1,
        CURRENT_DATE, 'This is article description 1'),
       ('42eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', '22eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Article Title 2', 2,
        CURRENT_DATE, 'This is article description 2');

-- Mock data for "article".articles_documents
INSERT INTO "article".articles_documents (id, article_id, name, filepath)
VALUES ('52eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', '42eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Document 1',
        '/path/to/document1'),
       ('62eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', '32eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'Document 2',
        '/path/to/document2');

-- Mock data for "auth".roles_claims
INSERT INTO "auth".roles_claims (claim_id, role_id)
VALUES ('a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'a1eebc99-9c0b-4ef8-bb6d-6bb9bd380a11'),
       ('b0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 'b1eebc99-9c0b-4ef8-bb6d-6bb9bd380a11');

-- Mock data for "article".users_articles
INSERT INTO "article".users_articles (user_id, article_id, role)
VALUES ('a2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', '32eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 1),
       ('b2eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', '42eebc99-9c0b-4ef8-bb6d-6bb9bd380a11', 2);