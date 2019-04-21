using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Rename
{
    class NormalizeArgs : StringArgs
    {
        public string Details => "Normalize full name";
    }

    class NormalizeAction : StringAction
    {
        public string Name => "Normalize action";

        public StringProcessor Processor => _normalize;

        private string _normalize(string origin)
        {
            var s = origin.Split('.');

            string result = s[0].Trim();

            result = result.ToLower();

            var ch = new StringBuilder(result);

            for (int i = 0; i < ch.Length; i++)
            {
                if (i == 0)
                {
                    ch[i] = Char.ToUpper(ch[i]);
                }
                else
                {
                    if (ch[i - 1] == ' ')
                    {
                        ch[i] = Char.ToUpper(ch[i]);
                    }
                }
            }

            result = "";

            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] != ' ')
                {
                    result += ch[i];
                }
                else if (ch[i - 1] != ' ')
                {
                    result += ch[i];
                }
            }

            return $"{result}.{s[1]}";
        }

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new NormalizeAction()
            {
                Args = new NormalizeArgs()
            };
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }
    }
}
