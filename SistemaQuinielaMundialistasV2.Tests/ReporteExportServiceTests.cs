using System.Text;
using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;
using Xunit;

namespace SistemaQuinielaMundialistasV2.Tests;

public class ReporteExportServiceTests
{
    private static EstadisticasAdministrador Admin() => new()
    {
        ResultadoMasRepetido="1-0; especial", PartidoConMasAciertos="CR vs JP",
        PartidoConMasPronosticos="CR vs BR", PromedioGoles=2.5, PartidoSinAciertos="A vs B",
        TopUsuarios = new() { new TopUsuarioAciertos { Posicion=1, NombreUsuario="ken\"dall", Aciertos=3, Puntos=11 } }
    };
    private static EstadisticasUsuario User(bool detalle=true) => new()
    {
        EquipoMasApostado="Costa Rica", EquipoSorpresa="Japón", ProbabilidadAcierto=66.67,
        PronosticosFinalizados=3, Aciertos=2, MarcadoresExactos=1,
        DetalleSorpresas = detalle ? new() { new PartidoSorpresaDetalle { Partido="CR vs JP", GanadorReal="JP", Resultado="0-1", TotalPronosticos=10, ApoyoGanador=2, PorcentajeApoyoGanador=20 } } : new()
    };

    [Fact]
    public void ExportesAdministrador_GeneranContenido()
    {
        var s = new ReporteExportService();
        var csv = s.CrearCsvAdministrador(Admin());
        var txt = s.CrearTxtAdministrador(Admin());
        var pdf = s.CrearPdfAdministrador(Admin());
        Assert.Contains("Reporte;Valor", Encoding.UTF8.GetString(csv));
        Assert.Contains("REPORTE DEL ADMINISTRADOR", Encoding.UTF8.GetString(txt));
        Assert.StartsWith("%PDF-1.4", Encoding.ASCII.GetString(pdf));
    }

    [Fact]
    public void ExportesUsuario_ConDetalle_GeneranContenido()
    {
        var s = new ReporteExportService();
        var csv = s.CrearCsvUsuario(User(), "kendall");
        var txt = s.CrearTxtUsuario(User(), "kendall");
        var pdf = s.CrearPdfUsuario(User(), "kendall");
        Assert.Contains("Costa Rica", Encoding.UTF8.GetString(csv));
        Assert.Contains("CR vs JP", Encoding.UTF8.GetString(txt));
        Assert.StartsWith("%PDF-1.4", Encoding.ASCII.GetString(pdf));
    }

    [Fact]
    public void TxtUsuario_SinDetalle_MuestraMensaje()
    {
        var txt = new ReporteExportService().CrearTxtUsuario(User(false), "kendall");
        Assert.Contains("No hay suficientes datos", Encoding.UTF8.GetString(txt));
    }

    [Fact]
    public void Pdf_MuchasLineas_GeneraVariasPaginas()
    {
        var datos = Admin();
        datos.TopUsuarios = Enumerable.Range(1, 100).Select(i => new TopUsuarioAciertos { Posicion=i, NombreUsuario=new string('X',100), Aciertos=i, Puntos=i*2 }).ToList();
        var pdf = new ReporteExportService().CrearPdfAdministrador(datos);
        Assert.True(pdf.Length > 1000);
        Assert.Contains("/Count", Encoding.ASCII.GetString(pdf));
    }
}
