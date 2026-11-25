import { writeFile } from 'node:fs/promises'

const API = 'http://localhost:5151/api'
const token = process.env.TMDB_TOKEN

if (!token) {
  console.error('Falta la variable TMDB_TOKEN')
  process.exit(1)
}

const titulos = await (await fetch(`${API}/titulos`)).json()
const lineas = []

for (const t of titulos) {
  const esSerie = t.tipo === 'Serie'
  const params = new URLSearchParams({
    query: t.nombre,
    language: 'es-ES',
    [esSerie ? 'first_air_date_year' : 'year']: t.anioEstreno,
  })
  const url = `https://api.themoviedb.org/3/search/${esSerie ? 'tv' : 'movie'}?${params}`

  const respuesta = await fetch(url, {
    headers: { Authorization: `Bearer ${token}` },
  })
  const datos = await respuesta.json()
  const poster = datos.results?.[0]?.poster_path

  if (poster) {
    lineas.push(
      `UPDATE titulos SET imagen_url = 'https://image.tmdb.org/t/p/w500${poster}' WHERE id = ${t.id};`
    )
    console.log(`✔ ${t.nombre}`)
  } else {
    console.log(`✘ ${t.nombre} (sin póster)`)
  }
}

await writeFile('database/posters.sql', lineas.join('\n') + '\n')
console.log(`\nListo: ${lineas.length} de ${titulos.length} pósters en database/posters.sql`)