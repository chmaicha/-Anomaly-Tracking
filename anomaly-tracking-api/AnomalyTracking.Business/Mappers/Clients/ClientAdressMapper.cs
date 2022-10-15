using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using Shared.Core.Model.Common;

namespace AnomalyTracking.Business.Mappers.Clients
{
    public class ClientAddressMapper : IMapper<ClientAddressDb, Address>, IMapper<Address, ClientAddressDb>
    {
        public ClientAddressDb Map(Address entity)
        {
            return new ClientAddressDb()
            {
                Id = entity.Id,
                StreetNumber = entity.StreetNumber,
                StreetName = entity.StreetName,
                City = entity.City,
                ZipCode = entity.ZipCode,
                CountryCode = entity.CountryCode,
                FurtherInformation = entity.FurtherInformation,
             
            };
        }

        public Address Map(ClientAddressDb entityDb)
        {
            return new Address()
            {
                Id = entityDb.Id,
                StreetNumber = entityDb.StreetNumber,
                StreetName = entityDb.StreetName,
                City = entityDb.City,
                ZipCode = entityDb.ZipCode,
                CountryCode = entityDb.CountryCode,
                FurtherInformation = entityDb.FurtherInformation,
            };
        }
    }
}
