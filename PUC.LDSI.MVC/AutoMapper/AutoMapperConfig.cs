using AutoMapper;

namespace PUC.LDSI.MVC.AutoMapper
{
    public abstract class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMappingProfile>();
            });
        }
    }
}
