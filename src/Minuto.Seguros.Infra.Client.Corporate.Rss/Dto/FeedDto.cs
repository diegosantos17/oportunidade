using System;
using System.Collections.Generic;
using System.Text;

namespace Minuto.Seguros.Infra.Client.Corporate.Rss.Dto
{
    public class FeedDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Categories { get; set; }
        public string FullMessageClean { get; set; }
    }
}
