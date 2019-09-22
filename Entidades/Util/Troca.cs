using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Util
{
    public class Troca
    {
        private Dictionary<string, string> dicTroca;

        public Troca()
        {
            dicTroca = new Dictionary<string, string>();
            dicTroca.Add("&#033;", "!");
            dicTroca.Add("&#035;", "#");
            dicTroca.Add("&#036;", "$");
            dicTroca.Add("&#037;", "%");
            dicTroca.Add("&#038;", "&");
            dicTroca.Add("&#039;", "'");
            dicTroca.Add("&#040;", "(");
            dicTroca.Add("&#041;", ")");
            dicTroca.Add("&#058;", ":");
            dicTroca.Add("&#059;", ";");
            dicTroca.Add("&#060;", "<");
            dicTroca.Add("&#061;", "=");
            dicTroca.Add("&#062;", ">");
            dicTroca.Add("&#063;", "?");
            dicTroca.Add("&#064;", "@");
            dicTroca.Add("&#091;", "[");
            dicTroca.Add("&#093;", "]");
            dicTroca.Add("&#094;", "^");
            dicTroca.Add("&#095;", "_");
            dicTroca.Add("&#123;", "{");
            dicTroca.Add("&#125;", "}");
            dicTroca.Add("&#126;", "~");
            dicTroca.Add("&#192;", "À");
            dicTroca.Add("&#193;", "Á");
            dicTroca.Add("&#194;", "Â");
            dicTroca.Add("&#195;", "Ã");
            dicTroca.Add("&#199;", "Ç");
            dicTroca.Add("&#200;", "È");
            dicTroca.Add("&#201;", "É");
            dicTroca.Add("&#202;", "Ê");
            dicTroca.Add("&#204;", "Ì");
            dicTroca.Add("&#205;", "Í");
            dicTroca.Add("&#206;", "Î");
            dicTroca.Add("&#209;", "Ñ");
            dicTroca.Add("&#210;", "Ò");
            dicTroca.Add("&#211;", "Ó");
            dicTroca.Add("&#212;", "Ô");
            dicTroca.Add("&#213;", "Õ");
            dicTroca.Add("&#217;", "Ù");
            dicTroca.Add("&#218;", "Ú");
            dicTroca.Add("&#219;", "Û");
            dicTroca.Add("&#224;", "à");
            dicTroca.Add("&#225;", "á");
            dicTroca.Add("&#226;", "â");
            dicTroca.Add("&#227;", "ã");
            dicTroca.Add("&#231;", "ç");
            dicTroca.Add("&#232;", "è");
            dicTroca.Add("&#233;", "é");
            dicTroca.Add("&#234;", "ê");
            dicTroca.Add("&#236;", "ì");
            dicTroca.Add("&#237;", "í");
            dicTroca.Add("&#238;", "î");
            dicTroca.Add("&#241;", "ñ");
            dicTroca.Add("&#242;", "ò");
            dicTroca.Add("&#243;", "ó");
            dicTroca.Add("&#244;", "ô");
            dicTroca.Add("&#245;", "õ");
            dicTroca.Add("&#249;", "ù");
            dicTroca.Add("&#250;", "ú");
            dicTroca.Add("&#251;", "û");
        }

        public string RetornaFraseSemASCII(string frase)
        {
            StringBuilder fraseFormatada = new StringBuilder();

            for (int i = 0; i < frase.Length; i++)
            {
                if (frase[i] == '&' && frase[i + 1] == '#')
                {
                    fraseFormatada.Append(this.TrocarCodigoPorLetra(string.Format("{0}{1}{2}{3}{4}{5}", frase[i], frase[i + 1], frase[i + 2], frase[i + 3], frase[i + 4], frase[i + 5])));
                    i = i + 5;
                }
                else
                    fraseFormatada.Append(frase[i]);
            }
            return fraseFormatada.ToString();

        }
        private string TrocarCodigoPorLetra(string ascii)
        {
            if (dicTroca.ContainsKey(ascii))
            {
                return dicTroca[ascii];
            }
            return string.Empty;
        }
    }
}
