
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;
using AnomalyTracking.Model.Anomalies;


using System.Linq;
using AnomalyTracking.Business.Mappers.Faces;

namespace AnomalyTracking.Business.Mappers.Anomalies
{
    public class AnomalyMapper : IMapper < Anomaly, AnomalyDb >, IMapper<AnomalyDb, Anomaly>
    {
        private readonly FaceMapper faceMapper;
        public AnomalyMapper()
        {
            this.faceMapper = new FaceMapper();
        }

        public Anomaly Map(AnomalyDb entityDb)
        {
            return new Anomaly()
            {
                Id = entityDb.Id,
                AnomalyTypeId = entityDb.AnomalyTypeId,
                Faces = entityDb.FaceDbs != null ? this.faceMapper.Map(entityDb.FaceDbs) : null,


            };
        }

        public AnomalyDb Map(Anomaly entity)
        {
            return new AnomalyDb()
            {
                Id = entity.Id,
                AnomalyTypeId = entity.AnomalyTypeId,

            };
        }

        public IEnumerable<AnomalyDb> Map(IEnumerable<Anomaly> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<Anomaly> Map(IEnumerable<AnomalyDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}
