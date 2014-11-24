namespace Template.Mapping
{
    public abstract class InjectedMappingConfiguration
    {
        /* You can theoretically do this without the static accessor 
         * (which is an easy way of enforcing a singelton like pattern here) but
         * then you have to remember to bind in Singleton scope in Ninject.  Adding
         * the static singleton enforcer makes this more fault tollerant.
         */
        
        protected abstract bool ConcreteStaticMappedAccessor { get; set; }

        protected InjectedMappingConfiguration()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            if(!ConcreteStaticMappedAccessor)
                InitMappings();
            ConcreteStaticMappedAccessor = true;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }
        protected abstract void InitMappings();

    }
}