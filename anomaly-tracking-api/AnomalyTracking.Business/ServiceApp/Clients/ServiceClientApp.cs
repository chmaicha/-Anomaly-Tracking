using AnomalyTracking.Business.Mappers.Clients;
using AnomalyTracking.Business.Service.Clients;
using AnomalyTracking.Model.Clients;
using AnomalyTracking.Repository;
using AnomalyTracking.Repository.UnitOfWork;
using Shared.Core.Business.Helpers;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using Shared.Core.Repository.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AnomalyTracking.Business.ServiceApp.Clients
{
    public class ServiceClientApp : IServiceClientApp
    {
        private readonly ClientMapper clientMapper;
        private readonly IServiceClient serviceClient;
        private readonly IAnomalyTrackingUnitOfWork unitOfWork;

        public ServiceClientApp(IServiceClient serviceClient, IAnomalyTrackingUnitOfWork unitOfWork)
        {
            this.clientMapper = new ClientMapper();
            this.unitOfWork = unitOfWork;
            this.serviceClient = serviceClient;
            this.serviceClient.SetContext(this.unitOfWork);
        }

        public Response<Client> Create(Client client)
        {
            try
            {
                CheckClientInfo(client);

                ClientDb clientDb = this.clientMapper.Map(client);

                clientDb = this.serviceClient.Create(clientDb);

                client = this.clientMapper.Map(clientDb);

                return new Response<Client>(client, "app.shared.succeededcreate");
            }
            catch (ArgumentException aex)
            {
                return new Response<Client>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Client>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Client> Update(int id, Client client)
        {
            try
            {
                CheckClientInfo(client, id);

                ClientDb clientDb = this.clientMapper.Map(client);

                clientDb = this.serviceClient.Update(clientDb);

                client = this.clientMapper.Map(clientDb);

                return new Response<Client>(client, "app.shared.succeededupdate");
            }
            catch (ArgumentException aex)
            {

                return new Response<Client>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<Client>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<Client> Get(int id)
        {
            try
            {
                ClientDb clientDb = this.serviceClient.Get(id);

                Client client = this.clientMapper.Map(clientDb);

                return new Response<Client>(client, "app.shared.succeededget");
            }
            catch (ArgumentException aex)
            {
                return new Response<Client>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<Client>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<Client>> GetAll(SearchFilterBase filter)
        {
            try
            {
                IEnumerable<ClientDb> clientDbs = this.serviceClient.GetAll(this.ApplyFilter(filter), "", filter.Page, filter.Paginate).ToList();

                IEnumerable<Client> clients = this.clientMapper.Map(clientDbs);

                return new Response<IEnumerable<Client>>(clients, "app.shared.succeededget", this.serviceClient.GetTotalCount());
            }
            catch (ArgumentException aex)
            {
                return new Response<IEnumerable<Client>>(aex);
            }
            catch (Exception ex)
            {
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<Client>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<int> Delete(int clientId)
        {
            try
            {
                this.unitOfWork.BeginTransaction();
                List<string> paths = new List<string>();

                this.unitOfWork.CommitTransaction();

                return new Response<int>(clientId, "app.shared.succeededdelete");
            }
            catch (ArgumentException aex)
            {
                this.unitOfWork.RollbackTransaction();
                return new Response<int>(aex);
            }
            catch (Exception ex)
            {
                this.unitOfWork.RollbackTransaction();
                ConcurrencyException cex = ExceptionHelper.HandleException(ex);
                return new Response<int>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        public Response<IEnumerable<int>> DeleteAll(string[] entitiesIds)
        {
            try
            {
                this.unitOfWork.BeginTransaction();

                List<int> clientsIds = entitiesIds.Select(id => int.Parse(id)).ToList();
                List<string> paths = new List<string>();

                foreach (int clientId in clientsIds)
                {
                    this.DeleteClientById(clientId, paths);
                }

                this.unitOfWork.CommitTransaction();

                return new Response<IEnumerable<int>>(clientsIds, "app.shared.succeededdelete");
            }
            catch (ArgumentException aex)
            {
                this.unitOfWork.RollbackTransaction();

                return new Response<IEnumerable<int>>(aex);
            }
            catch (Exception ex)
            {
                this.unitOfWork.RollbackTransaction();

                ConcurrencyException cex = ExceptionHelper.HandleException(ex);

                return new Response<IEnumerable<int>>(cex, cex.MessageKey, cex.ErrorneousEntity);
            }
        }

        private Expression<Func<ClientDb, bool>> ApplyFilter(SearchFilterBase filter)
        {
            if (filter == null)
            {
                throw new ArgumentException("app.error.invalidfilter");
            }

            bool hasSearchInput = !string.IsNullOrEmpty(filter.SearchInput);

            return client =>
                 (!hasSearchInput ||
                    client.Label.Contains(filter.SearchInput) ||
                    client.Email.Contains(filter.SearchInput) ||
                    client.PhoneNumber.Contains(filter.SearchInput)
                );
        }

        private void DeleteClientById(int clientId, List<string> paths)
        {
            Client client = this.clientMapper.Map(this.serviceClient.Get(clientId));

            ClientDb clientDb = this.serviceClient.GetAll(c => c.Id == clientId).Single();

            this.serviceClient.Delete(clientId);
        }

        private static void CheckClientInfo(Client client, int clientId = 0)
        {
            ManagementRuleHelper.CheckRequestParameters(client, clientId);
        }
    }
}
