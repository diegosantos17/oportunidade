using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minuto.Seguros.Service.Contracts;

namespace Minuto.Seguros.Application.Controllers.V1
{
    /// <summary>
    /// Gestão de feeds
    /// </summary>
    [Authorize]
    [ApiVersion("1")]
    [Route("api/v1/feeds")]
    [Produces("application/json")]
    public class FeedsController : ControllerBase
    {
        private readonly IFeedService _feedService;        
        
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="feedService"></param>
        public FeedsController(
            IFeedService feedService            
        )
        {
            _feedService = feedService;            
        }

        /// <summary>
        /// Baixar Feeds
        /// </summary>        
        [AllowAnonymous]
        [HttpGet]        
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult BaixarFeeds()
        {
            try
            {                
                return new ObjectResult(_feedService.GetAllFeeds());
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Contabilizar TOP 10 palavras por Tópico
        /// </summary>                        
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
        /// <returns>Mensagem de confirmação de sucesso ou erro.</returns>
        /// <response code="200">String com o token gerado</response>
        [AllowAnonymous]
        [HttpGet]
        [Route("topics")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult SincronizarFeeds()
        {            
            try
            {
                var result = _feedService.GetFeeds();
                return new ObjectResult(result);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }        
    }
}