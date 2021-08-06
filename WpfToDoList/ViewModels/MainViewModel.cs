using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfToDoList.Commands;
using System.Windows;
using WpfToDoList.Models;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace WpfToDoList.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            InputBox = "";
            AddTextCommand = new RelayCommand(AddText);
            DeleteCommand = new RelayCommand(Delete);
            SaveFileCommand = new RelayCommand(SaveFile);
            LoadFileComamnd = new RelayCommand(LoadFile);
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

        private string _outputDescriptionBox;
        public string OutputDescriptionBox
        {
            get { return _outputDescriptionBox; }
            set
            {
                _outputDescriptionBox = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MainModel> _taskList = new ObservableCollection<MainModel>();
        public ObservableCollection<MainModel> TaskList
        {
            get 
            {
                return _taskList;
            }
            set { _taskList = value; }
        }

        private MainModel _selectedTask;
        public MainModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                try
                {
                    OutputDescriptionBox = _selectedTask.Description;
                }catch (Exception ex)
                {

                }
                
                OnPropertyChanged();
            }
        }



        public ICommand DeleteCommand {  get; set; }
        private void Delete(Object obj)
        {
            if(SelectedTask != null)
            {
                TaskList.Remove(SelectedTask);
                OutputDescriptionBox = "";
            }
            else
            {
                MessageBox.Show("Należy wybrać dane do usunięcia", "Błąd", MessageBoxButton.OK);
            }
        }

        public ICommand AddTextCommand { get; set; }
        private void AddText(Object obj)
        {
            if (InputBox.Length > 0)
            {
                TaskList.Add(new MainModel { Task = InputBox, Description = InputDescriptionBox });
                InputBox = "";
                InputDescriptionBox = "";
            }
            else
            {
                MessageBox.Show("Należy dodać zadanie", "Błąd", MessageBoxButton.OK);
            }
            
        }

        public ICommand SaveFileCommand { get; set; }
        private void SaveFile(object obj)
        {
            
            var stringBuilder = new StringBuilder();
            foreach(var _task in _taskList)
            {
                stringBuilder.Append(_task.Task);
                stringBuilder.Append(";");
                stringBuilder.AppendLine(_task.Description);
            }
            string result = stringBuilder.ToString();
            if(result.Length>0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, result);
                }
            }
            else
            {
                MessageBox.Show("Brak zdań do zapisu!", "Błąd", MessageBoxButton.OK);
            }
        }

        public ICommand LoadFileComamnd { get; set; }

        private void LoadFile(object obj)
        {
            TaskList.Clear();
            String fileContent;
            string[] data;
            String filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\Users\\Adam Iwaszkiewicz\\Desktop";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        while ((fileContent = sr.ReadLine()) != null)
                        {
                            data = fileContent.Split(';');
                            TaskList.Add(new MainModel { Task = data[0], Description = data[1] }); 
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK);
                }
            }
        }

    }
}
