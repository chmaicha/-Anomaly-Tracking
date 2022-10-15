using Shared.Core.Repository.Base;
using Shared.Core.Repository.UnitOfWork;
namespace AnomalyTracking.Repository.UnitOfWork
{
    public class AnomalyTrackingUnitOfWork : BaseUnitOfWork, IAnomalyTrackingUnitOfWork
    {
        private IBaseRepository<ClientDb> clientRepo;
        private IBaseRepository<ProductDb> productRepo;
        private IBaseRepository<AnomalyDeclarationDb> anomalyDeclarationRepo;
        private IBaseRepository<UserDb> userRepo;
        private IBaseRepository<ProcessDb> processRepo;
        private IBaseRepository<MoldDb> moldRepo;
        private IBaseRepository<FaceDb> faceRepo;
        private IBaseRepository<AnomalyTypeDb> anomalyTypeRepo;
        private IBaseRepository<CavityDb> cavityRepo;
        private IBaseRepository<AnomalyDb> anomalyRepo;

        /// <summary>
        /// Creates a new instance of <code>AnomalyTrackingUnitOfWork</code>.
        /// </summary>
        /// <param name="context"></param>
        public AnomalyTrackingUnitOfWork(IAnomalyTrackingDbContext context)
             : base(context)
        {
        }

        public IBaseRepository<ClientDb> ClientRepo
        {
            get
            {
                if (this.clientRepo == null)
                {
                    this.clientRepo = new BaseRepository<ClientDb>(this.context);
                }
                return this.clientRepo;
            }
        }

        public IBaseRepository<ProductDb> ProductRepo
        {
            get
            {
                if (this.productRepo == null)
                {
                    this.productRepo = new BaseRepository<ProductDb>(this.context);
                }
                return this.productRepo;
            }
        }


     

        public IBaseRepository<AnomalyDeclarationDb> AnomalyDeclarationRepo
        {
            get
            {
                if (this.anomalyDeclarationRepo == null)
                {
                    this.anomalyDeclarationRepo = new BaseRepository<AnomalyDeclarationDb>(this.context);
                }
                return this.anomalyDeclarationRepo;
            }
        }
        public IBaseRepository<UserDb> UserRepo
        {
            get
            {
                if (this.userRepo == null)
                {
                    this.userRepo = new BaseRepository<UserDb>(this.context);
                }
                return this.userRepo;
            }
        }
        public IBaseRepository<ProcessDb> ProcessRepo
        {
            get
            {
                if (this.processRepo == null)
                {
                    this.processRepo = new BaseRepository<ProcessDb>(this.context);
                }
                return this.processRepo;
            }
        }

        public IBaseRepository<MoldDb> MoldRepo
        {
            get
            {
                if (this.moldRepo == null)
                {
                    this.moldRepo = new BaseRepository<MoldDb>(this.context);
                }
                return this.moldRepo;
            }
        }
        public IBaseRepository<FaceDb> FaceRepo
        {
            get
            {
                if (this.faceRepo == null)
                {
                    this.faceRepo = new BaseRepository<FaceDb>(this.context);
                }
                return this.faceRepo;
            }
        }
        public IBaseRepository<AnomalyTypeDb> AnomalyTypeRepo
        {
            get
            {
                if (this.anomalyTypeRepo == null)
                {
                    this.anomalyTypeRepo = new BaseRepository<AnomalyTypeDb>(this.context);
                }
                return this.anomalyTypeRepo;
            }
        }

        public IBaseRepository<CavityDb> CavityRepo
        {
            get
            {
                if (this.cavityRepo == null)
                {
                    this.cavityRepo = new BaseRepository<CavityDb>(this.context);
                }
                return this.cavityRepo;
            }
        }

        public IBaseRepository<AnomalyDb> AnomalyRepo
        {
            get
            {
                if (this.anomalyRepo == null)
                {
                    this.anomalyRepo = new BaseRepository<AnomalyDb>(this.context);
                }
                return this.anomalyRepo;
            }
        }




    }
}