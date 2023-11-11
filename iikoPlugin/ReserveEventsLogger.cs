using Resto.Front.Api;
using Resto.Front.Api.Data.Brd;
using Resto.Front.Api.Data.Common;
using System;

namespace iikoPlugin
{
    internal class ReserveEventsLogger : IObserver<EntityChangedEventArgs<IReserve>>
    {
        public void OnCompleted() {}

        public void OnError(Exception error)
        {
            PluginContext.Log.Error($"Notifications error:\n{error.Message}");
        }

        public void OnNext(EntityChangedEventArgs<IReserve> value)
        {
            PluginContext.Log.Info($"{value.EventType}\n{value.Entity}");
        }
    }
}
