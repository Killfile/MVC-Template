using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Template.Website.ViewModels.Membership.Rolls
{
    public class LinkedRollViewModel
    {
        public Guid RollID { get; set; }
        public String Label { get; set; }
        public bool Assigned { get; set; }
    }
}