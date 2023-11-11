using Resto.Front.Api.Data.Brd;
using System;

namespace iikoPlugin
{
    internal class MockDataProvider : IDataProvider
    { 
        public PhoneDto GetPhone(bool isPrimary = true) =>
            new PhoneDto
            {
                PhoneValue = $"+79991112233",
                IsMain = true
            };

        public string GetName() => "Alice";

        public string GetCardNumber() => string.Empty;

        public Guid GetGuid() => Guid.NewGuid();
    }
}
