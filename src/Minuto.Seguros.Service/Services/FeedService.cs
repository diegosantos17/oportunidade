using AutoMapper;
using Minuto.Seguros.Domain.Entities;
using Minuto.Seguros.Infra.Client.Corporate.Rss.Contracts;
using Minuto.Seguros.Infra.CrossCutting.Utils;
using Minuto.Seguros.Infra.Data.Contracts;
using Minuto.Seguros.Service.Contracts;
using Minuto.Seguros.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minuto.Seguros.Service.Services
{
    internal class FeedService : BaseService<IFeedRepository, Feed>, IFeedService
    {
        private readonly IFeedRepository _feedRepository;
        private readonly IRssClient _rssClient;
        private readonly IMapper _mapper;

        public FeedService(
            IFeedRepository feedRepository,
            IRssClient rssClient,
            IMapper mapper
            ) : base(feedRepository)
        {
            _feedRepository = feedRepository;
            _rssClient = rssClient;
            _mapper = mapper;
        }

        public List<TopicDto> GetFeeds()
        {
            try
            {
                var feeds = _rssClient.GetFeeds();
                List<FeedDto> feedsMap = new List<FeedDto>();

                foreach (var item in feeds)
                {
                    item.FullMessageClean = ClearSentenceUtil.ClearSentence(string.Concat(item.Title, " ", item.Description, " ", string.Join(", ", item.Categories)));
                    feedsMap.Add(_mapper.Map<FeedDto>(item));
                }

                List<string> words;
                List<WordDto> wordsRecurrence;
                List<TopicDto> topics = new List<TopicDto>();

                foreach (var item in feedsMap)
                {
                    wordsRecurrence = new List<WordDto>();
                    words = item.FullMessageClean.Split(' ').ToList();

                    foreach (var word in words)
                    {
                        if (!wordsRecurrence.Any(w => w.Word == word))
                        {
                            wordsRecurrence.Add(new WordDto()
                            {
                                Word = word,
                                Amount = WordUtil.CountRecurrence(word, words)
                            });
                        }
                    }

                    topics.Add(new TopicDto()
                    {
                        Title = item.Title,
                        TotalWords = wordsRecurrence.Count(),
                        TopWords = wordsRecurrence.OrderByDescending(w => w.Amount).Take(10).ToList(),
                        CleanerText = item.FullMessageClean
                    });
                }

                return topics;

            } catch(Exception ex)
            {
                throw ex;
            }

        }

        public List<FeedDto> GetAllFeeds()
        {
            try { 
                var feeds = _rssClient.GetFeeds();
                List<FeedDto> feedsMap = new List<FeedDto>();

                foreach (var item in feeds)
                {
                    feedsMap.Add(_mapper.Map<FeedDto>(item));

                    /* Sem necessidade, apenas uma amostragem que quis colocar
                     * pra mostrar que se fosse uma integração continua, teríamos fácil uma base de dados para manipulação.
                     */
                    _feedRepository.Add(_mapper.Map<Feed>(item));
                }

                return feedsMap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
