docker run -d --name oracle-xe \
  -p 1521:1521 -p 5500:5500 \
  -e ORACLE_PASSWORD=MyStrongP@ssw0rdKPT \
  gvenzl/oracle-xe:21-slim


# Container içine gir
docker exec -it oracle-xe bash
# SYSDBA ile bağlan
sqlplus / as sysdba

-- XEPDB1'e geç
ALTER SESSION SET CONTAINER = XEPDB1;

-- Uygulama kullanıcısı ve yetkiler
CREATE USER app IDENTIFIED BY "AppP@ssw0rd" QUOTA UNLIMITED ON USERS;
GRANT CREATE SESSION, CREATE TABLE, CREATE SEQUENCE, CREATE VIEW, CREATE PROCEDURE,
      UNLIMITED TABLESPACE TO app;
EXIT;
