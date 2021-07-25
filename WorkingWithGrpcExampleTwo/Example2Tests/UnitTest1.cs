using AutoMapper;
using Example2.Server.Services.V2;
using Example2.Services.v2;
using Example2.Shared.Context;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Example2Tests
{
    public class ServerServiceTests
    {

        private readonly pocContext so;
        private readonly Mock<ILogger<Example2.Server.Services.V2.FormEntryService>> logger;
        private readonly Mock<IMapper> mapper;
        // private readonly HttpClient client;
        private Example2.Server.Services.V2.FormEntryService service;
        public ServerServiceTests()
        {
            DbContextOptionsBuilder<pocContext> options = new DbContextOptionsBuilder<pocContext>();
            DbContextOptionsBuilder<pocContext> build = InMemoryDbContextOptionsExtensions.UseInMemoryDatabase(options, "pocdb");
            mapper = new Mock<IMapper>();
            logger = new Mock<ILogger<Example2.Server.Services.V2.FormEntryService>>();
            so = new pocContext(build.Options);
            service = new Example2.Server.Services.V2.FormEntryService(so, mapper.Object, logger.Object);
            
          
        }

        [Fact]
        public async Task ServerGetTest1()
        { 
            var request = new Example2.Services.v2.FormEntryRequest() { FormTypeId = 1 };
            var result = await service.GetForm(request, null);
            Assert.IsType<Example2.Services.v2.FormEntryResponse>(result);
            Assert.False(string.IsNullOrEmpty(result.FormTitle));
        }

       
    }
}
