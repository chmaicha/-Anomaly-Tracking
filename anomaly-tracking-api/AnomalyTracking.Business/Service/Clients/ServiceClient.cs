using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.Service.Clients
{
    /// <summary>
    /// Class that manages client.
    /// </summary>
    public class ServiceClient : ServiceBase<ClientDb, IAnomalyTrackingUnitOfWork>, IServiceClient
    {
        public ServiceClient(IAnomalyTrackingUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ClientDb Create(ClientDb clientDb)
        {
            clientDb.LastModificationDate = DateTime.Now;
            clientDb = this.unitOfWork.ClientRepo.Add(clientDb, true);
            return this.Get(clientDb.Id);
        }

        public ClientDb Update(ClientDb client, bool commit = true)
        {
            ClientDb clientDb = this.Get(client.Id);

            clientDb.Label = client.Label;
            clientDb.PhoneNumber = client.PhoneNumber;
            clientDb.Email = client.Email;
            clientDb.LastModificationDate = DateTime.Now;
            clientDb.ClientAddressDb = ClientAddressDb.Map(clientDb.ClientAddressDb, client.ClientAddressDb);

            this.unitOfWork.ClientRepo.Update(clientDb, true);

            return this.Get(clientDb.Id);
        }

        public int Delete(int id)
        {
            ClientDb clientDb = this.unitOfWork.ClientRepo.GetById(id);
            this.unitOfWork.ClientRepo.Update(clientDb, true);
            this.unitOfWork.ClientRepo.Delete(id, true);
            return id;
        }

        public ClientDb Get(int id, string includeProperties = "")
        {
            return this.GetAll(c => c.Id == id, includeProperties).Single();
        }

        public IEnumerable<ClientDb> GetAll(Expression<Func<ClientDb, bool>> filter = null, string includeProperties = "", int page = 1, bool paginate = true)
        {
            this.filter.IncludeProperties = string.IsNullOrWhiteSpace(includeProperties) ? "ClientAddressDb, MoldDbs" : includeProperties;
            this.filter.EntityFilter = filter;
            this.filter.Page = page;
            this.filter.Paginate = paginate;
            this.filter.OrderBy = p => p.OrderByDescending(e => e.LastModificationDate);

            return this.unitOfWork.ClientRepo.GetAll(this.filter).ToList();
        }
    }
}
