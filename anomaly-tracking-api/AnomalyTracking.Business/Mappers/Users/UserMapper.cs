using AnomalyTracking.Business.Mappers.AnomalyDeclarations;
using AnomalyTracking.Model.Users;
using AnomalyTracking.Repository;
using Shared.Core.Business.Common;
using System.Collections.Generic;
using System.Linq;

namespace AnomalyTracking.Business.Mappers.Users
{
    public class UserMapper : IMapper<User, UserDb>, IMapper<UserDb, User>
    {

        private readonly AnomalyDeclarationMapper anomalyDeclarationMapper;

        public UserMapper()
        {
            this.anomalyDeclarationMapper = new AnomalyDeclarationMapper();
        }

        public User Map(UserDb entityDb)
        {
            return new User()
            {
             
                Id = entityDb.Id,
                FirstName = entityDb.FirstName,
                LastName = entityDb.LastName, 
                LvfUserRole = entityDb.LvfUserRole,
                Code = entityDb.Code,
                Password = entityDb.Password,
                LastModificationDate = entityDb.LastModificationDate,
                AnomalyDeclarations = entityDb.AnomalyDeclarationDbs != null ? this.anomalyDeclarationMapper.Map(entityDb.AnomalyDeclarationDbs) : null,

            };
        }

        public UserDb Map(User entity)
        {
            return new UserDb()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName= entity.LastName,
                Code = entity.Code,
                Password = entity.Password,
                LvfUserRole = entity.LvfUserRole,
            };
        }

        public IEnumerable<UserDb> Map(IEnumerable<User> entities)
        {
            return entities.Select(e => this.Map(e)).ToList();
        }

        public IEnumerable<User> Map(IEnumerable<UserDb> entitiesDb)
        {
            return entitiesDb.Select(e => this.Map(e)).ToList();
        }
    }
}
