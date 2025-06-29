using CommunityToolkit.Mvvm.Messaging;
using ExchangeRate.Common.Navigations;
using ExchangeRate.Services;
using ExchangeRate.Views;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.WithCaller;
using System.Drawing;
using System.IO;
using System.Windows;

namespace ExchangeRate
{
    public partial class App : Application
    {
        private TaskbarIcon? _trayIcon;
        private MainWindow? _mainWindow;
        private IHost? _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Serilog 설정
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithEnvironmentName()
                .Enrich.WithThreadId()
                .Enrich.WithCaller() // 이게 핵심
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day,
                  outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Caller} {Message:lj}{NewLine}{Exception}")
                //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Caller} {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            // 1. DI 호스트 설정
            _host = Host.CreateDefaultBuilder()
                .UseSerilog()
                .ConfigureServices((context, services) =>
                {
                    // ViewModel
                    //services.AddSingleton<ViewModels.MainViewModel>();

                    // View
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<Mainpage>();
                    services.AddSingleton<VersionInfoPage>();

                    // Service
                    services.AddSingleton<LoadHomePage>();
                    services.AddSingleton<GetExchangeRate>();
                    services.AddSingleton<ExecuteTimer>();
                })
                .Build();

            _host.Start();

            var timer = _host.Services.GetRequiredService<ExecuteTimer>();
            Thread thread = new Thread(() => timer.Execute());
            thread.IsBackground = true;
            thread.Start();

            // 2. 트레이 아이콘 초기화
            var iconStream = new MemoryStream(ExchangeRate.Properties.Resources.smile);
            var icon = new Icon(iconStream);

            _trayIcon = new TaskbarIcon
            {
                Icon = icon,
                ToolTipText = "Exchange Rate Program"
            };

            var contextMenu = new System.Windows.Controls.ContextMenu();

            var openMenu = new System.Windows.Controls.MenuItem { Header = "Open" };
            openMenu.Click += (s, args) => ShowMainWindow();

            var exitMenu = new System.Windows.Controls.MenuItem { Header = "Close" };
            exitMenu.Click += (s, args) => ExitMainWindow();

            contextMenu.Items.Add(openMenu);
            contextMenu.Items.Add(exitMenu);
            _trayIcon.ContextMenu = contextMenu;

            _trayIcon.TrayMouseDoubleClick += (s, args) => ShowMainWindow();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            WeakReferenceMessenger.Default.Send(new NavigationMessage(typeof(Mainpage)));
        }

        private void ShowMainWindow()
        {
            if (_mainWindow == null)
            {
                _mainWindow = _host!.Services.GetRequiredService<MainWindow>();
            }

            _mainWindow.Show();
            _mainWindow.WindowState = WindowState.Normal;
            _mainWindow.Activate();
        }

        private void ExitMainWindow()
        {
            _host?.StopAsync().Wait();
            _host?.Dispose();
            Shutdown();
        }
    }

}
