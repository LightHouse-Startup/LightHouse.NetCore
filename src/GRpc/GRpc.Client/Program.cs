using System;
using Grpc.Net.Client;
using Grpc.Core;
namespace GRpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number must match the port of the gRPC server.
            var channel = new Channel("localhost:9007", ChannelCredentials.Insecure); // 建立channel
            var client = new IRpcService.clien(channel); // 建立client

            // 调用 RPC API
            var result = client.GetStreamContent(new StreamRequest { FileName = "你想获取的文件路径" });

            var iter = result.ResponseStream; // 拿到响应流
            using (var fs = new FileStream("写获取的数据的文件路径", FileMode.Create)) // 新建一个文件流用于存放我们获取到数据
            {
                while (await iter.MoveNext()) // 迭代
                {
                    iter.Current.Content.WriteTo(fs); // 将数据写入到文件流中
                }
            }
        }
    }
}
