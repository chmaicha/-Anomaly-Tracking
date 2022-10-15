using AnomalyTracking.Business.Mappers.AnomalyDeclarations;
using AnomalyTracking.Business.Mappers.Cavities;
using AnomalyTracking.Business.Mappers.Clients;
using AnomalyTracking.Model.Molds;
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;
using System.Linq;

namespace AnomalyTracking.Business.Mappers.Molds
{
    public class MoldMapper : IMapper<Mold, MoldDb>, IMapper<MoldDb, Mold>
    {
        private readonly AnomalyDeclarationMapper anomalyDeclarationMapper;
        private readonly CavityMapper cavityMapper;

        public MoldMapper()
        {
            this.anomalyDeclarationMapper = new AnomalyDeclarationMapper();
            this.cavityMapper = new CavityMapper();
        }

        public Mold Map(MoldDb entityDb)
        {
            return new Mold()
            {
                Id = entityDb.Id,
                Label = entityDb.Label,
                LastModificationDate = entityDb.LastModificationDate,
                ClientId = entityDb.ClientId,
                Cavities = entityDb.CavityDbs != null ? this.cavityMapper.Map(entityDb.CavityDbs) : null,

            };
        }

        public MoldDb Map(Mold entity)
        {
            return new MoldDb()
            {
                Id = entity.Id,
                Label = entity.Label,
                ClientId = entity.ClientId,
            };
        }

        public IEnumerable<MoldDb> Map(IEnumerable<Mold> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<Mold> Map(IEnumerable<MoldDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}
