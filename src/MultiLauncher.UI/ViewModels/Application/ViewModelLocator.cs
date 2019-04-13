using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLauncher.UI
{
    public class ViewModelLocator
    {
        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        public ApplicationViewModel ApplicationViewModel => DI.Provider.GetService<ApplicationViewModel>();
    }
}
