namespace Shared.Core.Business.Common
{
    /// <summary>
    /// Interface for mapping TEntity object to KEntity.
    /// </summary>
    /// <typeparam name="TEntity">Type of the first entity</typeparam>
    /// <typeparam name="KEntity">Type of the seoond entity</typeparam>
    public interface IMapper<TEntity, KEntity>
    {
        /// <summary>
        /// Converts an object type <code>TEntity</code> to <code>KEntity</code>.
        /// </summary>
        /// <param name="entity">Input object type</param>
        /// <returns>Output object type</returns>
        KEntity Map(TEntity entity);
    }
}