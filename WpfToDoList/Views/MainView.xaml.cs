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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfToDoList.Models;
using WpfToDoList.ViewModels;
using static System.Net.WebRequestMethods;

namespace WpfToDoList.Views
{
 
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            string confFile = ReadConfFile();
            DataContext = new MainViewModel(confFile);
        }

        private string ReadConfFile()
        {
            string filePath = System.IO.Path.GetFullPath(@"..\..\..\") + "\\Resources\\conf.txt";
            string fileContent;
            try
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    while ((fileContent = sr.ReadLine()) != null)
                    {
                        filePath = fileContent;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK);
            }
            return filePath;
        }
    }
}
