using Npgsql;

namespace GestionZapateria.Models
{
    public class Conexion
    {
        private readonly string cadenaConexion;
        public Conexion(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }

        public NpgsqlConnection AbrirConexion()
        {
            NpgsqlConnection conexion = new(cadenaConexion);

            try
            {
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
                return null;
            }
        }

        public void CerrarConexion(NpgsqlConnection conexion)
        {
            try
            {
                if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cerrar la conexión: {ex.Message}");
            }
        }
    }
}