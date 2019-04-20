using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Batch_Rename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class File : INotifyPropertyChanged
        {
            public File()
            {
            }

            private void NotifyChange(string v)
            {
                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public string OldName { get; set; }

            private string _NewName;
            public string NewName
            {
                get => _NewName;
                set
                {
                    _NewName = value;
                    NotifyChange("NewName");
                }
            }

            public string Path { get; set; }
            public string Error { get; set; }
        }

        List<StringAction> _protypes = null;
        List<File> _Files = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _protypes = new List<StringAction>()
            {
                new ReplaceAction(),
                new RemoveAction(),
                new ExtensionAction()
            };

            Add_Method_Combobox.ItemsSource = _protypes;
        }

        private void Refresh_Btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (File file in _Files)
            {
                foreach (StringAction action in Actions_ListBox.Items)
                {
                    file.NewName = action.Processor.Invoke(file.OldName);
                }
            }
        }

        private void Start_Batch_Btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (File file in _Files)
            {
                string result = file.Path;

                foreach (StringAction action in Actions_ListBox.Items)
                {
                    result = action.Processor.Invoke(result);
                }

                var f = new FileInfo(file.Path);
                f.MoveTo(result);
                MessageBox.Show(result);
            }
        }

        private void Add_Method_Btn_Click(object sender, RoutedEventArgs e)
        {
            var prototype = Add_Method_Combobox.SelectedItem as StringAction;

            var instance = prototype.Clone();
            Actions_ListBox.Items.Add(instance);
        }

        private void Clear_Method_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var action = Actions_ListBox.SelectedItem as StringAction;

            action.ShowEditDialog();
        }

        private void Load_Files_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Multiselect = true;

            if (screen.ShowDialog() == true)
            {
                _Files = new List<File>();
                foreach (var file in screen.FileNames)
                {
                    File f = new File() { OldName = Path.GetFileName(file), Path = Path.GetFullPath(file) };
                    _Files.Add(f);
                    Files_DataGrid.Items.Add(f);
                }
            }
        }
    }
}
