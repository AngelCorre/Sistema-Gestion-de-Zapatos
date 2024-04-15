using CRUD.Models;
using Npgsql;
using System.Collections;
using System.Data;

namespace GestionZapateria.Models
{
    public class Marca :AccesoDatos
    {
        public int Id_Marca { get; set; }

        public string Nombre_Marca { get; set; }

        #region Constructores

        public Marca() {}

        public Marca(int Id_Marca)
        {

            List<NpgsqlParameter> listParameter = new()
            {

                new("Id_Marca", Id_Marca)

            };

            const string sql = "SELECT * FROM  marcas WHERE id_marca = @Id_Marca";
            DataTable dt = GetQuery(sql, listParameter);
            if (dt.Rows.Count <= 0) return;

            DataRow dr = dt.Rows[0];
            Id_Marca = (int)dr["id_marca"];
            Nombre_Marca = (string)dr["nombre_marca"];

        }

        #endregion

        #region Métodos de gestión
        /// <summary>
        /// Método que agrega datos a la tabla personas
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public bool Add(Marca mar)
        {
            List<NpgsqlParameter> listParameter = new()
            {
                new("Id_Marca",mar.Id_Marca),
                new("Nombre_Marca",mar.Nombre_Marca)
            };

            const string sql = " INSERT INTO marcas " +
                " (id_marca, nombre_marca) " +
                " VALUES(@Id_Marca,@Nombre_Marca) ";
            int afectados = ExecuteQuery(sql, listParameter);
            if (afectados > 0)
                return true;
            return false;
        }



        /// <summary>
        /// Método que agrega datos a la tabla personas
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public bool UPDATE(Marca mar)
        {
            List<NpgsqlParameter> listParameter = new()
            {
                new("Id_Marca",mar.Id_Marca),
                new("Nombre_Marca",mar.Nombre_Marca)
            };

            const string sql = " UPDATE marcas SET nombre_marca = @Nombre_Marca WHERE id_marca = @Id_Marca";
            int afectados = ExecuteQuery(sql, listParameter);
            if (afectados > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Método que agrega datos a la tabla personas
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public bool DELETE(Marca mar)
        {
            List<NpgsqlParameter> listParameter = new()
            {
                new("Id_Marca", mar.Id_Marca)
            };

            const string sql = " DELETE FROM marcas " +
                " WHERE id_marca = @Id_Marca ";
            int afectados = ExecuteQuery(sql, listParameter);
            if (afectados > 0)
                return true;
            return false;
        }

        public List<Marca> GetMarcaByNombre(
            string nombre_marca)
        {
            var paramNombre = new NpgsqlParameter("nombre_marca", nombre_marca);

            var lstParametros = new List<NpgsqlParameter>();
            var argumentos = new ArrayList();
            if (!string.IsNullOrEmpty(nombre_marca))
            {
                argumentos.Add("nombre_marca ILIKE :nombre_marca || '%'");
                lstParametros.Add(paramNombre);
            }
            string condicion = string.Empty;
            switch (argumentos.Count)
            {
                case 1: // inicio de caso
                    condicion = string.Format("WHERE {0}", argumentos[0]);
                    break; //cierre de caso

                case 2: // inicio de caso
                    // llena la variable anterior con argumenos dependiendo el caso
                    condicion = string.Format("WHERE {0} AND {1}", argumentos[0], argumentos[1]);
                    break; //cierre de caso


            }

            string sql;
            if (string.IsNullOrEmpty(condicion))
                sql =
                    "SELECT * FROM marcas  ORDER BY nombre_marca";

            else
                sql =
                    string.Format(
                        "SELECT * FROM marcas {0} ORDER BY nombre_marca",
                        condicion);
            // Pinta la tabla
            DataTable table = GetQuery(sql, lstParametros);
            var lstMarcas = new List<Marca>();
            foreach (DataRow fila in table.Rows)
            {
                //crea filas 
                lstMarcas.Add(new Marca
                {
                    //Se muestra los datos en la consulta
                    Id_Marca = (int)fila["id_marca"],
                Nombre_Marca = (string)fila["nombre_marca"]
            });
            }
            return lstMarcas; //regresa el listado de alumnos
        }

        #endregion

    }
}
