using Microsoft.Extensions.DependencyInjection;
using Resto.Front.Api;
using Resto.Front.Api.Data.Brd;
using Resto.Front.Api.Data.Common;
using System;

namespace iikoPlugin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDataProvider, MockDataProvider>();
            services.AddTransient<IObserver<EntityChangedEventArgs<IReserve>>, ReserveEventsLogger>();
            services.AddTransient<IFrontPlugin, MockPlugin>();

            var provider = services.BuildServiceProvider();

            var plugin = provider.GetService<IFrontPlugin>();
        }
    }
}
