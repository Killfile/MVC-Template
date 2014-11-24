using AutoMapper;
using Template.Authentication.Model;
using Template.MappingContract;

namespace Template.Mapping
{
    public class EFMappingCreator : IMappingContract
    {
        private static bool _mapped;

        public TToType Map<TFromType, TToType>(TFromType source)
        {
            
        }

        public void InitMappings()
        {
            if(_mapped)
                return;
            
            Mapper.CreateMap<User, EFMembership>().ReverseMap();
            _mapped = true;
        }

    }
}
