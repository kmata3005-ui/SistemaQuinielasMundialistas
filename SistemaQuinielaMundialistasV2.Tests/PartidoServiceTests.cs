using SistemaQuinielaMundialistasV2.Models;
using SistemaQuinielaMundialistasV2.Services;

namespace SistemaQuinielaMundialistasV2.Tests;

public class PartidoServiceTests
{
    [Fact]
    public void CalcularPuntos_MarcadorExacto_DebeDarCinco()
    {
        var p = new Pronostico { GolesLocalPronosticados = 2, GolesVisitantePronosticados = 1 };
        var partido = new Partido { GolesLocal = 2, GolesVisitante = 1 };
        Assert.Equal(5, PartidoService.CalcularPuntos(p, partido));
    }

    [Fact]
    public void CalcularPuntos_GanadorCorrecto_DebeDarDos()
    {
        var p = new Pronostico { GolesLocalPronosticados = 3, GolesVisitantePronosticados = 1 };
        var partido = new Partido { GolesLocal = 2, GolesVisitante = 0 };
        Assert.Equal(2, PartidoService.CalcularPuntos(p, partido));
    }

    [Fact]
    public void CalcularPuntos_EmpateCorrectoSinExacto_DebeDarDos()
    {
        var p = new Pronostico { GolesLocalPronosticados = 1, GolesVisitantePronosticados = 1 };
        var partido = new Partido { GolesLocal = 2, GolesVisitante = 2 };
        Assert.Equal(2, PartidoService.CalcularPuntos(p, partido));
    }

    [Fact]
    public void CalcularPuntos_ResultadoIncorrecto_DebeDarCero()
    {
        var p = new Pronostico { GolesLocalPronosticados = 2, GolesVisitantePronosticados = 0 };
        var partido = new Partido { GolesLocal = 0, GolesVisitante = 1 };
        Assert.Equal(0, PartidoService.CalcularPuntos(p, partido));
    }
}
