using Minuto.Seguros.Domain.Entities;
using Minuto.Seguros.Infra.Data.Contracts;
using Minuto.Seguros.Service.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minuto.Seguros.Service.Contracts
{    
    public interface IFeedService : IBaseService<IFeedRepository, Feed>
    {
        List<TopicDto> GetFeeds();
        List<FeedDto> GetAllFeeds();
    }
}
