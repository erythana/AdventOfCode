using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AdventOfCode.ViewModels;
using AdventOfCode.Views;

namespace AdventOfCode
{
    public class App : Application
    {
        private readonly SessionCookieStorage _sessionCookieLoader = new();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
                desktop.Startup += DesktopOnStartup;
                desktop.Exit += DesktopOnExit;
            }
            base.OnFrameworkInitializationCompleted();
        }

        private void DesktopOnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            if (sender is ClassicDesktopStyleApplicationLifetime {MainWindow.DataContext: MainWindowViewModel mainWindowViewModel} )
                _sessionCookieLoader.SaveSessionCookie(mainWindowViewModel.SessionCookie);
        }

        private void DesktopOnStartup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
            if (sender is ClassicDesktopStyleApplicationLifetime {MainWindow.DataContext: MainWindowViewModel mainWindowViewModel} )
            {
                var sessionCookie = _sessionCookieLoader.LoadSessionCookie();
                mainWindowViewModel.SessionCookie = sessionCookie;
            }
        }
    }
}