﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModelsLibrary.ModelsNS.Changed
{
    public class IdentityUserLoginGuid : IdentityUserLogin<Guid>
    {
        public IdentityUserLoginGuid()
            : base()
        {

        }
    }
}