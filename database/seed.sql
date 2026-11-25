INSERT INTO tipos (nombre) VALUES ('Película'), ('Serie');

INSERT INTO generos (nombre) VALUES
('Acción'), ('Comedia'), ('Drama'), ('Terror'), ('Ciencia ficción'),
('Fantasía'), ('Thriller'), ('Misterio'), ('Crimen'), ('Romance'), ('Superhéroes');

INSERT INTO titulos (nombre, tipo_id, anio_estreno, sinopsis) VALUES
('Stranger Things', (SELECT id FROM tipos WHERE nombre='Serie'), 2016, 'Un grupo de niños se enfrenta a fuerzas sobrenaturales en un pueblo de los 80.'),
('The Witcher', (SELECT id FROM tipos WHERE nombre='Serie'), 2019, 'Un cazador de monstruos recorre un mundo de magia y política.'),
('Miércoles', (SELECT id FROM tipos WHERE nombre='Serie'), 2022, 'Miércoles Addams investiga un misterio en su nuevo internado.'),
('El juego del calamar', (SELECT id FROM tipos WHERE nombre='Serie'), 2021, 'Personas endeudadas compiten en juegos infantiles mortales por un gran premio.'),
('La casa de papel', (SELECT id FROM tipos WHERE nombre='Serie'), 2017, 'Una banda planea el mayor atraco a la Fábrica de Moneda y Timbre.'),
('Dark', (SELECT id FROM tipos WHERE nombre='Serie'), 2017, 'La desaparición de unos niños revela secretos que cruzan varias generaciones.'),
('Black Mirror', (SELECT id FROM tipos WHERE nombre='Serie'), 2011, 'Historias independientes sobre el lado oscuro de la tecnología.'),
('The Crown', (SELECT id FROM tipos WHERE nombre='Serie'), 2016, 'La vida de la reina Isabel II y los acontecimientos de su reinado.'),
('The Umbrella Academy', (SELECT id FROM tipos WHERE nombre='Serie'), 2019, 'Hermanos con superpoderes se reúnen para evitar el fin del mundo.'),
('Narcos', (SELECT id FROM tipos WHERE nombre='Serie'), 2015, 'El auge y la caída de los grandes cárteles de la droga colombianos.'),
('Ozark', (SELECT id FROM tipos WHERE nombre='Serie'), 2017, 'Un asesor financiero lava dinero para un cártel y se muda con su familia.'),
('Los Bridgerton', (SELECT id FROM tipos WHERE nombre='Serie'), 2020, 'Romances y secretos en la alta sociedad londinense del siglo XIX.'),
('Puñales por la espalda: El misterio de Glass Onion', (SELECT id FROM tipos WHERE nombre='Película'), 2022, 'Un detective investiga un asesinato durante una reunión en una isla privada.'),
('Tyler Rake', (SELECT id FROM tipos WHERE nombre='Película'), 2020, 'Un mercenario debe rescatar al hijo secuestrado de un capo.'),
('El agente invisible', (SELECT id FROM tipos WHERE nombre='Película'), 2022, 'Un asesino de la CIA es perseguido por todo el mundo.'),
('A ciegas', (SELECT id FROM tipos WHERE nombre='Película'), 2018, 'Una madre debe sobrevivir con sus hijos a una amenaza que no se puede mirar.'),
('Enola Holmes', (SELECT id FROM tipos WHERE nombre='Película'), 2020, 'La hermana de Sherlock Holmes busca a su madre desaparecida.'),
('Ejército de los muertos', (SELECT id FROM tipos WHERE nombre='Película'), 2021, 'Un grupo de mercenarios intenta un atraco en un Las Vegas lleno de zombis.'),
('Notificación roja', (SELECT id FROM tipos WHERE nombre='Película'), 2021, 'Un agente del FBI y dos criminales rivales persiguen un tesoro legendario.'),
('No mires arriba', (SELECT id FROM tipos WHERE nombre='Película'), 2021, 'Dos astrónomos intentan avisar al mundo de que un cometa va a chocar con la Tierra.');

INSERT INTO titulo_genero (titulo_id, genero_id)
SELECT t.id, g.id FROM titulos t, generos g
WHERE (t.nombre, g.nombre) IN (
  ('Stranger Things','Ciencia ficción'), ('Stranger Things','Terror'), ('Stranger Things','Drama'),
  ('The Witcher','Fantasía'), ('The Witcher','Acción'),
  ('Miércoles','Comedia'), ('Miércoles','Misterio'), ('Miércoles','Fantasía'),
  ('El juego del calamar','Thriller'), ('El juego del calamar','Drama'),
  ('La casa de papel','Acción'), ('La casa de papel','Crimen'), ('La casa de papel','Thriller'),
  ('Dark','Ciencia ficción'), ('Dark','Misterio'), ('Dark','Drama'),
  ('Black Mirror','Ciencia ficción'), ('Black Mirror','Thriller'), ('Black Mirror','Drama'),
  ('The Crown','Drama'),
  ('The Umbrella Academy','Superhéroes'), ('The Umbrella Academy','Ciencia ficción'), ('The Umbrella Academy','Acción'),
  ('Narcos','Crimen'), ('Narcos','Drama'),
  ('Ozark','Crimen'), ('Ozark','Thriller'), ('Ozark','Drama'),
  ('Los Bridgerton','Romance'), ('Los Bridgerton','Drama'),
  ('Puñales por la espalda: El misterio de Glass Onion','Misterio'), ('Puñales por la espalda: El misterio de Glass Onion','Comedia'), ('Puñales por la espalda: El misterio de Glass Onion','Crimen'),
  ('Tyler Rake','Acción'), ('Tyler Rake','Thriller'),
  ('El agente invisible','Acción'), ('El agente invisible','Thriller'),
  ('A ciegas','Terror'), ('A ciegas','Thriller'), ('A ciegas','Ciencia ficción'),
  ('Enola Holmes','Misterio'), ('Enola Holmes','Comedia'),
  ('Ejército de los muertos','Terror'), ('Ejército de los muertos','Acción'),
  ('Notificación roja','Acción'), ('Notificación roja','Comedia'), ('Notificación roja','Crimen'),
  ('No mires arriba','Comedia'), ('No mires arriba','Ciencia ficción'), ('No mires arriba','Drama')
);

UPDATE titulos
SET imagen_url = CONCAT('https://placehold.co/300x450?text=', REPLACE(nombre, ' ', '+'));