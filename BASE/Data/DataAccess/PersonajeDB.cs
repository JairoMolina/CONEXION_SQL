using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASE.Data.DataAccess
{
    internal class PersonajeDB
    {
        private string connectionString = "Server=localhost;Database=dragon_ball;Uid=root;Pwd=MyMolin@07*_";

        public bool ProbarConexion()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public DataTable LeerPersonajes()
        {
            DataTable personajes = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM personajes_dragon_ball";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(personajes);
                    }
                }
            }

            return personajes;
        }

        // CREAR PERSONAJE
        public int CrearPersonaje(string nombre, string raza, int nivelPoder, DateTime fecha_creacion, string historia) // MODIFICADO
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO personajes_dragon_ball (nombre, raza, nivel_poder, fecha_creacion, historia) VALUES (@nombre, @raza, @nivelPoder, @fecha_creacion, @historia)";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@raza", raza);
                    command.Parameters.AddWithValue("@nivelPoder", nivelPoder);
                    command.Parameters.AddWithValue("@fecha_creacion", fecha_creacion);
                    command.Parameters.AddWithValue("@historia", historia);

                    return command.ExecuteNonQuery();
                }
            }
        }


        // BUSCAR PERSONAJE POR ID
        public DataTable BuscarPersonajePorId(int id)
        {
            DataTable personaje = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM personajes_dragon_ball WHERE id = @id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(personaje);
                    }
                }
            }
            return personaje;
        }

        // ACTUALIZAR PERSONAJE
        public int ActualizarPersonaje(int id, string nombre, string raza, int nivelPoder, string historia) // OPCION CREADA
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "update personajes_dragon_ball set nombre=@nombre, raza=@raza, nivel_poder=@nivelPoder,historia=@historia where id=@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@raza", raza);
                    command.Parameters.AddWithValue("@nivelPoder", nivelPoder);
                    command.Parameters.AddWithValue("@historia", historia);
                    return command.ExecuteNonQuery();
                }
            }
        }

        // BORRAR PERSONAJE
        public int BorrarPersonaje(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString)) // OPCION CREADA
            {
                connection.Open();

                string sql = "delete from personajes_dragon_ball where id =@id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}