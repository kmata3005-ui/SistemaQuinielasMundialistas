namespace SistemaQuinielaMundialistasV2.Models;

public sealed class RankingItem
{
    public int Posicion { get; set; }
    public int UsuarioId { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public int Puntos { get; set; }
    public List<string> Insignias { get; set; } = new();
}

public sealed class QuinielaRankingItem
{
    public int Posicion { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int Puntos { get; set; }
    public List<string> Insignias { get; set; } = new();
}

public sealed class QuinielaResumen
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool EsPrivada { get; set; }
    public bool PerteneceUsuario { get; set; }
    public List<QuinielaRankingItem> Participantes { get; set; } = new();
}

public sealed class TopUsuarioAciertos
{
    public int Posicion { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public int Aciertos { get; set; }
    public int Puntos { get; set; }
}

public sealed class EstadisticasAdministrador
{
    public string ResultadoMasRepetido { get; set; } = "Sin datos";
    public string PartidoConMasAciertos { get; set; } = "Sin datos";
    public List<TopUsuarioAciertos> TopUsuarios { get; set; } = new();
    public string PartidoConMasPronosticos { get; set; } = "Sin datos";
    public double PromedioGoles { get; set; }
    public string PartidoSinAciertos { get; set; } = "Sin datos";
}

public sealed class PartidoSorpresaDetalle
{
    public string Partido { get; set; } = string.Empty;
    public string GanadorReal { get; set; } = string.Empty;
    public string Resultado { get; set; } = string.Empty;
    public int TotalPronosticos { get; set; }
    public int ApoyoGanador { get; set; }
    public double PorcentajeApoyoGanador { get; set; }
}

public sealed class EstadisticasUsuario
{
    public string EquipoMasApostado { get; set; } = "Sin datos";
    public string EquipoSorpresa { get; set; } = "Sin datos suficientes";
    public int CantidadSorpresas { get; set; }
    public List<PartidoSorpresaDetalle> DetalleSorpresas { get; set; } = new();
    public int PronosticosFinalizados { get; set; }
    public int Aciertos { get; set; }
    public int MarcadoresExactos { get; set; }
    public double ProbabilidadAcierto { get; set; }
}
