using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using BusinessLogicLayer.ViewModels;

namespace OfferGenerationTool
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var container = App.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IMainViewModel>();
                DataContext = app;
                datePicker.SelectedDate = DateTime.Now;
            }
        }
    }
}
