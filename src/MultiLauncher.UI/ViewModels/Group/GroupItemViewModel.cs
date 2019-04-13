using MultiLauncher.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLauncher.UI
{
    public class GroupItemViewModel
    {
        public string GroupName { get; set; }

        public ObservableCollection<StartItem> StartItems { get; set; }
    }
}
