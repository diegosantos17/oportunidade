using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minuto.Seguros.Application.Extensions.swagger
{
    /// <summary>
    /// Extensão para recuperar nome da api Swagger
    /// </summary>
    public static class ActionDescriptorExtensions
    {
        /// <summary>
        /// Retora versão da api
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static ApiVersionModel GetApiVersion(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor?.Properties
              .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
              .Select(kvp => kvp.Value as ApiVersionModel).FirstOrDefault();
        }
    }
}
