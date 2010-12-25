using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dnughh.SilverlightErfahrungen.Contract
{
    public interface ISilverlightService
    {
#if NETCLR
        void OnlyNetClr();
#endif

#if SILVERLIGHT
        void  OnlySilverlight();
#endif

    }
}
