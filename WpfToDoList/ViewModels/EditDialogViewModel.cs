using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfToDoList.Commands;
using WpfToDoList.Models;

namespace WpfToDoList.ViewModels
{
    class EditDialogViewModel
    {
        public EditDialogViewModel()
        {
            InputBox = MainViewModel._selectedTask.Task;
            InputDescriptionBox = MainViewModel._selectedTask.Description;
            EditTextCommand = new RelayCommand(EditText);
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

        public ICommand EditTextCommand { get; set; }
        private void EditText(Object obj)
        {
            if (InputBox.Length > 0)
            {

                MainViewModel._taskList.Remove(MainViewModel._selectedTask);
                MainViewModel._taskList.Add(new MainModel { Task = InputBox, Description = InputDescriptionBox });
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == this) item.Close();
                }

            }
            else
            {
                MessageBox.Show("Należy dodać zadanie", "Błąd", MessageBoxButton.OK);
            }

        }
    }
}
