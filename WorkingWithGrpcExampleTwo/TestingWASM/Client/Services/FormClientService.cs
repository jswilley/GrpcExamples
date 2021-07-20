using System.Threading.Tasks;
using TestingWASM.Services.v2;
using static TestingWASM.Services.v2.FormEntryService;

namespace TestingWASM.Services.v2
{
    public interface IFormClientService
    {
        FormEntryServiceClient GRpcClient { get; }

        Task<FormEntryResponse> GetForm(int formTypeId);
    }

    public class FormClientService : IFormClientService
    {


        public FormClientService(FormEntryServiceClient gRpcClient)
        {

            GRpcClient = gRpcClient;
        }

        public FormEntryServiceClient GRpcClient { get; }

        //public async Task<IEnumerable<FormType>> GetAllFormTypes()
        //{
        //    var result = await _service.GetAllFormTypes();
        //    return result;
        //}

        public async Task<FormEntryResponse> GetForm(int formTypeId)
        {
            try
            {
                var req = new FormEntryRequest();
                req.FormTypeId = formTypeId;

                var result = await GRpcClient.GetFormAsync(req);
                return result;
            }
            catch (System.Exception ex)
            {

                throw;
            }
           
           // return null;
        }

        //public async Task<IEnumerable<FormType>> GetFormRelatedTypes()
        //{
        //    var result = await _service.GetFormRelatedTypes();
        //    return result;
        //}


        //public async Task<List<FormQuestionDTO>> GetQuestionsByFormType(int formTypeId)
        //{
        //    var result = await _service.GetQuestionsByFormType(formTypeId);
        //    return result;
        //}
    }
}
