using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
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
using System.Collections.ObjectModel;
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

        public class file
        {
            public file() { }
            public string OldName { get; set; }
            public string NewName { get; set; }
            public string Path { get; set; }
            public string Error { get; set; }
        }

        List<StringAction> _protypes = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _protypes = new List<StringAction>()
            {
                new ReplaceAction(),
                new RemoveAction(),
                new ExtensionAction(),
                new MoveAction(),
                new UniqueNameAction(),
                new NewCaseAction(),
                new NormalizeAction()
            };

            Add_Method_Combobox.ItemsSource = _protypes;
        }

        private void Refresh_Btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (file file in Files_DataGrid.Items)
            {
                foreach (StringAction action in Actions_ListBox.Items)
                {
                    file.NewName = action.Processor.Invoke(file.OldName);
                }
            }

            Files_DataGrid.Items.Refresh();

            foreach (file file in Folders_DataGrid.Items)
            {
                foreach (StringAction action in Actions_ListBox.Items)
                {
                    file.NewName = action.Processor.Invoke(file.OldName);
                }
            }
            Folders_DataGrid.Items.Refresh();
        }

        private void Start_Batch_Btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (file file in Files_DataGrid.Items)
            {
                string dir = Path.GetDirectoryName(file.Path);
                string result = Path.GetFileName(file.Path);

                foreach (StringAction action in Actions_ListBox.Items)
                {
                    result = action.Processor.Invoke(result);
                }

                var f = new FileInfo(file.Path);
                f.MoveTo(dir + "\\" + result);
                MessageBox.Show(result);

                Refresh_Btn_Click(sender, e);
            }

            foreach (file file in Folders_DataGrid.Items)
            {
                string dir = Path.GetDirectoryName(file.Path);
                string result = Path.GetFileName(file.Path);

                foreach (StringAction action in Actions_ListBox.Items)
                {
                    result = action.Processor.Invoke(result);
                }

                var f = new FileInfo(file.Path);
                f.MoveTo(dir + "\\" + result);
                MessageBox.Show(result);

                Refresh_Btn_Click(sender, e);
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
            Actions_ListBox.Items.Clear();
            Files_DataGrid.Items.Clear();
            Folders_DataGrid.Items.Clear();
        }

        private void Top_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Actions_ListBox.SelectedIndex;
            if (index != 0)
            {
                var item = Actions_ListBox.Items[index];
                Actions_ListBox.Items[index] = Actions_ListBox.Items[0];
                Actions_ListBox.Items[0] = item;
                Actions_ListBox.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("You has moved to Top");
            }
        }

        private void Up_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Actions_ListBox.SelectedIndex;
            if (index != 0)
            {
                var item = Actions_ListBox.Items[index];
                Actions_ListBox.Items[index] = Actions_ListBox.Items[index - 1];
                Actions_ListBox.Items[index - 1] = item;
                Actions_ListBox.SelectedIndex = index - 1;
            }
            else
            {
                MessageBox.Show("You has moved to Top");
            }
        }

        private void Down_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Actions_ListBox.SelectedIndex;
            if (index != Actions_ListBox.Items.Count - 1)
            {
                var item = Actions_ListBox.Items[index];
                Actions_ListBox.Items[index] = Actions_ListBox.Items[index + 1];
                Actions_ListBox.Items[index + 1] = item;
                Actions_ListBox.SelectedIndex = index + 1;
            }
            else
            {
                MessageBox.Show("You has moved to Bottom");
            }
        }

        private void Bottom_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Actions_ListBox.SelectedIndex;
            var length = Actions_ListBox.Items.Count - 1;

            if (index != length)
            {
                var item = Actions_ListBox.Items[index];
                Actions_ListBox.Items[index] = Actions_ListBox.Items[length];
                Actions_ListBox.Items[length] = item;
                Actions_ListBox.SelectedIndex = length;
            }
            else
            {
                MessageBox.Show("You has moved to Bottom");
            }
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
                foreach (var file in screen.FileNames)
                {
                    Files_DataGrid.Items.Add(new file() { OldName = Path.GetFileName(file), Path = Path.GetFullPath(file) });
                }
            }
        }

        private void Load_Files_Folders_Click(object sender, RoutedEventArgs e)
        {
            var screen = new CommonOpenFileDialog();
            screen.IsFolderPicker = true;
            screen.Multiselect = true;

            if (screen.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (var file in screen.FileNames)
                {
                    string[] filePaths = Directory.GetFiles(file);
                    foreach (var f in filePaths)
                    {
                        Files_DataGrid.Items.Add(new file() { OldName = Path.GetFileName(f), Path = Path.GetFullPath(f) });
                    }
                }
            }
        }

        private void Load_Folders_Click(object sender, RoutedEventArgs e)
        {
            var screen = new CommonOpenFileDialog();
            screen.IsFolderPicker = true;
            screen.Multiselect = true;

            if (screen.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (var file in screen.FileNames)
                {
                    Folders_DataGrid.Items.Add(new file() { OldName = Path.GetFileName(file), Path = Path.GetFullPath(file) });
                }
            }
        }

        private void Top_File_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Files_DataGrid.SelectedIndex;
            if (index != 0)
            {
                var item = Files_DataGrid.Items[index];
                Files_DataGrid.Items[index] = Files_DataGrid.Items[0];
                Files_DataGrid.Items[0] = item;
                Files_DataGrid.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("You has moved to Top");
            }
        }

        private void Up_File_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Files_DataGrid.SelectedIndex;
            if (index != 0)
            {
                var item = Files_DataGrid.Items[index];
                Files_DataGrid.Items[index] = Files_DataGrid.Items[index - 1];
                Files_DataGrid.Items[index - 1] = item;
                Files_DataGrid.SelectedIndex = index - 1;
            }
            else
            {
                MessageBox.Show("You has moved to Top");
            }
        }

        private void Down_File_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Files_DataGrid.SelectedIndex;
            if (index != Files_DataGrid.Items.Count - 1)
            {
                var item = Files_DataGrid.Items[index];
                Files_DataGrid.Items[index] = Files_DataGrid.Items[index + 1];
                Files_DataGrid.Items[index + 1] = item;
                Files_DataGrid.SelectedIndex = index + 1;
            }
            else
            {
                MessageBox.Show("You has moved to Bottom");
            }
        }

        private void Bottom_File_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Files_DataGrid.SelectedIndex;
            var length = Files_DataGrid.Items.Count - 1;

            if (index != length)
            {
                var item = Files_DataGrid.Items[index];
                Files_DataGrid.Items[index] = Files_DataGrid.Items[length];
                Files_DataGrid.Items[length] = item;
                Files_DataGrid.SelectedIndex = length;
            }
            else
            {
                MessageBox.Show("You has moved to Bottom");
            }
        }

        private void Top_Folder_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Folders_DataGrid.SelectedIndex;
            if (index != 0)
            {
                var item = Folders_DataGrid.Items[index];
                Folders_DataGrid.Items[index] = Folders_DataGrid.Items[0];
                Folders_DataGrid.Items[0] = item;
                Folders_DataGrid.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("You has moved to Top");
            }
        }

        private void Up_Folder_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Folders_DataGrid.SelectedIndex;
            if (index != 0)
            {
                var item = Folders_DataGrid.Items[index];
                Folders_DataGrid.Items[index] = Folders_DataGrid.Items[index - 1];
                Folders_DataGrid.Items[index - 1] = item;
                Folders_DataGrid.SelectedIndex = index - 1;
            }
            else
            {
                MessageBox.Show("You has moved to Top");
            }
        }

        private void Down_Folder_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Folders_DataGrid.SelectedIndex;
            if (index != Folders_DataGrid.Items.Count - 1)
            {
                var item = Folders_DataGrid.Items[index];
                Folders_DataGrid.Items[index] = Folders_DataGrid.Items[index + 1];
                Folders_DataGrid.Items[index + 1] = item;
                Folders_DataGrid.SelectedIndex = index + 1;
            }
            else
            {
                MessageBox.Show("You has moved to Bottom");
            }
        }

        private void Bottom_Folder_Btn_Click(object sender, RoutedEventArgs e)
        {
            var index = Folders_DataGrid.SelectedIndex;
            var length = Folders_DataGrid.Items.Count - 1;

            if (index != length)
            {
                var item = Folders_DataGrid.Items[index];
                Folders_DataGrid.Items[index] = Folders_DataGrid.Items[length];
                Folders_DataGrid.Items[length] = item;
                Folders_DataGrid.SelectedIndex = length;
            }
            else
            {
                MessageBox.Show("You has moved to Bottom");
            }
        }
    }
}
