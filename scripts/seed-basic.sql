-- Basic seed for FakeRubikStoreAPI
-- Run this after applying the EF migrations.
-- It only creates the minimum catalog data needed by the API.

INSERT INTO "Rol" ("IdRol", "Nombre")
VALUES
    (1, 'ADMIN'),
    (2, 'USER'),
    (3, 'EMPLOYED')
ON CONFLICT ("IdRol") DO NOTHING;

INSERT INTO "Estado" ("Id", "Nombre")
VALUES
    (1, 'NO_ADMITIDO'),
    (2, 'EN_PREPARACION'),
    (3, 'ENVIADO'),
    (4, 'ENTREGADO'),
    (5, 'CANCELADO')
ON CONFLICT ("Id") DO NOTHING;

INSERT INTO "Categorias" ("Id", "Nombre")
VALUES
    (1, 'CUBOS'),
    (2, 'ACCESORIOS'),
    (3, 'REPUESTOS')
ON CONFLICT ("Id") DO NOTHING;

-- Keep identity counters aligned after inserting explicit IDs.
SELECT setval(pg_get_serial_sequence('"Rol"', 'IdRol'), (SELECT COALESCE(MAX("IdRol"), 1) FROM "Rol"));
SELECT setval(pg_get_serial_sequence('"Estado"', 'Id'), (SELECT COALESCE(MAX("Id"), 1) FROM "Estado"));
SELECT setval(pg_get_serial_sequence('"Categorias"', 'Id'), (SELECT COALESCE(MAX("Id"), 1) FROM "Categorias"));
