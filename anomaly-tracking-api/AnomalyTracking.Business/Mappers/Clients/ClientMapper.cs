using AnomalyTracking.Business.Mappers.Molds;
using AnomalyTracking.Model.Clients;
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;
using System.Linq;

namespace AnomalyTracking.Business.Mappers.Clients
{
    public class ClientMapper : IMapper<Client, ClientDb>, IMapper<ClientDb, Client>
    {
        private readonly ClientAddressMapper addressMapper;
        private readonly MoldMapper moldMapper;

        public ClientMapper()
        {
            this.addressMapper = new ClientAddressMapper();
            this.moldMapper = new MoldMapper();
        }

        public Client Map(ClientDb entityDb)
        {
            return new Client()
            {
                Id = entityDb.Id,
                Label = entityDb.Label,
                Email = entityDb.Email,
                PhoneNumber = entityDb.PhoneNumber,
                LastModificationDate = entityDb.LastModificationDate,
                AddressId = entityDb.AddressId,
                Address = entityDb.ClientAddressDb != null ? this.addressMapper.Map(entityDb.ClientAddressDb) : null,
                Molds = entityDb.MoldDbs != null ? this.moldMapper.Map(entityDb.MoldDbs) : null,

            };
        }

        public ClientDb Map(Client entity)
        {
            return new ClientDb()
            {
                Id = entity.Id,
                Label = entity.Label,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                AddressId = entity.AddressId,
                ClientAddressDb = entity.Address != null && entity.Address.ZipCode != null ? this.addressMapper.Map(entity.Address) : null,

            };
        }

        public IEnumerable<ClientDb> Map(IEnumerable<Client> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<Client> Map(IEnumerable<ClientDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}
