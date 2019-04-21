using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Rename
{
    public class NewCaseArgs : StringArgs, INotifyPropertyChanged
    {
        private string _NewCase;

        public string NewCase
        {
            get => _NewCase;
            set
            {
                _NewCase = value;
                NotifyChange("NewCase");
                NotifyChange("Details");
            }
        }

        public string Details => $"Change type name to {NewCase + " letters"}";

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    class NewCaseAction : StringAction
    {
        public string Name => "New case action";

        public StringProcessor Processor => _newcase;

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new NewCaseAction()
            {
                Args = new NewCaseArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new NewCaseArgsDialog(Args as NewCaseArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as NewCaseArgs;
                myArgs.NewCase = screen.NewCase;
            }
        }

        private string _newcase(string origin)
        {
            var myArgs = Args as NewCaseArgs;

            string result = origin;

            switch (myArgs.NewCase)
            {
                case "Upper":
                    result = result.ToUpper();
                    break;
                case "Lower":
                    result = result.ToLower();
                    break;
                case "Upper First":
                    result = result.ToLower();

                    var ch = new StringBuilder(result);

                    for (int i = 0; i < ch.Length; i++)
                    {
                        if (i == 0)
                            ch[i] = Char.ToUpper(ch[i]);
                        else
                        {
                            if (ch[i - 1] == ' ')
                                ch[i] = Char.ToUpper(ch[i]);
                        }
                    }

                    result = ch.ToString();
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
