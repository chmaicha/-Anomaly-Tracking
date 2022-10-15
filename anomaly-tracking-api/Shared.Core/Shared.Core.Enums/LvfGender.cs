using System.Runtime.Serialization;

namespace Shared.Core.Enums
{
    /// <summary>
    /// Defines all supported gender for contact.
    /// </summary>
    public enum LvfGender
    {
        [DataMember]
        FEMALE = 1,

        [DataMember]
        MALE = 2
    }
}
