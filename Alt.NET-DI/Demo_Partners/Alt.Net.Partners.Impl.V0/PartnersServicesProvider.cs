using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alt.Net.Step0
{
    public class PartnersServicesProvider : IPartnersServicesProvider
    {
        readonly IPartnerStorage _storage;

        public PartnersServicesProvider( IPartnerStorage storage )
        {
            if( storage == null ) throw new ArgumentNullException( "storage" );
            _storage = storage;
        }

        public IEnumerable<IPartnerServiceGateway> GetPartnerServices()
        {
            return _storage.GetPartnerServicesData().Select( data => new PartnerServiceGateway( data ) );
        }
    }

}
