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
using WpfToDoList.ViewModels;

namespace WpfToDoList.Views
{
    /// <summary>
    /// Logika interakcji dla klasy EditDialogView.xaml
    /// </summary>
    public partial class EditDialogView : Window
    {
        public EditDialogView()
        {
            try
            {
                InitializeComponent();
                DataContext = new EditDialogViewModel();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Należy wybrać dane do edycji", "Błąd", MessageBoxButton.OK);
            }
            
        }
    }
}
