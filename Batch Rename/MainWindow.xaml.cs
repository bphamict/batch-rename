using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

        List<StringAction> _protypes = null;

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

        private void Start_Batch_Btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (string filename in Files_ListView.Items)
            {
                string result = filename;

                foreach (StringAction action in Actions_Listbox.Items)
                {
                    result = action.Processor.Invoke(result);
                }

                var file = new FileInfo(filename);
                file.MoveTo(result);
                MessageBox.Show(result);
            }
        }

        private void Add_Method_Btn_Click(object sender, RoutedEventArgs e)
        {
            var prototype = Add_Method_Combobox.SelectedItem as StringAction;
            
            var instance = prototype.Clone();
            Actions_Listbox.Items.Add(instance);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var action = Actions_Listbox.SelectedItem as StringAction;

            action.ShowEditDialog();
        }

        private void Load_Files_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Multiselect = true;

            if (screen.ShowDialog() == true)
            {
                foreach (var file in screen.FileNames)
                {
                    Files_ListView.Items.Add(file);
                }
            }
        }
    }
}
