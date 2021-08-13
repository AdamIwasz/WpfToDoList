using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfToDoList.Commands;
using WpfToDoList.Models;
using WpfToDoList.Commands;


namespace WpfToDoList.ViewModels 
{
    class AddDialogViewModel : INotifyPropertyChanged
    {


        public AddDialogViewModel(){
            
            InputBox = "";
            AddTextCommand = new RelayCommand(AddTextt);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _inputDescriptionBox;
        public string InputDescriptionBox
        {
            get { return _inputDescriptionBox; }
            set
            {
                _inputDescriptionBox = value;
                OnPropertyChanged();
            }
        }

        private string _inputBox;
        public string InputBox
        {
            get { return _inputBox; }
            set
            {
                _inputBox = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddTextCommand { get; set; }
        private void AddTextt(Object obj)
        {
            if (InputBox.Length > 0)
            {

                MainViewModel._taskList.Add(new MainModel { Task = InputBox, Description = InputDescriptionBox });
                InputBox = "";
                InputDescriptionBox = "";

            }
            else
            {
                MessageBox.Show("Należy dodać zadanie", "Błąd", MessageBoxButton.OK);
            }

        }

    }
}
