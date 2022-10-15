using System.Runtime.Serialization;

namespace Shared.Core.Enums
{
    /// <summary>
    /// Enum that contains supported <code>operation</code>'s type.
    /// </summary>
    public enum LvfOperationType
    {
        [DataMember]
        NONE = 0,

        [DataMember]
        CREATE = 1,

        [DataMember]
        READ = 2,

        [DataMember]
        UPDATE = 3,

        [DataMember]
        UPDATE_STATUS = 4,

        [DataMember]
        UPDATE_CONFIG = 5,

        [DataMember]
        DELETE = 6,

        [DataMember]
        READ_ALL = 7,

        [DataMember]
        VALIDATION = 20,

        [DataMember]
        SEARCH = 30,

        [DataMember]
        PRINTING = 40,

        [DataMember]
        UPDLOADING = 50,

        [DataMember]
        DOWNLOAD = 41,

        [DataMember]
        SENDING_BY_EMAIL = 60,

        [DataMember]
        COMMON = 100,

        [DataMember]
        LOG_IN = 110,

        [DataMember]
        LOG_OUT = 111
    }
}
