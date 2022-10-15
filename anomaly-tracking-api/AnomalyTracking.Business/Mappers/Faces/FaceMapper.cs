
using AnomalyTracking.Model.Faces;
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;
using System.Linq;

namespace AnomalyTracking.Business.Mappers.Faces
{
    public class FaceMapper : IMapper<Face, FaceDb>, IMapper<FaceDb, Face>
    {
       
        public Face Map(FaceDb entityDb)
        {
            return new Face()
            {
                Id = entityDb.Id,
                Label = entityDb.Label,
                LastModificationDate = entityDb.LastModificationDate,
                Position = string.IsNullOrWhiteSpace(entityDb.Position) ? null : entityDb.Position.Split('|'),

            };
        }

        public FaceDb Map(Face entity)
        {
            return new FaceDb()
            {
                Id = entity.Id,
                Label = entity.Label,
                Position = entity.Position?.Count() > 0 ? string.Join("|", entity.Position) : ""


            };
        }

        public IEnumerable<FaceDb> Map(IEnumerable<Face> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<Face> Map(IEnumerable<FaceDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}
