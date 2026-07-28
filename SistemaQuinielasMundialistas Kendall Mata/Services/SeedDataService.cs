using SistemaQuinielasMundialistas.Models;
using SistemaQuinielasMundialistas.Repositories;

namespace SistemaQuinielasMundialistas.Services
{
    public static class SeedDataService
    {
        private static readonly string[] Paises = { "Costa Rica", "Argentina", "Brasil", "México", "España", "Francia", "Alemania", "Japón", "Canadá", "Portugal" };
        public static void InicializarSiEsNecesario()
        {
            var ur = new JsonRepository<Usuario>("usuarios.json");
            var pr = new JsonRepository<Partido>("partidos.json");
            var rr = new JsonRepository<Pronostico>("pronosticos.json");
            var qr = new JsonRepository<Quiniela>("quinielas.json");
            var usuarios = ur.GetAll();
            var partidos = pr.GetAll();
            var pronosticos = rr.GetAll();
            var quinielas = qr.GetAll();
            if (usuarios.Count < 40)
            {
                for (int i = usuarios.Count + 1; i <= 40; i++) usuarios.Add(new Usuario { Id = i, Nombre = $"Aficionado {i}", Correo = $"aficionado{i}@correo.com", NombreUsuario = $"usuario{i}", Contrasena = "Mundial2026", PaisPreferido = Paises[(i - 1) % Paises.Length] });
                ur.SaveAll(usuarios);
            }
            if (partidos.Count < 12)
            {
                partidos.Clear();
                for (int i = 0; i < 12; i++) partidos.Add(new Partido { Id = i + 1, EquipoLocal = Paises[i % Paises.Length], EquipoVisitante = Paises[(i + 3) % Paises.Length], FechaHora = DateTime.Now.AddDays(i - 6), Estado = i < 6 ? "Finalizado" : "Próximo", GolesLocal = i < 6 ? i % 4 : 0, GolesVisitante = i < 6 ? (i + 1) % 3 : 0, Anotadores = i < 6 ? "Datos simulados" : string.Empty });
                pr.SaveAll(partidos);
            }

            // Partidos de demostración para las tablas de posiciones.
            // Se agregan una sola vez y no reemplazan los partidos/pronósticos existentes.
            if (!partidos.Any(p => !string.IsNullOrWhiteSpace(p.Grupo)))
            {
                DateTime inicioGrupos = new DateTime(2026, 7, 10, 14, 0, 0);
                int siguienteId = partidos.Count == 0 ? 1 : partidos.Max(p => p.Id) + 1;

                var partidosGrupo = new List<Partido>
                {
                    CrearPartidoGrupo(siguienteId++, "A", "Costa Rica", "México", inicioGrupos, 2, 1),
                    CrearPartidoGrupo(siguienteId++, "A", "Japón", "Alemania", inicioGrupos.AddHours(3), 1, 1),
                    CrearPartidoGrupo(siguienteId++, "A", "Costa Rica", "Japón", inicioGrupos.AddDays(2), 1, 0),
                    CrearPartidoGrupo(siguienteId++, "A", "México", "Alemania", inicioGrupos.AddDays(2).AddHours(3), 2, 2),
                    CrearPartidoGrupo(siguienteId++, "A", "Costa Rica", "Alemania", inicioGrupos.AddDays(4), 0, 1),
                    CrearPartidoGrupo(siguienteId++, "A", "México", "Japón", inicioGrupos.AddDays(4).AddHours(3), 3, 1),

                    CrearPartidoGrupo(siguienteId++, "B", "Argentina", "Brasil", inicioGrupos.AddDays(1), 2, 2),
                    CrearPartidoGrupo(siguienteId++, "B", "Francia", "Canadá", inicioGrupos.AddDays(1).AddHours(3), 1, 0),
                    CrearPartidoGrupo(siguienteId++, "B", "Argentina", "Francia", inicioGrupos.AddDays(3), 1, 2),
                    CrearPartidoGrupo(siguienteId++, "B", "Brasil", "Canadá", inicioGrupos.AddDays(3).AddHours(3), 3, 0),
                    CrearPartidoGrupo(siguienteId++, "B", "Argentina", "Canadá", inicioGrupos.AddDays(5), 2, 0),
                    CrearPartidoGrupo(siguienteId++, "B", "Brasil", "Francia", inicioGrupos.AddDays(5).AddHours(3), 1, 1),

                    CrearPartidoGrupo(siguienteId++, "C", "España", "Portugal", inicioGrupos.AddDays(1), 1, 0),
                    CrearPartidoGrupo(siguienteId++, "C", "Países Bajos", "Uruguay", inicioGrupos.AddDays(1).AddHours(3), 2, 1),
                    CrearPartidoGrupo(siguienteId++, "C", "España", "Países Bajos", inicioGrupos.AddDays(3), 2, 2),
                    CrearPartidoGrupo(siguienteId++, "C", "Portugal", "Uruguay", inicioGrupos.AddDays(3).AddHours(3), 1, 0),
                    CrearPartidoGrupo(siguienteId++, "C", "España", "Uruguay", inicioGrupos.AddDays(5), 3, 0),
                    CrearPartidoGrupo(siguienteId++, "C", "Portugal", "Países Bajos", inicioGrupos.AddDays(5).AddHours(3), 1, 2),

                    CrearPartidoGrupo(siguienteId++, "D", "Estados Unidos", "Colombia", inicioGrupos.AddDays(2), 2, 0),
                    CrearPartidoGrupo(siguienteId++, "D", "Uruguay", "Corea del Sur", inicioGrupos.AddDays(2).AddHours(3), 1, 1),
                    CrearPartidoGrupo(siguienteId++, "D", "Estados Unidos", "Uruguay", inicioGrupos.AddDays(4), 1, 2),
                    CrearPartidoGrupo(siguienteId++, "D", "Colombia", "Corea del Sur", inicioGrupos.AddDays(4).AddHours(3), 2, 1),
                    CrearPartidoGrupo(siguienteId++, "D", "Estados Unidos", "Corea del Sur", inicioGrupos.AddDays(6), 3, 0),
                    CrearPartidoGrupo(siguienteId++, "D", "Colombia", "Uruguay", inicioGrupos.AddDays(6).AddHours(3), 1, 0)
                };

                partidos.AddRange(partidosGrupo);
                pr.SaveAll(partidos);
            }


            // Migración para versiones anteriores que ya tenían A-C, pero aún no el Grupo D.
            if (!partidos.Any(p => string.Equals(p.Grupo, "D", StringComparison.OrdinalIgnoreCase)))
            {
                DateTime inicioGrupoD = new DateTime(2026, 7, 12, 14, 0, 0);
                int siguienteIdD = partidos.Count == 0 ? 1 : partidos.Max(p => p.Id) + 1;
                partidos.AddRange(new[]
                {
                    CrearPartidoGrupo(siguienteIdD++, "D", "Estados Unidos", "Colombia", inicioGrupoD, 2, 0),
                    CrearPartidoGrupo(siguienteIdD++, "D", "Uruguay", "Corea del Sur", inicioGrupoD.AddHours(3), 1, 1),
                    CrearPartidoGrupo(siguienteIdD++, "D", "Estados Unidos", "Uruguay", inicioGrupoD.AddDays(2), 1, 2),
                    CrearPartidoGrupo(siguienteIdD++, "D", "Colombia", "Corea del Sur", inicioGrupoD.AddDays(2).AddHours(3), 2, 1),
                    CrearPartidoGrupo(siguienteIdD++, "D", "Estados Unidos", "Corea del Sur", inicioGrupoD.AddDays(4), 3, 0),
                    CrearPartidoGrupo(siguienteIdD++, "D", "Colombia", "Uruguay", inicioGrupoD.AddDays(4).AddHours(3), 1, 0)
                });
                pr.SaveAll(partidos);
            }

            if (pronosticos.Count < 100)
            {
                pronosticos.Clear(); int id = 1; var random = new Random(2026);
                foreach (var usuario in usuarios.Take(35))
                    foreach (var partido in partidos.Take(6))
                    {
                        var p = new Pronostico { Id = id++, NombreUsuario = usuario.NombreUsuario, PartidoId = partido.Id, EquipoLocal = partido.EquipoLocal, EquipoVisitante = partido.EquipoVisitante, GolesLocalPronosticados = random.Next(0, 4), GolesVisitantePronosticados = random.Next(0, 4), FechaRegistro = partido.FechaHora.AddDays(-2) };
                        p.PuntosObtenidos = new PronosticoService().CalcularPuntos(p, partido);
                        pronosticos.Add(p);
                    }
                rr.SaveAll(pronosticos);
            }
            if (quinielas.Count < 5)
            {
                quinielas.Clear();
                for (int i = 1; i <= 5; i++) quinielas.Add(new Quiniela { Id = i, Nombre = $"Quiniela Privada {i}", Descripcion = $"Liga privada mundialista número {i}", EsPrivada = true, ParticipanteIds = usuarios.Skip((i - 1) * 5).Take(12).Select(u => u.Id).ToList(), Timeline = new List<string> { "Quiniela creada y lista para competir." } });
                qr.SaveAll(quinielas);
            }
            new UsuarioService().RecalcularPuntosUsuarios(rr.GetAll());
        }
        private static Partido CrearPartidoGrupo(int id, string grupo, string local, string visitante, DateTime fecha, int golesLocal, int golesVisitante)
        {
            return new Partido
            {
                Id = id,
                Grupo = grupo,
                EquipoLocal = local,
                EquipoVisitante = visitante,
                FechaHora = fecha,
                Estado = "Finalizado",
                GolesLocal = golesLocal,
                GolesVisitante = golesVisitante,
                Anotadores = "Datos simulados para la tabla de grupos"
            };
        }

    }
}
