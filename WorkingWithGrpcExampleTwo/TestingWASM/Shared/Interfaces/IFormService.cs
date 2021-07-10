using TestingWASM.Shared.Data.Entities;
using TestingWASM.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingWASM.Shared.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IFormEntryService" />.
    /// </summary>
    public interface IFormEntryService
    {

        Task<FormEntryRequest> GetForm(int formTypeId);

        /////// <summary>
        /////// The GetClientForm.
        /////// </summary>
        /////// <param name="formId">The formId<see cref="int"/>.</param>
        /////// <param name="bookingNumber">The bookingNumber<see cref="int"/>.</param>
        /////// <returns>The <see cref="FormDTO"/>.</returns>
        ////FormDTO GetClientForm(int formId, int bookingNumber);
        //Task<List<FormQuestionDTO>> GetQuestionsByFormType(int formTypeId);
        //Task<List<FormType>> GetFormTypes();

        //Task<IEnumerable<FormType>> GetFormRelatedTypes();
        //Task<IEnumerable<FormType>> GetAllFormTypes();


    }


}
