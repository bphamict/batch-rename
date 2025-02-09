﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Rename
{
    public class RemoveArgs : StringArgs, INotifyPropertyChanged
    {
        private string _StartIndex;
        private string _Count;

        public string StartIndex
        {
            get => _StartIndex;
            set
            {
                _StartIndex = value;
                NotifyChange("StartIndex");
                NotifyChange("Details");
            }
        }

        public string Count
        {
            get => _Count;
            set
            {
                _Count = value;
                NotifyChange("Count");
                NotifyChange("Details");
            }
        }

        public string Details => $"Remove {Count} characters from index {StartIndex}";

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
        }
        public event PropertyChangedEventHandler PropertyChanged;
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

            return origin.Remove(int.Parse(startIndex), int.Parse(count));
        }
    }
}
