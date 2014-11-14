using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Template.Authentication.Model;


namespace Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Membership
{
    public class EFMembership
    {
        static EFMembership()
        {
            Mapper.CreateMap<User, EFMembership>();
            Mapper.CreateMap<EFMembership, User>();
        }

        [Key]
        public Guid UserId { get; set; }
        public String UserName { get; set; }
        public String HashedPassword { get; set; }
        public String Salt { get; set; }
    }
}
