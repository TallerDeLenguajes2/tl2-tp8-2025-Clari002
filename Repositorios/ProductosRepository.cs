using Microsoft.Data.Sqlite;
using SQLitePCL;
using tl2_tp8_2025_Clari002.Models;

namespace tl2_tp8_2025_Clari002.Repositorios
{
    //Repositorio creado en paso 1
    public class ProductosRepository
    {
        private string cadenaConexion = "Data Source=Tienda_final.db;";

        public void CrearProducto(Productos producto)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string queryString = "INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio) ";

                using var comando = new SqliteCommand(queryString, conexion);

                comando.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
                comando.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));

                comando.ExecuteNonQuery();

            }
        }
        public void ModificarProducto(int idProductoBuscado, Productos producto)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();

                string sql = "UPDATE Productos SET Descripcion=@Descripcion, Precio=@Precio WHERE idProducto=@idProducto";
                using var comando = new SqliteCommand(sql, conexion);

                comando.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
                comando.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
                comando.Parameters.Add(new SqliteParameter("@idProducto", idProductoBuscado));

                comando.ExecuteNonQuery();
            }
        }

        public List<Productos> ListarProductos()
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();

                string sql = @"SELECT idProducto, Descripcion, Precio FROM Productos";

                using var comando = new SqliteCommand(sql, conexion);
                using var lector = comando.ExecuteReader();

                var productos = mapearProductos(lector);
                return productos;
            }

        }
        private List<Productos> mapearProductos(SqliteDataReader lector)
        {
            var productos = new List<Productos>();
            while (lector.Read())
            {
                var producto = new Productos
                {
                    IdProducto = Convert.ToInt32(lector["idProducto"]),
                    Descripcion = lector["Descripcion"].ToString(),
                    Precio = Convert.ToDouble(lector["Precio"])

                };
                productos.Add(producto);
            }
            return productos;
        }

        public Productos? BuscarProductoId(int idProductoBuscado)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string sql = "SELECT idProducto, Descripcion, Precio FROM Productos WHERE idProducto = @IdBuscado";

                using var comando = new SqliteCommand(sql, conexion);
                comando.Parameters.Add(new SqliteParameter("@IdBuscado", idProductoBuscado));

                using var lector = comando.ExecuteReader();

                if (lector.Read())//si encontró un registro
                {
                    var producto = new Productos
                    {
                        IdProducto = Convert.ToInt32(lector["idProducto"]),
                        Descripcion = lector["Descripcion"].ToString(),
                        Precio = Convert.ToDouble(lector["Precio"])
                    };
                    return producto;
                }
                return null;
            }
        }

       
       
        public bool EliminarProducto(int idProductoAEliminar)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();

                string sql = "DELETE FROM Productos WHERE idProducto = @IdEliminar";
                
                using var comando = new SqliteCommand(sql, conexion);

                comando.Parameters.AddWithValue("@IdEliminar", idProductoAEliminar);

                int filasAfectadas = comando.ExecuteNonQuery();
                //Devuelve true si se elimino al menos una fila
                return filasAfectadas > 0;
                
                
            }
            
        }
        
    }
}