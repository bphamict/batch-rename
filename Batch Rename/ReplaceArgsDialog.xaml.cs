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
    /// Interaction logic for ReplaceArgsDialog.xaml
    /// </summary>
    public partial class ReplaceArgsDialog : Window
    {
        public string Needle;
        public string Hammer;

        public ReplaceArgsDialog(ReplaceArgs args)
        {
            InitializeComponent();

            Needle_TextBox.Text = args.Needle;
            Hammer_TextBox.Text = args.Hammer;
        }

        private void Ok_Btn_Click(object sender, RoutedEventArgs e)
        {
            Needle = Needle_TextBox.Text;
            Hammer = Hammer_TextBox.Text;

            this.DialogResult = true;
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
