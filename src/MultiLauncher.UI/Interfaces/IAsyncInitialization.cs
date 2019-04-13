using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLauncher.UI
{
    public interface IAsyncInitialization
    {
        Task Initialization { get; }

        Task Initialize();
    }
}
