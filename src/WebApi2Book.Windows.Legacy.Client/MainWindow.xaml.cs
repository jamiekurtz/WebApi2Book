// MainWindow.xaml.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using WebApi2Book.Windows.Legacy.Client.TaskServiceReference;

namespace WebApi2Book.Windows.Legacy.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private bool _isProcessing;
        private TaskServiceReference.Task _task;
        private int? _taskId;
        private bool _useWebApi;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            GetTasksCommand = new DelegateCommand(GetTasks, AllowFetch);
            GetTaskCommand = new DelegateCommand(GetTask, AllowIndividialTaskFetch);

            Tasks = new ObservableCollection<TaskServiceReference.Task>();
        }

        public ObservableCollection<TaskServiceReference.Task> Tasks { get; private set; }

        public DelegateCommand GetTasksCommand { get; private set; }
        public DelegateCommand GetTaskCommand { get; private set; }

        public TaskServiceReference.Task Task
        {
            get { return _task; }
            set
            {
                _task = value;
                OnPropertyChanged("Task");
            }
        }

        public int? TaskId
        {
            get { return _taskId; }
            set
            {
                _taskId = value;
                OnPropertyChanged("TaskId");
                GetTaskCommand.RaiseCanExecuteChanged();
            }
        }

        public bool UseWebApi
        {
            get { return _useWebApi; }
            set
            {
                _useWebApi = value;
                OnPropertyChanged("UseWebApi");
            }
        }

        public bool IsProcessing
        {
            get { return _isProcessing; }
            set
            {
                _isProcessing = value;
                OnPropertyChanged("IsProcessing");

                GetTaskCommand.RaiseCanExecuteChanged();
                GetTasksCommand.RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private bool AllowIndividialTaskFetch()
        {
            return AllowFetch() && TaskId.HasValue;
        }

        private bool AllowFetch()
        {
            return !IsProcessing;
        }

        public void DisplayErrorMessage(string errorMessage)
        {
            var formattedErrorMessage =
                string.Format("An error has occurred while processing the request.{0}{1}Details:{2}{3}",
                    Environment.NewLine, Environment.NewLine, Environment.NewLine, errorMessage);

            MessageBox.Show(formattedErrorMessage, "System Message");
        }

        public async void GetTasks()
        {
            IsProcessing = true;

            try
            {
                Tasks.Clear();
                var result = await System.Threading.Tasks.Task.Run(() => GetTasksAsync());
                result.Body.GetTasksResult.ToList().ForEach(x => Tasks.Add(x));
            }
            catch (Exception e)
            {
                DisplayErrorMessage(e.Message);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private async void GetTask()
        {
            IsProcessing = true;

            try
            {
                Task = null;
                var result = await System.Threading.Tasks.Task.Run(() => GetTaskAsync());
                Task = result.Body.GetTaskByIdResult;
            }
            catch (Exception e)
            {
                DisplayErrorMessage(e.Message);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        public TeamTaskServiceSoapClient GetServiceClient()
        {
            var taskServiceSoapClient = UseWebApi
                ? new TeamTaskServiceSoapClient("TeamTaskServiceViaRest")
                : new TeamTaskServiceSoapClient("TeamTaskServiceSoap");
            return taskServiceSoapClient;
        }

        public async Task<GetTasksResponse> GetTasksAsync()
        {
            var taskServiceSoapClient = GetServiceClient();
            taskServiceSoapClient.Open();

            try
            {
                var result = await taskServiceSoapClient.GetTasksAsync();
                return result;
            }
            finally
            {
                taskServiceSoapClient.Close();
            }
        }

        public async Task<GetTaskByIdResponse> GetTaskAsync()
        {
            var taskServiceSoapClient = GetServiceClient();
            taskServiceSoapClient.Open();

            try
            {
                var result = await taskServiceSoapClient.GetTaskByIdAsync(TaskId.Value);
                return result;
            }
            finally
            {
                taskServiceSoapClient.Close();
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}