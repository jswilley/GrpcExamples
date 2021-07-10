using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestingWASM.Shared.DTOs
{

    [DataContract]
    public class FormQuestionListDTO
    {
        [DataMember(Order = 1)]
        public int FormTypeID { get; set; }
        [DataMember(Order = 2)]
        public string FormTitle { get; set; }
        [DataMember(Order = 3)]
        public string ApplicationUserName { get; set; }

        [DataMember(Order = 4)] 
        public List<FormQuestionDTO> Questions { get; set; }
        
        public FormQuestionListDTO()
        {
            ApplicationUserName = ApplicationConstants.UnknownUser;
            Questions = new List<FormQuestionDTO>();
        }
    }
}
