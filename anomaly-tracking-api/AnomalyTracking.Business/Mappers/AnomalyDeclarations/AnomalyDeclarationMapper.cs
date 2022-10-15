
using AnomalyTracking.Model.AnomalyDeclarations;
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace AnomalyTracking.Business.Mappers.AnomalyDeclarations
{
    public class AnomalyDeclarationMapper : IMapper<AnomalyDeclaration, AnomalyDeclarationDb>, IMapper<AnomalyDeclarationDb, AnomalyDeclaration>
    {

        public AnomalyDeclaration Map(AnomalyDeclarationDb entityDb)
        {

            return new AnomalyDeclaration()
            {
                Id = entityDb.Id,
                UserId = (int)entityDb.UserId,
                ProcessId = entityDb.ProcessId,
                CavityId = entityDb.CavityId,
                AnomalyId = (int)entityDb.AnomalyId,
                LastModificationDate = entityDb.LastModificationDate,
            };
        }

        public AnomalyDeclarationDb Map(AnomalyDeclaration entity)
        {
            return new AnomalyDeclarationDb()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ProcessId = entity.ProcessId,
                CavityId = entity.CavityId,
                AnomalyId = entity.AnomalyId,
                



            };
        }

        public IEnumerable<AnomalyDeclarationDb> Map(IEnumerable<AnomalyDeclaration> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<AnomalyDeclaration> Map(IEnumerable<AnomalyDeclarationDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}
