using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Template.Authentication.Model;
using Template.Mapping;
using Template.MappingContract;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Membership;
using Template.Website.ViewModels.Account;
using Template.Website.ViewModels.Membership.Permissions;
using Template.Website.ViewModels.Membership.Rolls;

namespace Template.Website.ViewModels.Mapping
{
    public class DomainToViewModelMappingConfig : InjectedMappingConfiguration
    {
        private static bool _mapped;
        protected override bool ConcreteStaticMappedAccessor { get { return _mapped; } set { _mapped = value; } }

        protected override void InitMappings()
        {
            Mapper.CreateMap<User,LoginViewModel>().ReverseMap();
            Mapper.CreateMap<User, RegistrationViewModel>().ReverseMap();
            Mapper.CreateMap<EditPermissionViewModel, Permission>().ReverseMap();

            Mapper.CreateMap<EditRollViewModel, Roll>().ReverseMap();
            Mapper.CreateMap<CreateRollViewModel, Roll>().ReverseMap();

            Mapper.CreateMap<EditPermissionViewModel, Permission>().ReverseMap();
            Mapper.CreateMap<CreatePermissionViewModel, Permission>().ReverseMap();

        }
    }
}