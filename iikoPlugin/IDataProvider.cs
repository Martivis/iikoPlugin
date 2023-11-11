using Resto.Front.Api.Data.Brd;
using System;

namespace iikoPlugin
{
    public interface IDataProvider
    {
        string GetCardNumber();
        string GetName();
        Guid GetGuid();
        PhoneDto GetPhone(bool isPrimary = true);
    }
}