using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp2.Models;
using MyApp2.Services;

namespace MyApp2.ViewModels;

/// <summary>
/// ViewModel responsible for managing user data and user-related actions
/// such as adding and removing users, exposing commands and properties
/// for binding in the UI.
/// </summary>
public partial class UsersViewModel : ViewModelBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Observable collection of users loaded from the database.
    /// Bound to UI controls like ListBox to display the users.
    /// </summary>
    public ObservableCollection<User> Users { get; } = new();

    /// <summary>
    /// Username entered in the UI for adding a new user.
    /// </summary>
    [ObservableProperty]
    private string _newUsername;

    /// <summary>
    /// Password entered in the UI for adding a new user.
    /// </summary>
    [ObservableProperty]
    private string _newPassword;

    /// <summary>
    /// Currently selected user in the UI (e.g., from a ListBox).
    /// </summary>
    [ObservableProperty]
    private User? _selectedUser;

    /// <summary>
    /// Command to add a new user asynchronously.
    /// </summary>
    public IAsyncRelayCommand AddUserCommand { get; }

    /// <summary>
    /// Command to remove the selected user asynchronously.
    /// Can only execute if a user is selected.
    /// </summary>
    public IAsyncRelayCommand RemoveUserCommand { get; }

    /// <summary>
    /// Constructs the ViewModel with the injected user service.
    /// Initializes commands and loads users.
    /// </summary>
    /// <param name="userService">Service to handle user data operations.</param>
    public UsersViewModel(IUserService userService)
    {
        _userService = userService;

        AddUserCommand = new CommunityToolkit.Mvvm.Input.AsyncRelayCommand(AddUserAsync);
        RemoveUserCommand = new CommunityToolkit.Mvvm.Input.AsyncRelayCommand(RemoveUserAsync, CanRemoveUser);

        LoadUsersAsync();
    }

    /// <summary>
    /// Called automatically by CommunityToolkit when the SelectedUser property changes.
    /// Raises CanExecuteChanged on RemoveUserCommand to update button enabled state.
    /// </summary>
    /// <param name="oldValue">Previous selected user.</param>
    /// <param name="newValue">New selected user.</param>
    partial void OnSelectedUserChanged(User? oldValue, User? newValue)
    {
        RemoveUserCommand.NotifyCanExecuteChanged();
    }

    /// <summary>
    /// Loads all users asynchronously from the user service and populates the Users collection.
    /// </summary>
    private async Task LoadUsersAsync()
    {
        Users.Clear();
        var usersFromDb = await _userService.GetAllUsersAsync();
        foreach (var user in usersFromDb)
        {
            Users.Add(user);
        }
    }

    /// <summary>
    /// Adds a new user asynchronously using the username and password properties.
    /// Clears the input fields and reloads the user list on success.
    /// </summary>
    private async Task AddUserAsync()
    {
        if (string.IsNullOrWhiteSpace(NewUsername) || string.IsNullOrWhiteSpace(NewPassword))
            return;

        var success = await _userService.AddUserAsync(NewUsername, NewPassword);
        if (success)
        {
            NewUsername = string.Empty;
            NewPassword = string.Empty;
            await LoadUsersAsync();
        }
        else
        {
            // Optional: Handle duplicate username case (e.g., notify user)
        }
    }

    /// <summary>
    /// Determines whether the RemoveUserCommand can execute.
    /// Returns true only if a user is currently selected.
    /// </summary>
    /// <returns>True if a user is selected; otherwise false.</returns>
    private bool CanRemoveUser()
    {
        return SelectedUser != null;
    }

    /// <summary>
    /// Removes the currently selected user asynchronously.
    /// Reloads the user list after removal.
    /// </summary>
    private async Task RemoveUserAsync()
    {
        if (SelectedUser == null)
            return;

        await _userService.DeleteUserAsync(SelectedUser.Username);
        await LoadUsersAsync();
    }
}
