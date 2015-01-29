using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alt.Net
{
    public interface IPartnersServicesProvider
    {
        IEnumerable<IPartnerServiceGateway> GetPartnerServices();
    }

    public interface IPartnerServiceGateway
    {
        IPartner Partner { get; }
        IEnumerable<string> Categories { get; }
        int PerUseCost { get; }
        Type ServiceType { get; }
        object ObtainService();
    }

}
