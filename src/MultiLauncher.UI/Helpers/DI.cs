using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLauncher.UI
{
    public static class DI
    {
        public static bool ProviderBuilt = false;
        public static IServiceProvider Provider { get; private set; }

        public static void BuildServiceProvider(IServiceCollection collection)
        {
            if (ProviderBuilt)
                throw new Exception("Provider is already built");

            Provider = collection.BuildServiceProvider();
            ProviderBuilt = true;
        }
    }
}
