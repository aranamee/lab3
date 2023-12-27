using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Composition
    {
        private static int nextId = 1;
        public string Artist { get; set; }
        public string Title { get; set; }

        public int Id { get; set; }

        public Composition()
        {
            Id = nextId++;
        }
    }
}
