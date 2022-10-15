namespace Shared.Core.Repository.Core
{
    /// <summary>
    /// Base class of all entity that has a media to proceed.
    /// </summary>
    public partial class EntityBaseDb
    {
        /// <summary>
        /// Entity's identifier.
        /// </summary>
        public virtual int EntityId { get; }

        /// <summary>
        /// Entity's media instance.
        /// </summary>
        public virtual EntityMediaBaseDb EntityMediaDb { get; set; }

        /// <summary>
        /// Entity's media UID.
        /// </summary>
        public virtual string EntityMediaUID { get; set; }
    }
}
