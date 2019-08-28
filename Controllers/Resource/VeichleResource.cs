using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace marioProgetto.Controllers.Resource
{
    public class VeichleResource
    {
        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }
        public  KeyValuePairResource Make {get;set;}
        public bool IsRegistered { get; set; }
        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<KeyValuePairResource> Features { get; set; }
        public VeichleResource ()
        {
            Features= new Collection<KeyValuePairResource>();
        }
    }
}