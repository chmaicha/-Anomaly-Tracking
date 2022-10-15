using AnomalyTracking.Model.Cavities;
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;
using System.Linq;

namespace AnomalyTracking.Business.Mappers.Cavities
{
    public class CavityMapper : IMapper<Cavity, CavityDb>, IMapper<CavityDb, Cavity>
    {
       
        public Cavity Map(CavityDb entityDb)
        {
            return new Cavity()
            {
                Id = entityDb.Id,
                Label = entityDb.Label,
                LastModificationDate = entityDb.LastModificationDate,
                MoldId = entityDb.MoldId,
            };
        }

        public CavityDb Map(Cavity entity)
        {
            return new CavityDb()
            {
                Id = entity.Id,
                Label = entity.Label,
                MoldId= entity.MoldId,
            };
        }

        public IEnumerable<CavityDb> Map(IEnumerable<Cavity> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<Cavity> Map(IEnumerable<CavityDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}
