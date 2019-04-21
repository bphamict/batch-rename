using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Rename
{
    public class MoveArgs : StringArgs
    {
        public string Details => "Move ISBN to the bottom";
    }

    public class MoveAction : StringAction
    {
        public string Name => "Move ISBN action";

        public StringProcessor Processor => _move;

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new MoveAction()
            {
                Args = new MoveArgs()
            };
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }

        private string _move(string origin)
        {
            string[] words = origin.Split('.');

            return $"{words[0].Substring(17)} - {origin.Substring(0, 13)}.{words[1]}";
        }
    }
}
