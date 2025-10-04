using System;
using System.Collections.Generic;
using System.Linq;

namespace API_REST_DARIO.Models
{
    public class Publicaciones
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public DateTime created_at { get; set; }
    }
}