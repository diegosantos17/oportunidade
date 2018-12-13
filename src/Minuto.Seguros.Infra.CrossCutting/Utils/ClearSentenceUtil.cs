using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Minuto.Seguros.Infra.CrossCutting.Utils
{
    public static class ClearSentenceUtil
    {
        /// <summary>
        /// Fontes:     https://www.normaculta.com.br/artigo-indefinido/
        ///             https://www.normaculta.com.br/artigo-definido/
        ///             https://www.normaculta.com.br/preposicao/           
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public static string ClearSentence(string sentence)
        {
            var sentenceClear = string.Concat(" ", sentence, " ");

            sentenceClear = StripHTML(sentenceClear);

            #region | Pontuação

            sentenceClear = sentenceClear
                                    .Replace(",", "")
                                    .Replace(".", "")
                                    .Replace("?", "")
                                    .Replace("!", "")
                                    .Replace(":", "")
                                    .Replace("(", "")
                                    .Replace(")", "")
                                    .Replace(";", "")
                                    .Replace(" - ", " ")
                                    .Replace("\n", "")
                                    .Replace("\"", "");

            #endregion            

            #region | Artigos Definidos

            sentenceClear = sentenceClear
                                    .Replace(" a ", " ")
                                    .Replace(" as ", " ")
                                    .Replace(" A ", " ")
                                    .Replace(" As ", " ")
                                    .Replace(" AS ", " ")
                                    .Replace(" o ", " ")
                                    .Replace(" os ", " ")
                                    .Replace(" O ", " ")
                                    .Replace(" Os ", " ")
                                    .Replace(" OS ", " ")
                                    .Replace(" da ", " ")
                                    .Replace(" Da ", " ")
                                    .Replace(" DA ", " ")
                                    .Replace(" das ", " ")
                                    .Replace(" Das ", " ")
                                    .Replace(" DAS ", " ")
                                    .Replace(" de ", " ")
                                    .Replace(" De ", " ")
                                    .Replace(" DE ", " ")
                                    .Replace(" no ", " ")
                                    .Replace(" No ", " ")
                                    .Replace(" NOS ", " ")
                                    .Replace(" nos ", " ")
                                    .Replace(" Nos ", " ")
                                    .Replace(" NOS ", " ");
            #endregion

            #region | Artigos Indefinidos

            sentenceClear = sentenceClear.Replace(" um ", " ")
                                    .Replace(" Um ", " ")
                                    .Replace(" UM ", " ")
                                    .Replace(" uns ", " ")
                                    .Replace(" Uns ", " ")
                                    .Replace(" UNS ", " ")
                                    .Replace(" dum ", " ")
                                    .Replace(" Dum ", " ")
                                    .Replace(" DUM ", " ")
                                    .Replace(" duns ", " ")
                                    .Replace(" Duns ", " ")
                                    .Replace(" DUNS ", " ")
                                    .Replace(" uma ", " ")
                                    .Replace(" Uma ", " ")
                                    .Replace(" UMA ", " ")
                                    .Replace(" umas ", " ")
                                    .Replace(" Umas ", " ")
                                    .Replace(" UMAS ", " ")
                                    .Replace(" em ", " ")
                                    .Replace(" Em ", " ")
                                    .Replace(" EM ", " ");

            #endregion

            #region | Preposições            

            sentenceClear = sentenceClear
                                    .Replace(" do ", " ")
                                    .Replace(" Do ", " ")
                                    .Replace(" DO ", " ")
                                    .Replace(" dos ", " ")
                                    .Replace(" Dos ", " ")
                                    .Replace(" DOS ", " ")
                                    .Replace(" DOS ", " ")

                                    .Replace(" à ", " ")
                                    .Replace(" às ", " ")
                                    .Replace(" À ", " ")
                                    .Replace(" Às ", " ")
                                    .Replace(" ÀS ", " ")

                                    .Replace(" duma ", " ")
                                    .Replace(" Duma ", " ")
                                    .Replace(" DUMA ", " ")
                                    .Replace(" dumas ", " ")
                                    .Replace(" Dumas ", " ")
                                    .Replace(" DUMAS ", " ")

                                    .Replace(" disto ", " ")
                                    .Replace(" Disto ", " ")
                                    .Replace(" DISTO ", " ")

                                    .Replace(" na ", " ")
                                    .Replace(" Na ", " ")
                                    .Replace(" NA ", " ")
                                    .Replace(" nas ", " ")
                                    .Replace(" Nas ", " ")
                                    .Replace(" NAS ", " ")

                                    .Replace(" ao ", " ")
                                    .Replace(" aos ", " ")
                                    .Replace(" AO ", " ")
                                    .Replace(" Aos ", " ")
                                    .Replace(" AOS ", " ")

                                    .Replace(" num ", " ")
                                    .Replace(" Num ", " ")
                                    .Replace(" NUM ", " ")
                                    .Replace(" nuns ", " ")
                                    .Replace(" Nuns ", " ")
                                    .Replace(" NUNS ", " ")

                                    .Replace(" numa ", " ")
                                    .Replace(" Numa ", " ")
                                    .Replace(" NUMA ", " ")
                                    .Replace(" numas ", " ")
                                    .Replace(" Numas ", " ")
                                    .Replace(" NUMAS ", " ")

                                    .Replace(" pelo ", " ")
                                    .Replace(" Pelo ", " ")
                                    .Replace(" PELO ", " ")
                                    .Replace(" pelos ", " ")
                                    .Replace(" Pelos ", " ")
                                    .Replace(" PELOS ", " ")

                                    .Replace(" pela ", " ")
                                    .Replace(" Pela ", " ")
                                    .Replace(" PELA ", " ")
                                    .Replace(" pelas ", " ")
                                    .Replace(" Pelas ", " ")
                                    .Replace(" PELAS ", " ")

                                    .Replace(" nessa ", " ")
                                    .Replace(" Nessa ", " ")
                                    .Replace(" NESSA", " ")
                                    .Replace(" nessas ", " ")
                                    .Replace(" Nessas ", " ")
                                    .Replace(" NESSAS ", " ")

                                    .Replace(" aonde ", " ")
                                    .Replace(" Aonde ", " ")
                                    .Replace(" AONDE", " ");
                                    
            #endregion

            return sentenceClear.TrimEnd().TrimStart().ToLower();
        }

        private static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
