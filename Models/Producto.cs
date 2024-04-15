using CRUD.Models;
using Npgsql;
using System.Collections;
using System.Data;

namespace GestionZapateria.Models
{
    public class Producto : AccesoDatos
    {

        #region Atributos

        public int Id_Producto { get; set; }

        public int Id_Categoria { get; set; }

        public int Id_Marca { get; set; }

        public string Modelo { get; set; }

        public double Talla { get; set; }

        public double Precio { get; set;}

        #endregion


        #region Constructores

        public Producto() { }

        public Producto(int Id_Producto)
        {

            List<NpgsqlParameter> listParameter = new()
            {

                new("Id_Producto", Id_Producto)

            };

            const string sql = "SELECT * FROM  productos WHERE id_producto = @Id_Producto";
            DataTable dt = GetQuery(sql, listParameter);
            if (dt.Rows.Count <= 0) return;

            DataRow dr = dt.Rows[0];
            Id_Producto = (int)dr["id_producto"];
            Id_Categoria = (int)dr["id_categoria"];
            Id_Marca = (int)dr["id_marca"];
            Modelo = (string)dr["modelo"];
            Talla = (double)(decimal)dr["talla"];
            Precio = (double)(decimal)dr["precio"];

        }

#endregion

        #region Métodos de gestión
        /// <summary>
        /// Método que agrega datos a la tabla personas
        /// </summary>
        /// <param name="per"></param>
        /// <returns></returns>
        public bool Add(Producto por)
        {
            List<NpgsqlParameter> listParameter = new()
            {
                new("Id_Producto", por.Id_Producto),
                new("Id_Categoria", por.Id_Categoria),
                new("Id_Marca", por.Id_Marca),
                new("Modelo",por.Modelo),
                new("Talla",por.Talla),
                new("Precio",por.Precio)
            };

            const string sql = " INSERT INTO productos " +
                " (id_producto, id_categoria, id_marca,modelo, talla, precio) " +
                " VALUES(@Id_Producto,@Id_Categoria,@Id_Marca,@Modelo,@Talla,@Precio) ";
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
        public bool UPDATE(Producto por)
        {
            List<NpgsqlParameter> listParameter = new()
            {
                new("Id_Producto", por.Id_Producto),
                new("Id_Categoria", por.Id_Categoria),
                new("Id_Marca", por.Id_Marca),
                new("Modelo",por.Modelo),
                new("Talla",por.Talla),
                new("Precio",por.Precio)
            };

            const string sql = " UPDATE productos SET " +
                " id_categoria = @Id_Categoria, " +
                " id_marca = @Id_Marca, " +
                " modelo = @Modelo, " +
                " talla = @Talla, " +
                " precio = @Precio WHERE id_producto = @Id_Producto ";
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
        public bool DELETE(Producto por)
        {
            List<NpgsqlParameter> listParameter = new()
            {
                new("Id_Producto",por.Id_Producto)
            };

            const string sql = " DELETE FROM productos " +
                " WHERE id_producto = @Id_Producto ";
            int afectados = ExecuteQuery(sql, listParameter);
            if (afectados > 0)
                return true;
            return false;
        }

        public List<Producto> GetProductoByNombre(
            string modelo, double talla, double precio)
        {
            var paramModelo = new NpgsqlParameter("modelo", modelo);
            var paramTalla = new NpgsqlParameter("talla", talla);
            var paramPrecio = new NpgsqlParameter("precio", precio);

            var lstParametros = new List<NpgsqlParameter>();
            var argumentos = new ArrayList();
            if (!string.IsNullOrEmpty(modelo))
            {
                argumentos.Add("modelo ILIKE :modelo || '%'");
                lstParametros.Add(paramModelo);
            }
            if (!string.IsNullOrEmpty("" + talla))
            {
                argumentos.Add("talla ILIKE :talla || '%'");
                lstParametros.Add(paramTalla);
            }
            if (!string.IsNullOrEmpty("" + precio))
            {
                argumentos.Add("precio ILIKE :precio || '%'");
                lstParametros.Add(paramPrecio);
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
                    "SELECT * FROM productos ORDER BY modelo";

            else
                sql =
                    string.Format(
                        "SELECT * FROM productos {0} ORDER BY modelo",
                        condicion);
            // Pinta la tabla
            DataTable table = GetQuery(sql, lstParametros);
            var lstProductos = new List<Producto>();
            foreach (DataRow fila in table.Rows)
            {
                //crea filas 
                lstProductos.Add(new Producto
                {
                    //Se muestra los datos en la consulta
                    Id_Producto = (int)fila["id_producto"],
                Id_Categoria = (int)fila["id_categoria"],
                Id_Marca = (int)fila["id_marca"],
                Modelo = (string)fila["modelo"],
                Talla = (double)(decimal)fila["talla"],
                Precio = (double)(decimal)fila["precio"]
            });
            }
            return lstProductos; //regresa el listado de alumnos
        }

        #endregion


    }
}
