using Npgsql;
using System.Data;
using GestionZapateria.Models;

namespace CRUD.Models
{
    public class AccesoDatos
    {
        string cadenaConexion = "Server=localhost; userid=postgres; Password=12345678;Database=Zapateria;Port=5432; Pooling=false;";
        public DataTable GetQuery(string sql, List<NpgsqlParameter> parameters)
        {
            Conexion conexionBD = new(cadenaConexion);
            DataTable table = new();
            NpgsqlDataAdapter adapter = new();
            using (NpgsqlConnection conexion = conexionBD.AbrirConexion())
            {
                using NpgsqlCommand command = new();
                command.Connection = conexion;
                command.CommandText = sql;
                command.Parameters.Clear();
                foreach (NpgsqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            return table;
        }


        /// <summary>
        /// Ejecuta una consulta en la base de datos de inserción, actualización o eliminación.
        /// </summary>
        /// <param name="sql">Texto de la consulta SQL.</param>
        /// <param name="parameters">Lista de parámetros de la consulta.</param>
        /// <returns>Número de filas afectadas.</returns>
        protected int ExecuteQuery(string sql, List<NpgsqlParameter> parameters)
        {
            int rowsAffected;

            Conexion conexionBD = new(cadenaConexion);
            using (NpgsqlConnection conexion = conexionBD.AbrirConexion())
            {
                using NpgsqlCommand command = new();
                command.Connection = conexion;
                command.CommandText = sql;
                command.Parameters.Clear();
                foreach (NpgsqlParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }


    }
}
