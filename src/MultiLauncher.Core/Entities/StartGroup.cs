﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLauncher.Core
{
    public class StartGroup
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<StartItem> StartItems { get; set; }
    }
}
