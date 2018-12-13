using Microsoft.Extensions.Options;
using Minuto.Seguros.Infra.Client.Corporate.Rss.Contracts;
using Minuto.Seguros.Infra.Client.Corporate.Rss.Dto;
using Minuto.Seguros.Infra.CrossCutting.Configuration;
using Minuto.Seguros.Infra.CrossCutting.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Minuto.Seguros.Infra.Client.Corporate.Rss
{
    public class RssClient : IRssClient
    {
        private readonly AppSettings _appSettings;

        public RssClient(
            IOptions<AppSettings> appSettings
        )
        {
            _appSettings = appSettings.Value;
        }        

        public List<FeedDto> GetFeeds()
        {
            try { 
                XDocument document = XDocument.Load(_appSettings.UriXmlMinutoSeguros);
                rss Rss = SerializationUtil.Deserialize<rss>(document);

                var feeds = new List<FeedDto>();

                foreach (var item in Rss.channel.item)
                {
                    feeds.Add(new FeedDto()
                    {
                        Title = item.title,
                        Description = item.description,
                        Categories = item.category
                    });
                }

                return feeds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
