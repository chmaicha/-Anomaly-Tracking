using AnomalyTracking.Business.Mappers.Anomalies;
using AnomalyTracking.Model.AnomalyTypes;
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;
using System.Linq;

namespace AnomalyTracking.Business.Mappers.AnomalyTypes
{
    public class AnomalyTypeMapper : IMapper<AnomalyType, AnomalyTypeDb>, IMapper<AnomalyTypeDb, AnomalyType>
    {
        private readonly AnomalyMapper anomalyMapper;

        public AnomalyTypeMapper()
        {
            this.anomalyMapper = new AnomalyMapper();
        }
        public AnomalyType Map(AnomalyTypeDb entityDb)
        {
            return new AnomalyType()
            {
                Id = entityDb.Id,
                Label = entityDb.Label,
                ProcessId= entityDb.ProcessId,
                LastModificationDate = entityDb.LastModificationDate,
                Anomalies = entityDb.AnomalyDbs != null ? this.anomalyMapper.Map(entityDb.AnomalyDbs) : null,

            };
        }

        public AnomalyTypeDb Map(AnomalyType entity)
        {
            return new AnomalyTypeDb()
            {
                Id = entity.Id,
                Label = entity.Label,
                ProcessId=entity.ProcessId,
            };
        }

        public IEnumerable<AnomalyTypeDb> Map(IEnumerable<AnomalyType> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<AnomalyType> Map(IEnumerable<AnomalyTypeDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}
