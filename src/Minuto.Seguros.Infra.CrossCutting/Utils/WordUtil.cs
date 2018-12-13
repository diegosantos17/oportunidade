using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minuto.Seguros.Infra.CrossCutting.Utils
{
    public static class WordUtil
    {
        public static int CountRecurrence(string word, List<string> words)
        {
            return words.Count(w => w == word);
        }
    }
}
