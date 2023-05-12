using ApiNoSqlDomain.Client;
using ApiNoSqlInfraestructure.Entitys;
using AutoMapper;

namespace ApiNoSqlInfraestructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientModels, ClientModelsMDB>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
            .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person != null ? new PersonModelsMDB
            {
                Name = src.Person.Name,
                Lastname = src.Person.Lastname,
                Dni = src.Person.Dni,
                Birthdate = src.Person.Birthdate ?? new DateTime(),
            } : null));


            CreateMap<ClientModelsMDB, ClientModels>()
             .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
             .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
             .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
             .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person != null ? new PersonModels
             {
                 Name = src.Person.Name,
                 Lastname = src.Person.Lastname,
                 Dni = src.Person.Dni,
                 Birthdate = src.Person.Birthdate ?? new DateTime()
             } : null));

            CreateMap<ClientModels, ClientModelsSQL>()
             .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
             .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
             .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
             .ForMember(dest => dest.Person, opt => opt.MapFrom(src => new PersonModelsSQL
             {
                 Name = src.Person != null ? src.Person.Name : null,
                 Lastname = src.Person != null ? src.Person.Lastname : null,
                 Dni = src.Person != null ? src.Person.Dni : null,
                 Birthdate = src.Person != null ? src.Person.Birthdate ?? new DateTime() : new DateTime()
             }));


            CreateMap<ClientModelsSQL, ClientModels>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person != null ? new PersonModels
                {
                    Name = src.Person.Name,
                    Lastname = src.Person.Lastname,
                    Dni = src.Person.Dni,
                    ClientId = src.ClientId,
                    Birthdate = src.Person.Birthdate ?? new DateTime()
                } : null));

        }
    }
}
