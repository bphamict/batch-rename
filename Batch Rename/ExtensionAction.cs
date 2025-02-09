﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch_Rename
{
    public class ExtensionArgs : StringArgs, INotifyPropertyChanged
    {
        private string _NewExtension;

        public string NewExtension
        {
            get => _NewExtension;
            set
            {
                _NewExtension = value;
                NotifyChange("NewExtension");
                NotifyChange("Details");
            }
        }

        public string Details => $"Change extension by \"{NewExtension}\"";

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    class ExtensionAction : StringAction
    {
        public string Name => "New extension";

        public StringProcessor Processor => _changeExtension;

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new ExtensionAction()
            {
                Args = new ExtensionArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new ExtensionArgsDialog(Args as ExtensionArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as ExtensionArgs;
                myArgs.NewExtension = screen.NewExtension;
            }
        }

        private string _changeExtension(string origin)
        {
            var myArgs = Args as ExtensionArgs;
            var NewExtension = myArgs.NewExtension;

            var foundPos = origin.LastIndexOf(".");
            var beginning = origin.Substring(0, foundPos);

            return $"{beginning}.{NewExtension}";
        }
    }
}
