using appservicequotes.DataAccess.Entity;
using appservicequotes.DataTransferObject.Object;
using AutoMapper;

namespace appservicequotes
{
    public static class InitAutoMapper
    {
        public static IMapper mapper = null;
        public static bool autoMapperInit = false;

        public static void Star()
        {
            if (!autoMapperInit)
            {
                MapperConfiguration configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<User, DtoUser>().MaxDepth(7);
                        cfg.CreateMap<Professional, DtoProfessional>().MaxDepth(7);
                        cfg.CreateMap<ProjectType, DtoProjectType>().MaxDepth(7);
                        cfg.CreateMap<ProjectTypeMechanism, DtoProjectTypeMechanism>().MaxDepth(7);
                        cfg.CreateMap<AssignProject, DtoAssignProject>().MaxDepth(7);

                        cfg.CreateMap<DtoUser, User>().MaxDepth(7);
                        cfg.CreateMap<DtoProfessional, Professional>().MaxDepth(7);
                        cfg.CreateMap<DtoProjectType, ProjectType>().MaxDepth(7);
                        cfg.CreateMap<DtoProjectTypeMechanism, ProjectTypeMechanism>().MaxDepth(7);
                        cfg.CreateMap<DtoAssignProject, AssignProject>().MaxDepth(7);
                    });

                mapper = configuration.CreateMapper();
                autoMapperInit = true;
            }
        }
    }
}
