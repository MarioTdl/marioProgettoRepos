using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace marioProgetto.Controllers.Resource
{
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }

        public MakeResource()
        {
            Models= new Collection<KeyValuePairResource>();
        }

    }
}