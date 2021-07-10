using System.Collections.Generic;
using System.Threading.Tasks;
using TestingWASM.Services;
using TestingWASM.Shared.Data.Entities;
using TestingWASM.Shared.DTOs;

namespace TestingWASM.Server.Api.Form.Services
{
    public interface IFormEntryService
    {
        Task<IEnumerable<FormType>> GetAllFormTypes();
        Task<FormEntryResponse> GetForm(FormEntryRequest request);
        Task<IEnumerable<FormType>> GetFormRelatedTypes();
        Task<List<FormType>> GetFormTypes();
        Task<List<FormQuestionDTO>> GetQuestionsByFormType(int formTypeId);
    }
}