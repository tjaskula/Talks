using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Alt.Net.Step0
{
    public class PartnerServiceGateway : IPartnerServiceGateway, IPartner
    {
        readonly PartnerServiceData _data;
        Uri _partnerUri;
        IEnumerable<string> _cat;
        Type _serviceType;

        internal PartnerServiceGateway( PartnerServiceData data )
        {
            Debug.Assert( data != null );
            _data = data;
        }

        public IPartner Partner 
        { 
            get { return this; } 
        }

        public IEnumerable<string> Categories
        {
            get { return _cat ?? (_cat = _data.CommaSepratedCategories.Split(',')); }
        }

        public int PerUseCost 
        {
            get { return _data.PerUseCost; }
        }

        public Type ServiceType 
        {
            get { return _serviceType ?? (_serviceType = Type.GetType( _data.AssemblyQualifiedServiceTypeName )); } 
        }


        public object ObtainService()
        {
            Type t = Type.GetType( _data.AssemblyQualifiedImplementation );
            return Activator.CreateInstance( t );
        }

        #region IPartner Members - Autoimplementation

        string IPartner.UniqueName
        {
            get { return _data.PartnerUniqueName; }
        }

        Uri IPartner.MainUri
        {
            get { return _partnerUri ?? (_partnerUri = new Uri( _data.PartnerMainUri )); }
        }

        #endregion
    }

}
