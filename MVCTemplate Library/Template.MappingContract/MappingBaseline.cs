using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Template.Mapping;

namespace Template.MappingContract
{
    public class MappingBaseline
    {
        private readonly List<InjectedMappingConfiguration> _configurations;

        public MappingBaseline(List<InjectedMappingConfiguration> configurations)
        {
            _configurations = configurations;
        }

        public TToType Map<TFromType, TToType>(TFromType source)
        {
            return Mapper.Map<TFromType, TToType>(source);
        }
    }
}
