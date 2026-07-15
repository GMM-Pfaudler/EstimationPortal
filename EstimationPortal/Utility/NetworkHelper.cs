using System.Web;

namespace EstimationPortal.Utility
{
    public static class NetworkHelper
    {
        public static string GetClientIpAddress(HttpRequestBase request)
        {
            // Check if the request is forwarded by a proxy
            string ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ip))
            {
                // The HTTP_X_FORWARDED_FOR variable can contain multiple IP addresses
                // The first one is the client's IP address
                string[] addresses = ip.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            ip = request.ServerVariables["HTTP_X_REAL_IP"];
            if (!string.IsNullOrEmpty(ip))
            {
                return ip;
            }

            ip = request.ServerVariables["CF-Connecting-IP"];
            if (!string.IsNullOrEmpty(ip))
            {
                return ip;
            }

            return request.ServerVariables["REMOTE_ADDR"];
        }
    }
}