using System;
using System.Collections.Generic;
using System.Linq;

namespace API_REST_DARIO.Models
{
    public class Comentarios
    {
        public int id { get; set; }
        public string? content { get; set; }
        public DateTime created_at { get; set; }
        public int post_id { get; set; }
    }
}