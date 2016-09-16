﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfa.Das.ApprenticeshipInfoService.Core.Models
{
    public class TrainingLocation
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public Address Address { get; set; }

        public object Location { get; set; }

        public object LocationPoint { get; set; }
    }
}
