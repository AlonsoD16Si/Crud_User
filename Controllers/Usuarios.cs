using System.Data;
using System.Data.SqlClient;
using API_REST_DARIO.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_DARIO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly string _con;

        public UsuariosController(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("DefaultConnection");
        }

        // GET api/usuarios
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            List<Usuario> usuarios = new();
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("GetUsers", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario
                            {
                                id = Convert.ToInt32(reader["id"]),
                                name = reader["name"].ToString(),
                                email = reader["email"].ToString()
                            });
                        }
                    }
                }
            }
            return usuarios;
        }

        // POST api/usuarios
        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("CreateUser", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", usuario.name);
                    cmd.Parameters.AddWithValue("@email", usuario.email);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Usuario creado correctamente");
        }

        // PUT api/usuarios/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("UpdateUser", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", usuario.name);
                    cmd.Parameters.AddWithValue("@email", usuario.email);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Usuario actualizado correctamente");
        }

        // DELETE api/usuarios/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("DeleteUser", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Usuario eliminado correctamente");
        }
    }
}
