using Grpc.Net.Client;

using ProtoBuf.Grpc.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static TestingWASM.Services.FormEntry;

namespace GrpcConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Type a key!");
            Console.ReadKey();

            Console.WriteLine("Calling gRPC service...");

            ;

            //using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            //{
            //    var client = new LuPig.LuPigClient(channel);
            //    var reply = client.SuckingPig(new SuckingPigRequest { Name = "小猪" });
            //    Console.WriteLine(reply);
            //    Console.ReadKey();
            //}

            using (var channel = GrpcChannel.ForAddress("https://localhost:44366"))
            {
                //var client = channel.CreateGrpcService<IFormEntryService>();
                //var req = TestingWASM.Services.FormEntryRequest.FormEntryRequest();

                //var result = await testFacade.GetForm(req);

                var client = new FormEntryClient(channel);
                var reply  = await client.GetFormAsync(new TestingWASM.Services.FormEntryRequest() { FormTypeId = 1 });
                Console.WriteLine(reply.Id);
                
            }
            Console.ReadLine();
        }
    }
}
