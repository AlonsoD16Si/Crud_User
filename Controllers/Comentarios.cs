using System.Data;
using System.Data.SqlClient;
using API_REST_DARIO.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_DARIO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentariosController : ControllerBase
    {
        private readonly string _con;

        public ComentariosController(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("DefaultConnection");
        }

        // GET api/comentarios
        [HttpGet]
        public IEnumerable<Comentarios> Get()
        {
            List<Comentarios> comentarios = new();
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("GETComments", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comentarios.Add(new Comentarios
                            {
                                id = Convert.ToInt32(reader["id"]),
                                content = reader["content"].ToString(),
                                created_at = Convert.ToDateTime(reader["created_at"]),
                                post_id = Convert.ToInt32(reader["post_id"])
                            });
                        }
                    }
                }
            }
            return comentarios;
        }

        // POST api/comentarios
        [HttpPost]
        public IActionResult Post([FromBody] Comentarios comentarios)
        {
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("CreateComment", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@content", comentarios.content);
                    cmd.Parameters.AddWithValue("@post_id", comentarios.post_id);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Comentario creado correctamente");
        }

        // PUT api/comentarios/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Comentarios comentarios)
        {
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("UPDATEComment", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@content", comentarios.content);
                    cmd.Parameters.AddWithValue("@post_id", comentarios.post_id);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Comentario actualizado correctamente");
        }

        // DELETE api/comentarios/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("DeleteComment", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Comentario eliminado correctamente");
        }
    }
}       