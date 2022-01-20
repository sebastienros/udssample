using System.Net.Sockets;
using Yarp.ReverseProxy.Forwarder;

namespace FrontEnd
{
    public class UdsHttpClientFactory : ForwarderHttpClientFactory
    {
        protected override void ConfigureHandler(ForwarderHttpClientContext context, SocketsHttpHandler handler)
        {
            base.ConfigureHandler(context, handler);

            if (context.NewMetadata != null && context.NewMetadata.TryGetValue("UnixDomainSocket", out var address))
            {
                // Register UDS
                handler.ConnectCallback = async (context, token) =>
                {
                    var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
                    var endpoint = new UnixDomainSocketEndPoint(address);
                    await socket.ConnectAsync(endpoint);
                    return new NetworkStream(socket, ownsSocket: true);
                };
            }
        }
    }
}
