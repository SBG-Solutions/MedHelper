namespace MedHelper
{
    using System.Windows;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// This is the main Application entry point (sort of like ```void main()``` in c-like syntax)
        /// </summary>
        public App()
        {
            using (Sentry.SentrySdk.Init("https://0873f71d79894dd39c466e91196bd63d@sentry.io/1442762"))
                InitializeComponent();
        }
    }
}