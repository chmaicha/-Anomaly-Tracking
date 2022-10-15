using Shared.Core.Repository.Filters;
using System.Collections.Generic;

namespace Shared.Core.Repository.Base
{
    /// <summary>
    /// Interface that allows to manipulate entity tables via ORM.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity to manipulate</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">Identifier of the entity to get</param>
        /// <returns>Entity instance</returns>
        TEntity GetById(int id);

        /// <summary>
        /// Gets a list of entities based on the proviers constraintes.
        /// </summary>
        /// <param name="filter">Filter to apply</param>
        /// <returns>Entities list</returns>
        IEnumerable<TEntity> GetAll(RepositoryFilter<TEntity> filter);

        /// <summary>
        /// Adds a new entity to database.
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <param name="commit">Indicates whether or not it should commit the operation</param>
        /// <returns>Created entity instance</returns>
        TEntity Add(TEntity entity, bool commit);

        /// <summary>
        /// Updates the provided entity.
        /// </summary>
        /// <param name="entityToUpdate">Entity to update</param>
        /// <param name="commit">Indicates whether or not it should commit the operation</param>
        void Update(TEntity entityToUpdate, bool commit);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">Identifier of entity to delete</param>
        /// <param name="commit">Indicates whether or not it should commit the operation</param>
        /// <returns>Deleted entity instance</returns>
        TEntity Delete(int id, bool commit);

        /// <summary>
        /// Deletes the provided entity instance.
        /// </summary>
        /// <param name="id">Identifiant de l'objet à supprimer</param>
        /// <param name="commit">Indique que la modificaiton doit etre enregistrée immédiatement si oui, plus tard sinon</param>
        /// <returns>Deleted entity instance</returns>
        TEntity Delete(TEntity entityToDelete, bool commit);
    }
}