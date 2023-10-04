using System.Windows;
using Autofac;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer;
using DataAccessLayer.Api;
using Serilog;


namespace OfferGenerationTool
{
    public partial class App : Application
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainViewModel>().As<IMainViewModel>();
            builder.RegisterType<OfferService>().As<IOfferService>();
            builder.RegisterType<TaxpayerApiClient>().As<ITaxpayerApiClient>();
            builder.RegisterType<ExcelFileHandler>().As<IExcelFileHandler>();

            return builder.Build();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Log.CloseAndFlush();
        }
    }
}
