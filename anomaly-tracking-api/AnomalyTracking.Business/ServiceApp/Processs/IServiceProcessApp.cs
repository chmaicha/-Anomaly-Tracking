using AnomalyTracking.Model.Processs;
using Shared.Core.Client.Common;
using Shared.Core.Model.Filters;
using System.Collections.Generic;

namespace AnomalyTracking.Business.ServiceApp.Processs
{ /// <summary>
  /// Interface that manages process operations.
  /// </summary>
    public interface IServiceProcessApp
    {
        /// <summary>
        /// Create the given process
        /// </summary>
        /// <param name="process">Process to create</param>
        /// <returns>Create a new process</returns>
        Response<Process> Create(Process process);

        /// <summary>
        /// Update the given process
        /// </summary>
        /// <param name="id"></param>
        /// <param name="process"> Process to update</param>
        /// <returns>Modified process info</returns>
        Response<Process> Update(int id, Process process);


        /// <summary>
        /// Get specific process by id
        /// </summary>
        /// <param name="id"> process on retrieved data </param>
        /// <returns> get the existant process</returns>
        Response<Process> Get(int id);


        /// <summary>
        ///  Delete specific process by id
        /// </summary>
        /// <param name="id">identifier of the process to delete</param>
        /// <returns> Deleted process identifier</returns>
        Response<int> Delete(int id);

        /// <summary>
        /// Deletes a list of processs based on identifiers.
        /// </summary>
        /// <param name="entitiesIds"> The list of identifiers of processs to be deleted</param>
        /// <returns> The list of processs identifiers deleted</returns>
        Response<IEnumerable<int>> DeleteAll(string[] entitiesIds);

        /// <summary>
        /// Get all process
        /// </summary>
        /// <returns> get all existant process </returns>
        Response<IEnumerable<Process>> GetAll(SearchFilterBase filter);

    }
}