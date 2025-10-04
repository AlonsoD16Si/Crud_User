using System.Data;
using System.Data.SqlClient;
using API_REST_DARIO.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_DARIO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicacionesController : ControllerBase
    {
        private readonly string _con;

        public PublicacionesController(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("DefaultConnection");
        }        

        // GET api/publicaciones
        [HttpGet]
        public IEnumerable<Publicaciones> Get()
        {
            List<Publicaciones> publicaciones = new();
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("GETPosts", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            publicaciones.Add(new Publicaciones
                            {
                                id = Convert.ToInt32(reader["id"]),
                                title = reader["title"].ToString(),
                                content = reader["content"].ToString(),
                                created_at = Convert.ToDateTime(reader["created_at"])
                            });
                        }
                    }                    
                }
            }
            return publicaciones;
        }

        // POST api/publicaciones
        [HttpPost]
        public IActionResult Post([FromBody] Publicaciones publicaciones)
        {
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("CreatePost", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@title", publicaciones.title);
                    cmd.Parameters.AddWithValue("@content", publicaciones.content);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Publicación creada correctamente");
        }

        // PUT api/publicaciones/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Publicaciones publicaciones)
        {
            using (SqlConnection connection = new(_con))            
            {
                connection.Open();
                using (SqlCommand cmd = new("UpdatePost", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@title", publicaciones.title);
                    cmd.Parameters.AddWithValue("@content", publicaciones.content);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Publicación actualizada correctamente");
        }

        // DELETE api/publicaciones/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SqlConnection connection = new(_con))
            {
                connection.Open();
                using (SqlCommand cmd = new("DeletePost", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Publicación eliminada correctamente");
        }        
    }
}   