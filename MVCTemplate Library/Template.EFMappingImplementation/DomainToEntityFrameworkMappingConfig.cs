using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Template.Authentication.Model;
using Template.Mapping;
using Template.MappingContract;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Membership;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Permission;

namespace Template.EFMappingImplementation
{
    public class DomainToEntityFrameworkMappingConfig : InjectedMappingConfiguration
    {
        private static bool _mapped;
        protected override bool ConcreteStaticMappedAccessor { get { return _mapped; } set { _mapped = value; } }

        protected override void InitMappings()
        {
            Mapper.CreateMap<User, EFMembership>()
                //.ForMember(dest=>dest.UserId, expression => expression.MapFrom(user => user.UserID))
                .ReverseMap();
            Mapper.CreateMap<Roll, EFRoll>()
                //.ForMember(dest=>dest.RollID, expression => expression.MapFrom(src=>src.RollID))
                .ReverseMap();
            Mapper.CreateMap<Permission, EFPermission>()
                //.ForMember(dest=>dest.PermissionID, expression => expression.MapFrom(permission => permission.PermissionID))
                .ReverseMap();
        }
    }
}
