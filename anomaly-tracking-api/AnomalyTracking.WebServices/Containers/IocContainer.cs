using AnomalyTracking.Business.Service.Clients;
using AnomalyTracking.Business.Service.Products;
using AnomalyTracking.Business.Service.Users;
using AnomalyTracking.Business.ServiceApp.Authentifications;
using AnomalyTracking.Business.Service.Processs;
using AnomalyTracking.Business.ServiceApp.Clients;
using AnomalyTracking.Business.ServiceApp.Products;
using AnomalyTracking.Business.ServiceApp.Users;
using AnomalyTracking.Business.ServiceApp.Processs;
using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Unity;
using AnomalyTracking.Business.ServiceApp.AnomalyTypes;
using AnomalyTracking.Business.Service.AnomalyTypes;
using AnomalyTracking.Business.ServiceApp.Molds;
using AnomalyTracking.Business.Service.Molds;
using AnomalyTracking.Business.ServiceApp.Cavities;
using AnomalyTracking.Business.Service.Cavities;
using AnomalyTracking.Business.ServiceApp.AnomalyDeclarations;
using AnomalyTracking.Business.Service.AnomalyDeclarations;
using AnomalyTracking.Business.ServiceApp.Anomalies;
using AnomalyTracking.Business.Service.Anomalies;
using AnomalyTracking.Business.ServiceApp.Faces;
using AnomalyTracking.Business.Service.Faces;

namespace AnomalyTracking.WebServices.Containers
{
    public class IocContainer
    {
        private static IocContainer instance;
        private readonly IUnityContainer unitycontainer;

        private IocContainer()
        {
            this.unitycontainer = new UnityContainer();
            
            this.unitycontainer.RegisterType<IServiceAuthenticationApp, ServiceAuthenticationApp>();

            this.unitycontainer.RegisterType<IServiceClientApp, ServiceClientApp>();
            this.unitycontainer.RegisterType<IServiceClient, ServiceClient>();

            this.unitycontainer.RegisterType<IServiceCavityApp, ServiceCavityApp>();
            this.unitycontainer.RegisterType<IServiceCavity, ServiceCavity>();

            this.unitycontainer.RegisterType<IServiceProductApp, ServiceProductApp>();
            this.unitycontainer.RegisterType<IServiceProduct, ServiceProduct>();

            this.unitycontainer.RegisterType<IServiceAnomalyTypeApp, ServiceAnomalyTypeApp>();
            this.unitycontainer.RegisterType<IServiceAnomalyType, ServiceAnomalyType>();

            this.unitycontainer.RegisterType<IServiceUserApp, ServiceUserApp>();
            this.unitycontainer.RegisterType<IServiceUser, ServiceUser>();

            this.unitycontainer.RegisterType<IServiceProcessApp, ServiceProcessApp>();
            this.unitycontainer.RegisterType<IServiceProcess, ServiceProcess>();

             this.unitycontainer.RegisterType<IServiceMoldApp, ServiceMoldApp>();
            this.unitycontainer.RegisterType<IServiceMold, ServiceMold>();

            this.unitycontainer.RegisterType<IServiceFaceApp, ServiceFaceApp>();
            this.unitycontainer.RegisterType<IServiceFace, ServiceFace>();

            this.unitycontainer.RegisterType<IServiceAnomalyApp, ServiceAnomalyApp>();
            this.unitycontainer.RegisterType<IServiceAnomaly, ServiceAnomaly>();

            this.unitycontainer.RegisterType<IServiceAnomalyDeclarationApp, ServiceAnomalyDeclarationApp>();
            this.unitycontainer.RegisterType<IServiceAnomalyDeclaration, ServiceAnomalyDeclaration>();

            this.unitycontainer.RegisterType<IAnomalyTrackingUnitOfWork, AnomalyTrackingUnitOfWork>();
            this.unitycontainer.RegisterType<IAnomalyTrackingDbContext, AnomalyTrackingDbEntities>();



        }

        public static IocContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IocContainer();
                }

                return instance;
            }
        }

        public IUnityContainer UnityContainer
        {
            get
            {
                return this.unitycontainer;
            }
        }
    }
}