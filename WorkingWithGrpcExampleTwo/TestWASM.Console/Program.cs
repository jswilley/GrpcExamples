using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using static TestingWASM.Services.FormEntry;

namespace TestWASM
{
    class Program
    {
        static async Task Main(string[] args)
        {


            Console.WriteLine("Calling gRPC service...");

            using (var channel = GrpcChannel.ForAddress("https://localhost:44366"))
            {
                var client = new FormEntryClient(channel);
                var reply = await client.GetFormAsync(new TestingWASM.Services.FormEntryRequest() { FormTypeId = 1 });
                Console.WriteLine($"return id {reply.Id}");
            }
            Console.ReadLine();
        }
    }
}
