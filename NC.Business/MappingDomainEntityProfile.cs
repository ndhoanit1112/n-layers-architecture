using AutoMapper;

namespace NC.Business
{
    public class MappingDomainEntityProfile : Profile
    {
        public MappingDomainEntityProfile()
        {
            //Mapping Domain model to Entity


            //Mapping Entity to Domain (Use restrictions, priority to use LinQ Select to optimize the DB query)

        }
    }
}
