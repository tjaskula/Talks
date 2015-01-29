using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alt.Net
{
    public interface IPartner
    {
        string UniqueName { get; }
        
        Uri MainUri { get; }
    }

}
