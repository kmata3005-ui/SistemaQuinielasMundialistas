using System.Globalization;
using System.Text;
using SistemaQuinielaMundialistasV2.Models;

namespace SistemaQuinielaMundialistasV2.Services;

public static class ReporteExportService
{
    public static byte[] CrearCsvAdministrador(EstadisticasAdministrador datos)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Reporte;Valor");
        sb.AppendLine($"Resultado mas repetido;{EscaparCsv(datos.ResultadoMasRepetido)}");
        sb.AppendLine($"Partido con mas aciertos;{EscaparCsv(datos.PartidoConMasAciertos)}");
        sb.AppendLine($"Partido con mas pronosticos;{EscaparCsv(datos.PartidoConMasPronosticos)}");
        sb.AppendLine($"Promedio de goles;{datos.PromedioGoles.ToString("0.00", CultureInfo.InvariantCulture)}");
        sb.AppendLine($"Partido sin aciertos;{EscaparCsv(datos.PartidoSinAciertos)}");
        sb.AppendLine();
        sb.AppendLine("Posicion;Usuario;Aciertos;Puntos");

        foreach (var usuario in datos.TopUsuarios)
        {
            sb.AppendLine($"{usuario.Posicion};{EscaparCsv(usuario.NombreUsuario)};{usuario.Aciertos};{usuario.Puntos}");
        }

        return Utf8ConBom(sb.ToString());
    }

    public static byte[] CrearTxtAdministrador(EstadisticasAdministrador datos)
    {
        var lineas = ObtenerLineasAdministrador(datos);
        return Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lineas));
    }

    public static byte[] CrearPdfAdministrador(EstadisticasAdministrador datos)
        => PdfSimple.Crear("Reporte administrativo - Quinielas Mundialistas V2", ObtenerLineasAdministrador(datos));

    public static byte[] CrearCsvUsuario(EstadisticasUsuario datos, string nombreUsuario)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Reporte;Valor");
        sb.AppendLine($"Usuario;{EscaparCsv(nombreUsuario)}");
        sb.AppendLine($"Equipo mas apostado;{EscaparCsv(datos.EquipoMasApostado)}");
        sb.AppendLine($"Equipo sorpresa;{EscaparCsv(datos.EquipoSorpresa)}");
        sb.AppendLine($"Probabilidad de acierto;{datos.ProbabilidadAcierto.ToString("0.00", CultureInfo.InvariantCulture)}%");
        sb.AppendLine($"Pronosticos finalizados;{datos.PronosticosFinalizados}");
        sb.AppendLine($"Aciertos;{datos.Aciertos}");
        sb.AppendLine($"Marcadores exactos;{datos.MarcadoresExactos}");
        sb.AppendLine();
        sb.AppendLine("Partido;Ganador real;Resultado;Pronosticos;Apoyaban ganador;Porcentaje apoyo");

        foreach (var item in datos.DetalleSorpresas)
        {
            sb.AppendLine(
                $"{EscaparCsv(item.Partido)};{EscaparCsv(item.GanadorReal)};{EscaparCsv(item.Resultado)};" +
                $"{item.TotalPronosticos};{item.ApoyoGanador};" +
                $"{item.PorcentajeApoyoGanador.ToString("0.00", CultureInfo.InvariantCulture)}%");
        }

        return Utf8ConBom(sb.ToString());
    }

    public static byte[] CrearTxtUsuario(EstadisticasUsuario datos, string nombreUsuario)
    {
        var lineas = ObtenerLineasUsuario(datos, nombreUsuario);
        return Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, lineas));
    }

    public static byte[] CrearPdfUsuario(EstadisticasUsuario datos, string nombreUsuario)
        => PdfSimple.Crear($"Reporte del usuario - {nombreUsuario}", ObtenerLineasUsuario(datos, nombreUsuario));

    private static List<string> ObtenerLineasAdministrador(EstadisticasAdministrador datos)
    {
        var lineas = new List<string>
        {
            "SISTEMA DE QUINIELAS MUNDIALISTAS V2",
            "REPORTE DEL ADMINISTRADOR",
            $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}",
            "",
            $"Resultado mas repetido: {datos.ResultadoMasRepetido}",
            $"Partido con mas aciertos: {datos.PartidoConMasAciertos}",
            $"Partido con mas pronosticos: {datos.PartidoConMasPronosticos}",
            $"Promedio de goles: {datos.PromedioGoles:0.00}",
            $"Partido sin aciertos: {datos.PartidoSinAciertos}",
            "",
            "USUARIOS CON MAS ACIERTOS"
        };

        foreach (var usuario in datos.TopUsuarios)
        {
            lineas.Add($"{usuario.Posicion}. {usuario.NombreUsuario} - Aciertos: {usuario.Aciertos} - Puntos: {usuario.Puntos}");
        }

        return lineas;
    }

    private static List<string> ObtenerLineasUsuario(EstadisticasUsuario datos, string nombreUsuario)
    {
        var lineas = new List<string>
        {
            "SISTEMA DE QUINIELAS MUNDIALISTAS V2",
            $"REPORTE DEL USUARIO: {nombreUsuario}",
            $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}",
            "",
            $"Equipo mas apostado: {datos.EquipoMasApostado}",
            $"Equipo sorpresa: {datos.EquipoSorpresa}",
            $"Probabilidad de acierto: {datos.ProbabilidadAcierto:0.00}%",
            $"Pronosticos finalizados: {datos.PronosticosFinalizados}",
            $"Aciertos: {datos.Aciertos}",
            $"Marcadores exactos: {datos.MarcadoresExactos}",
            "",
            "DETALLE DE SORPRESAS"
        };

        if (datos.DetalleSorpresas.Count == 0)
        {
            lineas.Add("No hay suficientes datos para mostrar partidos sorpresa.");
        }
        else
        {
            foreach (var item in datos.DetalleSorpresas)
            {
                lineas.Add(
                    $"{item.Partido} | Ganador: {item.GanadorReal} | Resultado: {item.Resultado} | " +
                    $"Apoyo: {item.ApoyoGanador}/{item.TotalPronosticos} ({item.PorcentajeApoyoGanador:0.00}%)");
            }
        }

        return lineas;
    }

    private static string EscaparCsv(string? valor)
    {
        string texto = valor ?? string.Empty;
        if (texto.Contains(';') || texto.Contains('"') || texto.Contains('\n'))
            return $"\"{texto.Replace("\"", "\"\"")}\"";

        return texto;
    }

    private static byte[] Utf8ConBom(string texto)
    {
        byte[] bom = Encoding.UTF8.GetPreamble();
        byte[] datos = Encoding.UTF8.GetBytes(texto);
        return bom.Concat(datos).ToArray();
    }

    // Generador PDF propio, sin librerias externas.
    // Produce un PDF de texto simple para cumplir la bonificacion solicitada.
    private static class PdfSimple
    {
        public static byte[] Crear(string titulo, IEnumerable<string> lineas)
        {
            var paginas = DividirPaginas(new[] { titulo, "" }.Concat(lineas), 42);
            var objetos = new List<string>();

            objetos.Add("<< /Type /Catalog /Pages 2 0 R >>");

            int cantidadPaginas = paginas.Count;
            var idsPaginas = Enumerable.Range(0, cantidadPaginas)
                .Select(i => 3 + i * 2)
                .ToArray();

            objetos.Add($"<< /Type /Pages /Kids [{string.Join(" ", idsPaginas.Select(id => $"{id} 0 R"))}] /Count {cantidadPaginas} >>");

            for (int i = 0; i < cantidadPaginas; i++)
            {
                int idContenido = idsPaginas[i] + 1;
                string contenido = CrearContenidoPagina(paginas[i]);
                byte[] contenidoBytes = Encoding.ASCII.GetBytes(contenido);

                objetos.Add(
                    $"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] " +
                    $"/Resources << /Font << /F1 {3 + cantidadPaginas * 2} 0 R >> >> /Contents {idContenido} 0 R >>");

                objetos.Add($"<< /Length {contenidoBytes.Length} >>\nstream\n{contenido}\nendstream");
            }

            objetos.Add("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>");

            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream, Encoding.ASCII, leaveOpen: true);

            writer.Write("%PDF-1.4\n");
            writer.Flush();

            var offsets = new List<long> { 0 };

            for (int i = 0; i < objetos.Count; i++)
            {
                offsets.Add(stream.Position);
                writer.Write($"{i + 1} 0 obj\n{objetos[i]}\nendobj\n");
                writer.Flush();
            }

            long xref = stream.Position;
            writer.Write($"xref\n0 {objetos.Count + 1}\n");
            writer.Write("0000000000 65535 f \n");

            for (int i = 1; i < offsets.Count; i++)
                writer.Write($"{offsets[i]:0000000000} 00000 n \n");

            writer.Write(
                $"trailer\n<< /Size {objetos.Count + 1} /Root 1 0 R >>\n" +
                $"startxref\n{xref}\n%%EOF");
            writer.Flush();

            return stream.ToArray();
        }

        private static List<List<string>> DividirPaginas(IEnumerable<string> lineas, int porPagina)
        {
            var normalizadas = lineas
                .SelectMany(PartirLinea)
                .ToList();

            var paginas = new List<List<string>>();
            for (int i = 0; i < normalizadas.Count; i += porPagina)
                paginas.Add(normalizadas.Skip(i).Take(porPagina).ToList());

            if (paginas.Count == 0)
                paginas.Add(new List<string> { "Sin datos." });

            return paginas;
        }

        private static IEnumerable<string> PartirLinea(string linea)
        {
            string limpia = AAscii(linea);
            const int ancho = 88;

            if (limpia.Length <= ancho)
            {
                yield return limpia;
                yield break;
            }

            for (int i = 0; i < limpia.Length; i += ancho)
                yield return limpia.Substring(i, Math.Min(ancho, limpia.Length - i));
        }

        private static string CrearContenidoPagina(IEnumerable<string> lineas)
        {
            var sb = new StringBuilder();
            sb.AppendLine("BT");
            sb.AppendLine("/F1 11 Tf");
            sb.AppendLine("50 750 Td");
            sb.AppendLine("14 TL");

            bool primera = true;
            foreach (var linea in lineas)
            {
                string segura = EscaparPdf(linea);
                if (!primera)
                    sb.AppendLine("T*");

                sb.AppendLine($"({segura}) Tj");
                primera = false;
            }

            sb.AppendLine("ET");
            return sb.ToString();
        }

        private static string EscaparPdf(string texto) =>
            texto.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");

        private static string AAscii(string texto)
        {
            string normalizado = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalizado)
            {
                var categoria = CharUnicodeInfo.GetUnicodeCategory(c);
                if (categoria == UnicodeCategory.NonSpacingMark)
                    continue;

                if (c >= 32 && c <= 126)
                    sb.Append(c);
                else
                    sb.Append('?');
            }

            return sb.ToString();
        }
    }
}
