using System.Net;
using System.Net.Sockets;
using Yarp.ReverseProxy.Forwarder;

namespace FrontEnd
{
    public class UdsHttpClientFactory : IForwarderHttpClientFactory
    {
        public HttpMessageInvoker CreateClient(ForwarderHttpClientContext context)
        {

            var handler = new SocketsHttpHandler
            {
                // Preserve default factory settings
                UseProxy = false,
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.None,
                UseCookies = false,
            };

            if (context.ClusterId == "cluster2")
            {
                // Register UDS
                handler.ConnectCallback = async (context, token) =>
                {
                    var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
                    var endpoint = new UnixDomainSocketEndPoint(Path.Combine(Path.GetTempPath(), "app2.sock"));
                    await socket.ConnectAsync(endpoint);
                    return new NetworkStream(socket, ownsSocket: true);
                };
            }
            return new HttpMessageInvoker(handler, disposeHandler: true);
        }
    }
}
