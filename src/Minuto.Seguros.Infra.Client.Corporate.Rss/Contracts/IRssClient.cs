using Minuto.Seguros.Infra.Client.Corporate.Rss.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Minuto.Seguros.Infra.Client.Corporate.Rss.Contracts
{
    public interface IRssClient
    {
        List<FeedDto> GetFeeds();        
    }
}
