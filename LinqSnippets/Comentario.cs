using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public DateTime Created { get; set; }
    }
}
