using Grpc.Net.Client;
using GrpcServiceForWASM.Core.Data.Entities;
using GrpcServiceForWASM.Core.DTOs;
using GrpcServiceForWASM.Core.Interfaces;
using ProtoBuf.Grpc.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWASMGrpc.Services
{

    public interface IFormClientService
    {
        Task<FormDTO> GetForm(int formTypeId);

     
        Task<List<FormQuestionDTO>> GetQuestionsByFormType(int formTypeId);
      

        Task<IEnumerable<FormType>> GetFormRelatedTypes();
        Task<IEnumerable<FormType>> GetAllFormTypes();
    }
    public class FormClientService : IFormClientService
    {
        private readonly GrpcChannel channel;
        public IFormReadService _service;
        public FormClientService(GrpcChannel channel)
        {
            _service = channel.CreateGrpcService<IFormReadService>();
        }

        public async Task<IEnumerable<FormType>> GetAllFormTypes()
        {
            var result = await _service.GetAllFormTypes();
            return result;
        }

        public async Task<FormDTO> GetForm(int formTypeId)
        {
            var result = await _service.GetForm(formTypeId);
            return result;
        }

        public async Task<IEnumerable<FormType>> GetFormRelatedTypes()
        {
            var result = await _service.GetFormRelatedTypes();
            return result;
        }


        public async Task<List<FormQuestionDTO>> GetQuestionsByFormType(int formTypeId)
        {
            var result = await _service.GetQuestionsByFormType(formTypeId);
            return result;
        }
    }
}
