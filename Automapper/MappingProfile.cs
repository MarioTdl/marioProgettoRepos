using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using marioProgetto.Controllers.Resource;
using marioProgetto.Models;
using marioProgetto.Persistence;
using marioProgettoRepos.Controllers.Resource;
using marioProgettoRepos.Core.Models;

namespace marioProgetto.Automapper
{
    public class MappingProfile : Profile
    {
        private readonly MarioProgettoDbContext _context;
        public MappingProfile(MarioProgettoDbContext db) { _context = db; }
        public MappingProfile()
        {
            //mapping domain to api resource
            CreateMap<Photo, PhotoResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Veichle, SaveVehicleResource>()
            .ForMember(vr => vr.Contact,
            opt => opt.MapFrom(v =>
            new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
            .ForMember(v => v.Features, opt => opt.MapFrom(v => v.Features.Select(p => p.FeatureId)));
            CreateMap<Veichle, VeichleResource>()
            .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
            .ForMember(vr => vr.Contact,
            opt => opt.MapFrom(v =>
            new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
            .ForMember(v => v.Features, opt => opt.MapFrom(v => v.Features.Select(p => new KeyValuePairResource { Id = p.Feature.Id, Name = p.Feature.Name })));


            //mapping api resource to domain
            CreateMap<VeichleQueryResource, VeichleQuery>();
            CreateMap<SaveVehicleResource, Veichle>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(v => v.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(v => v.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(v => v.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.Ignore())
            .AfterMap((vr, v) =>
            {
                //vr veichle resource   v veichele
                //remove unselected features nel caso si e aggunta una nuova feature non presenta nel modello DB(v)

                var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                // se la feature contenuta in vr non e presente nel vr viene rimossa
                foreach (var f in removedFeatures)
                {
                    v.Features.Remove(f);
                }

                //add new feature se non e presente in feature
                var addFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id))
                .Select(id => new VeichleFeature { FeatureId = id });

                foreach (var f in addFeatures)
                {
                    v.Features.Add(f);
                }
            });
        }
    }
}