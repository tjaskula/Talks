using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Alt.Net
{
    public class PartnerStorageCollection : IPartnerStorage
    {
        readonly List<PartnerServiceData> _data;

        public PartnerStorageCollection()
        {
            _data = new List<PartnerServiceData>(); 
        }

        public IEnumerable<PartnerServiceData> GetPartnerServicesData()
        {
            return _data;
        }

        public void Add( PartnerServiceData data )
        {
            if( data == null ) throw new ArgumentNullException( "data" );
            _data.Add( data );
        }
    }
}
