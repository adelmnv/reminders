using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Study
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            Abstractions.IModel model = new Model();
            MainWindow view = new MainWindow();

            Presenter presenter = new Presenter(model, view);

            App app = new App();
            app.Run(view);
        }
    }
}
