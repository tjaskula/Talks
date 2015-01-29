using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alt.Net
{
    public interface IPartnerStorage
    {
        IEnumerable<PartnerServiceData> GetPartnerServicesData();
    }

    public class PartnerServiceData
    {
        public string PartnerUniqueName { get; set; }
        public string PartnerMainUri { get; set; }
        public string CommaSepratedCategories { get; set; }
        public int PerUseCost { get; set; }
        public string AssemblyQualifiedServiceTypeName { get; set; }
        public string AssemblyQualifiedImplementation { get; set; }
    }

}
