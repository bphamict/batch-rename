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
    /// Interaction logic for RemoveArgsDialog.xaml
    /// </summary>
    public partial class RemoveArgsDialog : Window
    {
        public string StartIndex;
        public string Count;

        public RemoveArgsDialog(RemoveArgs args)
        {
            InitializeComponent();

            Start_Index_TextBox.Text = args.StartIndex.ToString();
            Count_TextBox.Text = args.Count.ToString();
        }

        private void Ok_Btn_Click(object sender, RoutedEventArgs e)
        {
            StartIndex = Start_Index_TextBox.Text;
            Count = Count_TextBox.Text;

            this.DialogResult = true;
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
