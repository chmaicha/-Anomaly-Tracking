namespace AnomalyTracking.Repository
{
    /// <summary>
    /// Loads address info.
    /// </summary>
    public partial class ClientAddressDb
    {
        public static ClientAddressDb Map(ClientAddressDb addressDb, ClientAddressDb address)
        {
            if (address == null)
            {
                return null;
            }

            // Ensures that the addressDb is valid in database side and instanciate it only if there is a defined address
            if (addressDb == null && address != null)
            {
                addressDb = new ClientAddressDb();
            }

            //  Updates address only if there is a defined one
            if (address != null)
            {
                // /!\ DO NOT UPDATE THE DATABASE ADDRESS ID IF EXISTS /!\
                addressDb.StreetNumber = address.StreetNumber;
                addressDb.StreetName = address.StreetName;
                addressDb.City = address.City;
                addressDb.ZipCode = address.ZipCode;
                addressDb.CountryCode = address.CountryCode;
                addressDb.FurtherInformation = address.FurtherInformation;
            }

            return addressDb;
        }
    }
}
