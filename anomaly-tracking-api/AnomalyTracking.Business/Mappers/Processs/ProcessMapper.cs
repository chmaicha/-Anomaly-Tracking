using AnomalyTracking.Business.Mappers.AnomalyDeclarations;
using AnomalyTracking.Business.Mappers.AnomalyTypes;
using AnomalyTracking.Model.Processs;
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;

using System.Linq;

namespace AnomalyTracking.Business.Mappers.Processs
{
    public class ProcessMapper : IMapper<Process, ProcessDb>, IMapper<ProcessDb, Process>
    {
        
        private readonly AnomalyDeclarationMapper anomalyDeclarationMapper;
        private readonly AnomalyTypeMapper anomalyTypeMapper;
        public ProcessMapper()
        {
            
            this.anomalyDeclarationMapper = new AnomalyDeclarationMapper();
            this.anomalyTypeMapper = new AnomalyTypeMapper();
        }
        public Process Map(ProcessDb entityDb)
        {
            return new Process()
            {
                Id = entityDb.Id,
                Label= entityDb.Label,
                LastModificationDate = entityDb.LastModificationDate,
                AnomalyDeclarations = entityDb.AnomalyDeclarationDbs != null ? this.anomalyDeclarationMapper.Map(entityDb.AnomalyDeclarationDbs) : null,
                AnomalyTypes = entityDb.AnomalyTypeDbs != null ? this.anomalyTypeMapper.Map(entityDb.AnomalyTypeDbs) : null,
            };
        }

        public ProcessDb Map(Process entity)
        {
            return new ProcessDb()
            {
                Id = entity.Id,
                Label = entity.Label,
            };
        }

        public IEnumerable<ProcessDb> Map(IEnumerable<Process> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<Process> Map(IEnumerable<ProcessDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}
