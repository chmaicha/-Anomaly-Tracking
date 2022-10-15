using System;

namespace Shared.Core.Repository.Core
{
    /// <summary>
    /// Base class of all entity media.
    /// </summary>
    public abstract class EntityMediaBaseDb
    {
        /// <summary>
        /// Entity's media UID.
        /// </summary>
        public virtual string EntityMediaUID { get; set; }

        /// <summary>
        /// Entity's UID.
        /// </summary>
        public virtual string UID { get; }

        /// <summary>
        /// Entity's last modification date.
        /// </summary>
        public virtual DateTime LastModificationDate { get; set; }
    }
}
