using Shared.Core.Repository.Base;
using Shared.Core.Repository.UnitOfWork;

namespace AnomalyTracking.Repository.UnitOfWork
{
    /// <summary>
    /// AnomalyTracking applicaton's unit of work.
    /// </summary>
    public interface IAnomalyTrackingUnitOfWork : IBaseUnitOfWork
    {
        IBaseRepository<ClientDb> ClientRepo { get; }
        IBaseRepository<ProductDb> ProductRepo { get; }
        IBaseRepository<AnomalyDeclarationDb> AnomalyDeclarationRepo { get; }
        IBaseRepository<UserDb> UserRepo { get; }
        IBaseRepository<ProcessDb> ProcessRepo { get; }
        IBaseRepository<MoldDb> MoldRepo { get; }
        IBaseRepository<FaceDb> FaceRepo { get; }
        IBaseRepository<AnomalyTypeDb> AnomalyTypeRepo { get; }
        IBaseRepository<CavityDb> CavityRepo { get; }
        IBaseRepository<AnomalyDb> AnomalyRepo { get; }




    }
}
