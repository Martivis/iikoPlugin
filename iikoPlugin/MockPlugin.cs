using Resto.Front.Api;
using Resto.Front.Api.Data.Brd;
using Resto.Front.Api.Data.Common;
using Resto.Front.Api.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iikoPlugin
{
    public class MockPlugin : IFrontPlugin
    {
        private readonly IObserver<EntityChangedEventArgs<IReserve>> _reservesObserver;
        private IDisposable _reservesSubscription;
        private IDataProvider _provider;

        public MockPlugin(IObserver<EntityChangedEventArgs<IReserve>> observer, IDataProvider dataProvider)
        {
            _reservesObserver = observer;
            _provider = dataProvider;
            SendData();
            SubscribeToReserveEvents();
        }

        private IClient CreateClient() =>
            PluginContext.Operations.CreateClient(
                id: _provider.GetGuid(),
                name: _provider.GetName(),
                phones: new List<PhoneDto> { _provider.GetPhone() },
                cardNumber: _provider.GetCardNumber(),
                dateCreated: DateTime.Now,
                credentials: PluginContext.Operations.GetCredentials());

        private void SendData()
        {
            var session = PluginContext.Operations.CreateEditSession();

            var tables = PluginContext.Operations.GetTables();
            var tablesToReserve = tables.Take(tables.Count / 3 + 1).ToList();

            var client = CreateClient();

            session.CreateReserve(
                estimatedStartTime: DateTime.Now.AddHours(1),
                client: client,
                tables: tablesToReserve
            );

            PluginContext.Operations.SubmitChanges(PluginContext.Operations.GetCredentials(), session);
        }

        private void SubscribeToReserveEvents()
        {
            _reservesSubscription = PluginContext.Notifications.ReserveChanged.Subscribe(_reservesObserver);
        }

        public void Dispose()
        {
            _reservesSubscription.Dispose();
        }
    }
}
