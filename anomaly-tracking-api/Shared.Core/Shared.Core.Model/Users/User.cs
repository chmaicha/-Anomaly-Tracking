using Shared.Core.Enums;
using Shared.Core.Model.Common;
using Shared.Core.Model.Core;
using System;
using System.Configuration;
using System.Globalization;
using System.Runtime.Serialization;

namespace Shared.Core.Model.Users
{
    /// <summary>
    /// Represents <code>User</code> object.
    /// </summary>
    [DataContract]
    public class User : EntityBase
    {
        /// <summary>
        /// Represents <code>User call name</code>
        /// </summary>
        [DataMember]
        public string CallName { get; set; }

        /// <summary>
        /// Registration Number driver
        /// </summary>
        [DataMember]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Temporary user id (Ex: pour les interimaires et tous les users non salarié).
        /// </summary>
        [DataMember]
        public string TemporaryUserId { get; set; }

        /// <summary>
        /// Represents <code>User</code>'s gender.
        /// </summary>
        [DataMember]
        public LvfGender LvfGender { get; set; }

        /// <summary>
        /// Indicates whether or not the user is an admin.
        /// </summary>
        [DataMember]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Indicates whether the user is the root.
        /// </summary>
        [DataMember]
        public bool IsRoot { get; set; }

        /// <summary>
        /// Indicates whether the user is the root.
        /// </summary>
        [DataMember]
        public bool IsRecipient { get; set; }

        /// <summary>
        /// Indicates whether the user has mobile acces.
        /// </summary>
        [DataMember]
        public bool HasMobileAccess { get; set; }

        /// <summary>
        /// Indicates whether the user has keys acces.
        /// </summary>
        [DataMember]
        public bool HasKeysAccess { get; set; }

        /// <summary>
        /// Represents <code>User birth date</code>
        /// </summary>
        [IgnoreDataMember]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Represents <code>User birth date</code>
        /// </summary>
        [DataMember]
        public string BirthDateString { get; set; }

        /// <summary>
        /// Represents <code>User entry date</code>
        /// </summary>
        [IgnoreDataMember]
        public DateTime? EntryDate { get; set; }

        /// <summary>
        /// Represents <code>User entry date</code>
        /// </summary>
        [DataMember]
        public string EntryDateString { get; set; }

        /// <summary>
        /// Represents <code>User release date</code>
        /// </summary>
        [IgnoreDataMember]
        public DateTime? ResignationDate { get; set; }

        /// <summary>
        /// Represents <code>User release date</code>
        /// </summary>
        [DataMember]
        public string ResignationDateString { get; set; }

        /// <summary>
        /// Represents <code>User nationality</code>
        /// </summary>
        [DataMember]
        public string Nationality { get; set; }

        /// <summary>
        /// User's address
        /// </summary>
        [DataMember]
        public Address Address { get; set; }

        [OnDeserialized]
        private void OnDeserializing(StreamingContext context)
        {
            try
            {
                string dateformat = ConfigurationManager.AppSettings["dateformat"];

                this.EntryDate = Parse(this.EntryDateString, '/');
                this.ResignationDate = Parse(this.ResignationDateString, '/');
                this.BirthDate = Parse(this.BirthDateString, '/');
            }
            catch (Exception)
            {
            }
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            try
            {
                string dateformat = ConfigurationManager.AppSettings["dateformat"];

                this.EntryDateString = this.EntryDate.HasValue ? this.EntryDate.Value.ToString(dateformat, CultureInfo.InvariantCulture) : "";
                this.ResignationDateString = this.ResignationDate.HasValue ? this.ResignationDate.Value.ToString(dateformat, CultureInfo.InvariantCulture) : "";
                this.BirthDateString = this.BirthDate.HasValue ? this.BirthDate.Value.ToString(dateformat, CultureInfo.InvariantCulture) : "";
            }
            catch (Exception)
            {
            }
        }
    }
}
