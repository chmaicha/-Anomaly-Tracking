using AnomalyTracking.Business.ServiceApp.Faces;
using AnomalyTracking.Model.Faces;
using AnomalyTracking.WebServices.Containers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.WebServices.Common;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Unity;

namespace AnomalyTracking.WebServices.API.Faces
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceFaceWeb : ServiceBaseWeb, IServiceFaceWeb
    {
        private readonly IServiceFaceApp serviceFaceApp;

        public ServiceFaceWeb()
        {
            this.serviceFaceApp = IocContainer.Instance.UnityContainer.Resolve<IServiceFaceApp>();
        }

        public Response<Face> Create(Face face)
        {
            return this.serviceFaceApp.Create(face);
        }

        public Response<Face> Update(string id, Face face)
        {
            int.TryParse(id, out int faceId);

            return this.serviceFaceApp.Update(faceId, face);
        }

        public Response<Face> Get(string serviceFaceId)
        {
            int.TryParse(serviceFaceId, out int id);

            return this.serviceFaceApp.Get(id);
        }

        public Response<int> Delete(string faceId)
        {
            int.TryParse(faceId, out int id);

            return this.serviceFaceApp.Delete(id);
        }

        public Response<IEnumerable<int>> DeleteALL(string[] facesIds)
        {
            return this.serviceFaceApp.DeleteAll(facesIds);
        }

        public Response<IEnumerable<Face>> GetAll(SearchFilterBase filter)
        {
            return this.serviceFaceApp.GetAll(filter);
        }
    }
}
