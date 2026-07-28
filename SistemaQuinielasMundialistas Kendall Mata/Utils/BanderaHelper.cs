using System.Drawing;

namespace SistemaQuinielasMundialistas.Utils
{
    public static class BanderaHelper
    {
        private static readonly Dictionary<string, string> Archivos = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Costa Rica"] = "costa_rica.png",
            ["México"] = "mexico.png", ["Mexico"] = "mexico.png",
            ["Alemania"] = "alemania.png",
            ["Japón"] = "japon.png", ["Japon"] = "japon.png",
            ["Brasil"] = "brasil.png",
            ["Francia"] = "francia.png",
            ["España"] = "espana.png",
            ["Canadá"] = "canada.png", ["Canada"] = "canada.png",
            ["Argentina"] = "argentina.png",
            ["Portugal"] = "portugal.png",
            ["Países Bajos"] = "paises_bajos.png", ["Paises Bajos"] = "paises_bajos.png",
            ["Estados Unidos"] = "estados_unidos.png",
            ["Colombia"] = "colombia.png",
            ["Uruguay"] = "uruguay.png",
            ["Corea del Sur"] = "corea_del_sur.png",
            ["Inglaterra"] = "inglaterra.png",
            ["Marruecos"] = "marruecos.png"
        };

        private static readonly Dictionary<string, Image> Cache = new(StringComparer.OrdinalIgnoreCase);

        public static Image ObtenerImagen(string pais)
        {
            string archivo = Archivos.GetValueOrDefault(pais?.Trim() ?? string.Empty, "sin_bandera.png");

            if (Cache.TryGetValue(archivo, out Image? imagen))
                return imagen;

            string ruta = Path.Combine(AppContext.BaseDirectory, "Resources", "Banderas", archivo);
            if (!File.Exists(ruta))
                ruta = Path.Combine(AppContext.BaseDirectory, "Resources", "Banderas", "sin_bandera.png");

            try
            {
                if (File.Exists(ruta))
                {
                    using Image original = Image.FromFile(ruta);
                    imagen = new Bitmap(original);
                }
                else
                {
                    imagen = new Bitmap(32, 20);
                }
            }
            catch
            {
                imagen = new Bitmap(32, 20);
            }

            Cache[archivo] = imagen;
            return imagen;
        }
    }
}
