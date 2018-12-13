using System;
using System.Collections.Generic;
using System.Text;

namespace Minuto.Seguros.Infra.CrossCutting.Configuration
{
    public class ConfigEmail
    {
        public string MyProperty { get; set; }
        public string Credencial { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Smtp { get; set; }
        public int SmtpPort { get; set; }
        public bool Ssl { get; set; }
        public string EmailsTech { get; set; }

    }
}
