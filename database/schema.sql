CREATE TABLE usuarios (
  id INT AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(50) NOT NULL UNIQUE,
  password VARCHAR(255) NOT NULL
);

CREATE TABLE tipos (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE generos (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE titulos (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(150) NOT NULL,
  tipo_id INT NOT NULL,
  anio_estreno SMALLINT NOT NULL,
  sinopsis TEXT,
  imagen_url VARCHAR(255),
  FOREIGN KEY (tipo_id) REFERENCES tipos(id)
);

CREATE TABLE titulo_genero (
  titulo_id INT NOT NULL,
  genero_id INT NOT NULL,
  PRIMARY KEY (titulo_id, genero_id),
  FOREIGN KEY (titulo_id) REFERENCES titulos(id),
  FOREIGN KEY (genero_id) REFERENCES generos(id)
);

CREATE TABLE listas (
  id INT AUTO_INCREMENT PRIMARY KEY,
  usuario_id INT NOT NULL,
  nombre VARCHAR(100) NOT NULL,
  FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

CREATE TABLE lista_titulo (
  lista_id INT NOT NULL,
  titulo_id INT NOT NULL,
  fecha_agregado DATETIME DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (lista_id, titulo_id),
  FOREIGN KEY (lista_id) REFERENCES listas(id) ON DELETE CASCADE,
  FOREIGN KEY (titulo_id) REFERENCES titulos(id)
);