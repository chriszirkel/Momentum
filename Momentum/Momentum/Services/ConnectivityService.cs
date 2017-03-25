using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Services
{
    public class ConnectivityService
    {
        public static async Task<bool> IsConnected()
        {
            return await CrossConnectivity.Current.IsRemoteReachable(Constants.Host, 80, 5000);
        }
    }
}
