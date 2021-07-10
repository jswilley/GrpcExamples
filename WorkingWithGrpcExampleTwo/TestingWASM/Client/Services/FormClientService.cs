using System.Threading.Tasks;
using static TestingWASM.Services.FormEntry;

namespace TestingWASM.Services
{
    public interface IFormClientService
    {
        FormEntryClient GRpcClient { get; }

        Task<FormEntryResponse> GetForm(int formTypeId);
    }

    public class FormClientService : IFormClientService
    {


        public FormClientService(FormEntryClient gRpcClient)
        {

            GRpcClient = gRpcClient;
        }

        public FormEntryClient GRpcClient { get; }

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
                req.FormTypeId = (uint)formTypeId;

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
