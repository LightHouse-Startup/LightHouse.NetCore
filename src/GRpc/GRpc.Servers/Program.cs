using System;
using Grpc.Core;
using GRpc.Base;

namespace GRpc.Servers
{
    class Program
    {
        const int Port = 9007;

        static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { IRpcService.BindService(new RpcService()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();
            
            Console.WriteLine("GRpc server listening on port " + Port);
            Console.WriteLine("任意键退出...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
