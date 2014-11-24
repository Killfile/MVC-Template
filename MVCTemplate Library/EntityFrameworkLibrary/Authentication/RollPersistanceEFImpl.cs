using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniversity.DAL;
using Template.Authentication.Model;
using Template.Authentication.Persistance;
using Template.MappingContract;
using Template.Persistance.EntityFrameworkImpl.EntityFrameworkModels.Permission;

namespace Template.Persistance.EntityFrameworkImpl.Authentication
{
    public class RollPersistanceEFImpl : IRollPersistance 
    {
        private readonly TemplateDBContext _context;

        private readonly MappingBaseline _mapper;

        public RollPersistanceEFImpl(MappingBaseline mapper, TemplateDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Insert(Roll roll)
        {
            EFRoll efRoll = _mapper.Map<Roll, EFRoll>(roll);
            _context.Rolls.Add(efRoll);
            _context.SaveChanges();
        }

        public void Update(Roll roll)
        {
            EFRoll efRoll = _mapper.Map<Roll, EFRoll>(roll);
            _context.Rolls.AddOrUpdate(efRoll);
            _context.SaveChanges();
        }

        public void Delete(Guid RoleId)
        {
            EFRoll targetRoll = GetEFRollByID(RoleId);
            _context.Rolls.Remove(targetRoll);
            _context.SaveChanges();
        }

        public List<Roll> GetAll()
        {
            var returnValue = _mapper.Map<List<EFRoll>, List<Roll>>(_context.Rolls.ToList());
            return returnValue;
        }

        public Roll Get(Guid RoleId)
        {
            EFRoll targetRoll = GetEFRollByID(RoleId);
            if (targetRoll == null)
                return null;
            return _mapper.Map<EFRoll, Roll>(targetRoll);
        }

        public List<Roll> GetAll(List<Guid> rollIDs)
        {
            var efRolls = _context.Rolls.Where(r => rollIDs.Contains(r.RollID)).ToList();
            return _mapper.Map<List<EFRoll>, List<Roll>>(efRolls);
        }

        private EFRoll GetEFRollByID(Guid RoleId)
        {
            EFRoll targetRoll = _context.Rolls.Find(RoleId);
            return targetRoll;
        }
    }
}
