using System;
using System.Collections.Generic;
using System.Text;

namespace Minuto.Seguros.Infra.CrossCutting.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int LifeTimeMinutes { get; set; }
    }
}
