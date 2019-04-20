using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Rename
{
    public class RemoveArgs : StringArgs
    {
        internal string NewExtension;

        public int StartIndex { get; set; }
        public int Count { get; set; }

        public string Details => $"Remove {Count} characters from index: {StartIndex}";
    }

    public class RemoveAction : StringAction
    {
        public string Name => "Remove action";

        public StringProcessor Processor => _remove;

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new RemoveAction()
            {
                Args = new RemoveArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new RemoveArgsDialog(Args as RemoveArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as RemoveArgs;
                myArgs.StartIndex = screen.StartIndex;
                myArgs.Count = screen.Count;
            }
        }

        private string _remove(string origin)
        {
            var myArgs = Args as RemoveArgs;
            var startIndex = myArgs.StartIndex;
            var count = myArgs.Count;

            return origin.Remove(startIndex, count);
        }
    }
}
