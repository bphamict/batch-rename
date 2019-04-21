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
    /// Interaction logic for NewCaseArgsDialog.xaml
    /// </summary>
    public partial class NewCaseArgsDialog : Window
    {
        public string NewCase;

        public NewCaseArgsDialog(NewCaseArgs args)
        {
            InitializeComponent();

            NewCase = args.NewCase;

            switch (args.NewCase)
            {
                case "Upper":
                    Btn_1.IsChecked = true;
                    break;
                case "Lower":
                    Btn_2.IsChecked = true;
                    break;
                case "Upper First":
                    Btn_3.IsChecked = true;
                    break;
                default:
                    break;
            }
        }

        private void Ok_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Btn_1.IsChecked == true)
            {
                NewCase = "Upper";
            }
            else if (Btn_2.IsChecked == true)
            {
                NewCase = "Lower";
            }
            else if (Btn_3.IsChecked == true)
            {
                NewCase = "Upper First";
            }

            this.DialogResult = true;
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
