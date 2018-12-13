using System;
using System.Collections.Generic;
using System.Text;

namespace Minuto.Seguros.Domain.Entities
{
    public class Feed: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Categories { get; set; }
    }
}
