using Avalonia.Controls;
using Avalonia.Threading; // Needed for DispatcherTimer
using MyApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks; // Required for Task and CancellationTokenSource if you re-add async logic

namespace MyApp2.Views
{
    public partial class ControlPanelView : UserControl
    {
        private DispatcherTimer _continuousActionTimer;
        private ControlPanelViewModel _viewModel => (ControlPanelViewModel)DataContext;

        public ControlPanelView()
        {
            InitializeComponent();
        }

        // This method is called when the UserControl is loaded into the visual tree
        private void UserControl_Loaded(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Set up the timer to check button states periodically
            _continuousActionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(30) // Check every 50ms (adjust for desired responsiveness/CPU usage)
            };
            _continuousActionTimer.Tick += OnContinuousActionTimerTick;
            _continuousActionTimer.Start();
        }

        // This method is called when the UserControl is unloaded from the visual tree
        private void UserControl_Unloaded(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Stop and dispose the timer to prevent memory leaks and unnecessary processing
            _continuousActionTimer?.Stop();
            _continuousActionTimer.Tick -= OnContinuousActionTimerTick;
            _continuousActionTimer = null;
        }

        // This method is called repeatedly by the DispatcherTimer
        private void OnContinuousActionTimerTick(object sender, EventArgs e)
        {
            if (_viewModel == null) return; // Ensure ViewModel is set

            // Check the IsPressed state of each directional button
            // If pressed, execute the corresponding command on the ViewModel

            if (FrontButton.IsPressed)
            {
                _viewModel.MoveFrontCommand?.Execute(null);
            }
            if (BackButton.IsPressed)
            {
                _viewModel.MoveBackCommand?.Execute(null);
            }
            if (LeftButton.IsPressed)
            {
                _viewModel.MoveLeftCommand?.Execute(null);
            }
            if (RightButton.IsPressed)
            {
                _viewModel.MoveRightCommand?.Execute(null);
            }
            // The Stop button is handled by its direct Command binding on click
        }
    }
}