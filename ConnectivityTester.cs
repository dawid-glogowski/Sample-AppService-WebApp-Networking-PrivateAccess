using System.Net;

public class ConnectivityTester
{
    public async Task<object> TestConnectivity(string target, HttpClient client)
    {
        var result = new Dictionary<string, object>();

        try
        {
            var dns = await Dns.GetHostAddressesAsync(new Uri(target).Host);
            result["dns"] = dns.Select(ip => ip.ToString()).ToArray();
        }
        catch (Exception ex)
        {
            result["dns_error"] = ex.Message;
        }

        try
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var response = await client.GetAsync(target);
            sw.Stop();

            result["http_status"] = (int)response.StatusCode;
            result["http_time_ms"] = sw.ElapsedMilliseconds;
        }
        catch (Exception ex)
        {
            result["http_error"] = ex.Message;
        }

        result["server_hostname"] = Environment.MachineName;
        result["server_time"] = DateTime.UtcNow;

        return result;
    }
}
