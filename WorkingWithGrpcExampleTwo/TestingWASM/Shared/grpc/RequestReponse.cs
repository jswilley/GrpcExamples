using TestingWASM.Shared.Data.Entities;
using TestingWASM.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TestingWASM.Shared.grpc
{
    public class AllFormTypesRequest
    {
    }

    public class FormQuestionsRequest
    {
        public int Id { get; set; }
    }

    [ServiceContract]
    public interface IFormService
    {
        Task<IEnumerable<FormType>> GetAllFormTypes(AllFormTypesRequest request);
        Task<FormDTO> GetQuestions(FormQuestionsRequest request);
    }
}
