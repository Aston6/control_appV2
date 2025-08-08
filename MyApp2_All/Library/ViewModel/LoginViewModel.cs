using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Input;
using Avalonia.Threading;
using MyApp2.Services;

namespace MyApp2.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username = "";
        private string _password = "";
        private string _errorMessage = "";

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (SetProperty(ref _errorMessage, value))
                {
                    Console.WriteLine("ErrorMessage changed to: {0}", value);
                    OnPropertyChanged(nameof(ErrorMessage)); // Explicitly raise PropertyChanged
                }
            }
        }

        public ICommand LoginCommand { get; }
        public event Action? LoginSucceeded;

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IUserService _userService;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new AsyncRelayCommand(OnLoginAsync);
        }

        

    
        private async Task OnLoginAsync()
        {
            var success = await _userService.ValidateLoginAsync(Username, Password);
            if (success)
            {
                LoginSucceeded?.Invoke();
            }
            else
            {
                ErrorMessage = "Invalid login";
            }
        }


        public void CheckLogin()
        {
            Dispatcher.UIThread.Post(() =>
            {
                Console.WriteLine("Inside Dispatcher.UIThread.Post");
                if (Username == "1" && Password == "1")
                {
                    Console.WriteLine("True Message");
                    ErrorMessage = "Login Success";
                    Task.Delay(2000).ContinueWith(_ => LoginSucceeded?.Invoke(), TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
                    ErrorMessage = "Invalid username or password";
                    Console.WriteLine("ErrorMessage set to: {0}", ErrorMessage);
                }
            });
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Async ICommand implementation
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool>? _canExecute;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => !_isExecuting && (_canExecute?.Invoke() ?? true);

        public async void Execute(object? parameter)
        {
            if (!CanExecute(parameter))
                return;

            try
            {
                _isExecuting = true;
                RaiseCanExecuteChanged();
                await _execute();
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
