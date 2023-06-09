using BlazorWebKit;
using Gtk;
using LinuxDesktop;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Util.Reflection.Expressions;

Application.Init();

// Create the parent window
var window = new Window(WindowType.Toplevel);
window.DefaultSize = new Gdk.Size(1024, 768);
// window.Fullscreen();

window.DeleteEvent += (o, e) =>
{
    Application.Quit();
};

// Add the BlazorWebView
var service = new ServiceCollection()
    .AddBlazorWebViewOptions(new BlazorWebViewOptions()
    {
        RootComponent = typeof(App),
        HostPath = "wwwroot/index.html"
    })
    .AddLogging((lb) =>
    {
        lb.AddSimpleConsole(options =>
        {
            //options.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Disabled;
            //options.IncludeScopes = false;
            //options.SingleLine = true;
            options.TimestampFormat = "hh:mm:ss ";
        })
        .SetMinimumLevel(LogLevel.Information);
    });

service.AddMasaBlazor();
var serviceProvider = service
    .BuildServiceProvider();
var webView = new BlazorWebView(serviceProvider);
window.Add(webView);

// Allow opening developer tools
webView.Settings.EnableDeveloperExtras = true;

window.ShowAll();

Application.Run();
