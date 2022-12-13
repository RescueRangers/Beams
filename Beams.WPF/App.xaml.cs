﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Formatting.Compact;
using Serilog;
using Beams.Core.Interfaces;
using Beams.Core.Services;
using Beams.WPF.Services;
using Beams.WPF.ViewModels;

namespace Beams.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services
#if DEBUG
            .AddLogging(b =>
            {
                var logger = new LoggerConfiguration()
                .WriteTo.Debug(Serilog.Events.LogEventLevel.Verbose)
                .WriteTo.File(new CompactJsonFormatter(), @".\logs\log.txt", Serilog.Events.LogEventLevel.Verbose, rollingInterval: RollingInterval.Day)
                .CreateLogger();

                b.AddSerilog(logger);
            })
#else
                .AddLogging(b =>
                {
                    var logger = new LoggerConfiguration()
                    .WriteTo.File(new CompactJsonFormatter(), @".\logs\log.txt", Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                    .CreateLogger();

                    b.AddSerilog(logger);
                })
#endif
                .AddSingleton<ISideBeam, SideBeam>()
                .AddSingleton<MainWindowViewModel>()
                .AddTransient<MainWindow>()
                .AddSingleton<IMessagingService, WpfMessagingService>();

        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
