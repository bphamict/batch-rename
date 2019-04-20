using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Batch_Rename
{
    /// <summary>
    /// Interaction logic for ExtensionArgsDialog.xaml
    /// </summary>
    public partial class ExtensionArgsDialog : Window
    {
        public string NewExtension;
        public ExtensionArgsDialog(ExtensionArgs args)
        {
            InitializeComponent();
            Extension_TextBox.Text = NewExtension;
        }

        private void Ok_Btn_Click(object sender, RoutedEventArgs e)
        {
            NewExtension = Extension_TextBox.Text;

            this.DialogResult = true;
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
