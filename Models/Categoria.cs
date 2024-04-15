using CRUD.Models;
using Npgsql;
using System.Collections;
using System.Data;
using System.Reflection;

namespace GestionZapateria.Models
{
    public class Categoria : AccesoDatos
    {

        public int Id_Categoria { get; set; }

        public string Nombre_Categoria { get; set; }

        #region Constructores

        public Categoria()
        {

        }

        public Categoria(int Id_Categoria)
        {

            List<NpgsqlParameter> listParameter = new()
            {

                new("Id_Categoria", Id_Categoria)

            };

            const string sql = "SELECT * FROM  categorias WHERE id_categoria = @Id_Categoria";
            DataTable dt = GetQuery(sql, listParameter);
            if (dt.Rows.Count <= 0) return;

            DataRow dr = dt.Rows[0];
            Id_Categoria = (int)dr["id_categoria"];
            Nombre_Categoria = (string)dr["nombre_categoria"];

        }

        #endregion

        #region Métodos de gestión
        /// <summary>
        /// Método que agrega datos a la tabla personas
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public bool Add(Categoria cat)
        {
            List<NpgsqlParameter> listParameter = new()
            {
                new("Id_Categoria", cat.Id_Categoria),
                new("Nombre_Categoria", cat.Nombre_Categoria)
            };

            const string sql = " INSERT INTO categorias " +
                " (id_categoria, nombre_categoria) " +
                " VALUES(@Id_Categoria,@Nombre_Categoria) ";
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
        public bool UPDATE(Categoria cat)
        {
            List<NpgsqlParameter> listParameter = new()
            {
                new("Id_Categoria",cat.Id_Categoria),
                new("Nombre_Categoria",cat.Nombre_Categoria)
            };

            const string sql = "UPDATE categorias SET nombre_categoria = @Nombre_Categoria WHERE id_categoria = @Id_Categoria";
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
        public bool DELETE(Categoria cat)
        {
            List<NpgsqlParameter> listParameter = new()
            {
                new("Id_Categoria", cat.Id_Categoria)
            };

            const string sql = " DELETE FROM categorias " +
                " WHERE id_categoria = @Id_Categoria ";
            int afectados = ExecuteQuery(sql, listParameter);
            if (afectados > 0)
                return true;
            return false;
        }

        public List<Categoria> GetCategoriaByNombre(
            string nombre_categoria)
        {
            var paramNombre = new NpgsqlParameter("nombre_categoria", nombre_categoria);

            var lstParametros = new List<NpgsqlParameter>();
            var argumentos = new ArrayList();
            if (!string.IsNullOrEmpty(nombre_categoria))
            {
                argumentos.Add("nombre_categoria ILIKE :nombre_categoria || '%'");
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
                    "SELECT * FROM categorias  ORDER BY nombre_categoria";

            else
                sql =
                    string.Format(
                        "SELECT * FROM categorias {0} ORDER BY nombre_categoria",
                        condicion);
            // Pinta la tabla
            DataTable table = GetQuery(sql, lstParametros);
            var lstCategorias = new List<Categoria>();
            foreach (DataRow fila in table.Rows)
            {
                //crea filas 
                lstCategorias.Add(new Categoria
                {
                    //Se muestra los datos en la consulta
                    Id_Categoria = (int)fila["id_categoria"],
                Nombre_Categoria = (string)fila["nombre_categoria"]
            });
            }
            return lstCategorias; //regresa el listado de alumnos
        }

        #endregion

    }
}
