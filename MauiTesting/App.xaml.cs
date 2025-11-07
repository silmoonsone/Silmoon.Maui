using Silmoon.Maui.Services;

namespace MauiTesting
{
    public partial class App : Application
    {
        public static IFileService FileService { get; private set; }

        public App(IFileService fileService)
        {
            FileService = fileService;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Task.Run(() =>
            {
                FileService.CopyResourceRawFilesToAppData([("AboutAssets.txt", true)]);
            }).Wait();

            return new Window(new AppShell());
        }
    }
}