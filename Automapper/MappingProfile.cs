using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using marioProgetto.Controllers.Resource;
using marioProgetto.Models;

namespace marioProgetto.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //mapping domain to api resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Veichle, VehicleResource>()
            .ForMember(vr => vr.Contact,
            opt => opt.MapFrom(v =>
            new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
            .ForMember(v => v.Features, opt => opt.MapFrom(v => v.Features.Select(p => p.FeatureId)));

            //mapping api resource to domain
            CreateMap<VehicleResource, Veichle>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(v => v.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(v => v.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(v => v.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.Ignore())
            .AfterMap((vr, v) =>
            {
                //vr veichle resource   v veichele
                //remove unselected features nel caso si e aggunta una nuova feature non presenta nel modello DB(v)
                var removeFeatures = new List<VeichleFeature>();
                foreach (var f in v.Features)
                {
                    // se la feature contenuta in vr non e presente nel vr viene rimossa
                    if (!vr.Features.Contains(f.FeatureId))
                        removeFeatures.Add(f);
                }
                foreach (var f in removeFeatures)
                {
                    v.Features.Remove(f);
                }

                //add new feature
                foreach (var id in vr.Features)
                {
                    //se non sono presenti
                    if (!v.Features.Any(f => f.FeatureId == id))
                        v.Features.Add(new VeichleFeature { FeatureId = id });
                }
            });
        }
    }
}