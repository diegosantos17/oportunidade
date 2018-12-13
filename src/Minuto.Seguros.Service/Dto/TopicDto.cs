using System;
using System.Collections.Generic;
using System.Text;

namespace Minuto.Seguros.Service.Dto
{
    public class TopicDto
    {
        public string Title { get; set; }
        public List<WordDto> TopWords{ get; set; }
        public int TotalWords { get; set; }
        public string CleanerText { get; set; }
    }
}
