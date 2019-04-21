using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Rename
{
    class UniqueNameArgs : StringArgs
    {
        public string Details => "Unique naming";
    }


    class UniqueNameAction : StringAction
    {
        public string Name => "Unique name action";

        public StringProcessor Processor => _unique;

        private string _unique(string origin)
        {
            string[] words = origin.Split('.');

            Guid id = Guid.NewGuid();

            return $"{id}.{words[1]}";
        }

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new UniqueNameAction()
            {
                Args = new UniqueNameArgs()
            };
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }
    }
}
