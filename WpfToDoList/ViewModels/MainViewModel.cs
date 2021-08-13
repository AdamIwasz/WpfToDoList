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
using Microsoft.VisualBasic;
using WpfToDoList.Views;

namespace WpfToDoList.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {


        public MainViewModel(string startFile)
        {

            LoadStartFile(startFile);
            DeleteCommand = new RelayCommand(Delete);
            SaveFileCommand = new RelayCommand(SaveFile);
            SaveFileAsCommand = new RelayCommand(SaveFileAs);
            LoadFileComamnd = new RelayCommand(LoadFile);
            SetStartFileComamnd = new RelayCommand(SetStartFile);
            ExitCommand = new RelayCommand(Exit);
            AddTaskCommand = new RelayCommand(AddTask);
            EditTaskCommand = new RelayCommand(EditTask);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        
        public static ObservableCollection<MainModel> _taskList = new ObservableCollection<MainModel>();
        public ObservableCollection<MainModel> TaskList
        {
            get 
            {
                return _taskList;
            }
            set { 
                _taskList = value;
                OnPropertyChanged();
            }
        }


        public static MainModel _selectedTask;
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

        public ICommand SaveFileAsCommand { get; set; }
        private void SaveFileAs(object obj)
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
                saveFileDialog.DefaultExt = ".txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, result);
                    CurrentFile = saveFileDialog.FileName;
                }
            }
            else
            {
                MessageBox.Show("Brak zdań do zapisu!", "Błąd", MessageBoxButton.OK);
            }
        }

        public ICommand SaveFileCommand { get; set; }
        private void SaveFile(object obj)
        {

            var stringBuilder = new StringBuilder();
            foreach (var _task in _taskList)
            {
                stringBuilder.Append(_task.Task);
                stringBuilder.Append(";");
                stringBuilder.AppendLine(_task.Description);
            }
            string result = stringBuilder.ToString();
            if (result.Length > 0)
            {
                System.IO.File.WriteAllText(CurrentFile, result);
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
                    CurrentFile = filePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK);
                }
            }
        }

        public ICommand SetStartFileComamnd {  get; set; }

        private void SetStartFile(Object obj)
        {
            string filePath;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\Users\\Adam Iwaszkiewicz\\Desktop";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }
            System.IO.File.WriteAllText(System.IO.Path.GetFullPath(@"..\..\..\") + "\\Resources\\conf.txt", filePath);
        }

        private void LoadStartFile(string file)
        {
            if(file != null)
            {
                CurrentFile = file;
                file = file.Replace("'\'", "\\");
                String fileContent;
                string[] data;
                try
                {
                    var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
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

        public ICommand ExitCommand { get; set; }
        private void Exit(Object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public ICommand AddTaskCommand { get; set; }

        private void AddTask(Object obj)
        {
            AddDialogView myWindow = new AddDialogView();
            myWindow.Show();
        }

        public ICommand EditTaskCommand { get; set; }

        private void EditTask(Object obj)
        {
            if(_selectedTask != null)
            {
                EditDialogView myWindow = new EditDialogView();
                myWindow.Show();
            }
            else
            {

            }
            
        }

        private string _currentFile;
        public string CurrentFile
        {
            get { return _currentFile; }
            set
            {
                _currentFile = value;
                OnPropertyChanged();
            }
        }

    }
}
