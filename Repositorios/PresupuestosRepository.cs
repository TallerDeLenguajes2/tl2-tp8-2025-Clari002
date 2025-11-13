using Microsoft.Data.Sqlite;
using SQLitePCL;
using tl2_tp8_2025_Clari002.Models;

namespace tl2_tp8_2025_Clari002.Repositorios
{
    public class PresupuestosRepository
    {
        private string cadenaConexion = "Data Source=DB/Tienda_final.db;";

        public void CrearPresupuesto(Presupuestos presupuesto)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string queryString = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@NombreDestinatario, @FechaCreacion)";

                using var comando = new SqliteCommand(queryString, conexion);

                comando.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuesto.NombreDestinatario));
                comando.Parameters.Add(new SqliteParameter("@FechaCreacion", presupuesto.FechaCreacion));

                comando.ExecuteNonQuery();
            }
        }

        public List<Presupuestos> ListarPresupuestos()
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();

                string sql = @"SELECT * FROM Presupuestos";

                using var comando = new SqliteCommand(sql, conexion);
                using var lector = comando.ExecuteReader();

                var presupuestos = mapearPresupuestos(lector);
                return presupuestos;
            }

        }


        private List<Presupuestos> mapearPresupuestos(SqliteDataReader lector)
        {
            var presupuestos = new List<Presupuestos>();
            while (lector.Read())
            {
                var presupuesto = new Presupuestos
                {
                    IdPresupuesto = Convert.ToInt32(lector["idPresupuesto"]),
                    NombreDestinatario = lector["NombreDestinatario"].ToString(),
                    FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"])

                };
                presupuestos.Add(presupuesto);
            }
            return presupuestos;
        }

        public Presupuestos? BuscarPresupuestoPorId(int idPresupuestoBuscado)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();
                string sql = "SELECT * FROM Presupuestos WHERE idPresupuesto = @IdBuscado";

                using var comando = new SqliteCommand(sql, conexion);
                comando.Parameters.Add(new SqliteParameter("@IdBuscado", idPresupuestoBuscado));

                Presupuestos? presupuesto = null;

                using var lector = comando.ExecuteReader();
                if (lector.Read())//si encontró un registro
                {
                    presupuesto = new Presupuestos
                    {
                        IdPresupuesto = Convert.ToInt32(lector["idPresupuesto"]),
                        NombreDestinatario = lector["NombreDestinatario"].ToString(),
                        FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"])
                    };
                }

                if (presupuesto == null)
                {
                    return null;
                }


                string sqlDet = @"SELECT pd.Cantidad, pro.idProducto, pro.Descripcion, pro.Precio FROM PresupuestosDetalle pd 
                                  INNER JOIN  Productos pro ON pd.idProducto = pro.idProducto 
                                  WHERE pd.idPresupuesto = @IdBuscado";
                using var comandoDet = new SqliteCommand(sqlDet, conexion);
                comandoDet.Parameters.Add(new SqliteParameter("@IdBuscado", idPresupuestoBuscado));

                using var lectorDet = comandoDet.ExecuteReader();
                while (lectorDet.Read())
                {
                    var detalle = new PresupuestosDetalle
                    {
                        Producto = new Productos
                        {
                            IdProducto = Convert.ToInt32(lectorDet["idProducto"]),
                            Descripcion = lectorDet["Descripcion"].ToString(),
                            Precio = Convert.ToDouble(lectorDet["Precio"])
                            //si fuera double
                            //Precio = Convert.ToDouble(lectorDet["Precio])
                        },
                        Cantidad = Convert.ToInt32(lectorDet["Cantidad"])
                    };
                    presupuesto.Detalle.Add(detalle);
                }
                return presupuesto;
            }
            ;
        }

        //agrego producto + cantidad a un presupuesto existente
        public bool AgregarProductoAlPresupuesto(int idPresupuesto, int idProducto, int cantidad)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();

                string sql = @"
                INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad)
                VALUES (@idPresupuesto, @idProducto, @Cantidad)";

                using var comando = new SqliteCommand(sql, conexion);

                comando.Parameters.AddWithValue("@idPresupuesto", idPresupuesto);
                comando.Parameters.AddWithValue("@idProducto", idProducto);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);

                //Ejecuta y devuelve true si se inserto una fila
                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public bool EliminarPresupuesto(int idPresupuestoEiminar)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();

                //primero elimino los detalles del presupuesto
                string sqlDetalle = "DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @IdEliminar ";
                using (var comandoDetalle = new SqliteCommand(sqlDetalle, conexion))
                {
                    comandoDetalle.Parameters.AddWithValue("@IdEliminar", idPresupuestoEiminar);
                    comandoDetalle.ExecuteNonQuery();//elimino los productos asociados
                }

                //segundo elimino el presupuesto principal

                string sql = "DELETE FROM Presupuestos WHERE idPresupuesto = @IdEliminar";
                using (var comando = new SqliteCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@IdEliminar", idPresupuestoEiminar);
                    int filasAfectadas = comando.ExecuteNonQuery();

                    //Devuelve true si se elimino al menos una fila
                    return filasAfectadas > 0;

                }

            }
        }
        
        //modificar presupuesto
        public void ModificarPresupuesto(int idPresupuestoBuscado, Presupuestos presupuesto)
        {
            using (var conexion = new SqliteConnection(cadenaConexion))
            {
                conexion.Open();

                string sql = "UPDATE Presupuestos SET NombreDestinatario=@NombreDestinatario, FechaCreacion=@FechaCreacion WHERE idPresupuesto=@idPresupuesto";
                using var comando = new SqliteCommand(sql, conexion);

                comando.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuesto.NombreDestinatario));
                comando.Parameters.Add(new SqliteParameter("@FechaCreacion", presupuesto.FechaCreacion));
                comando.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuestoBuscado));

                comando.ExecuteNonQuery();
            }
        }
    }

}