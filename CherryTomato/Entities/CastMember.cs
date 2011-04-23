using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Entities
{
    public class CastMember
    {
        public string Name { get; set; }
        public List<string> Characters { get; set; }

        public CastMember()
        {
            Characters = new List<string>();
        }
    }
}
