using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TestingWASM.Shared.DTOs
{
    [DataContract]
    public class FormQuestionDTO
    {

        private string responseValue;
        [DataMember(Order = 1)]
        public int FormQuestionId { get; set; }
        [DataMember(Order = 2)]
        public string Question { get; set; }
        [DataMember(Order = 3)]
        public bool Required { get; set; }
        [DataMember(Order = 4)]
        public string FormSection { get; set; }
        [DataMember(Order = 5)]
        public int Sequence { get; set; }
        [DataMember(Order = 6)]
        public Enums.QuestionResponseType TypeOfResponse { get; set; }

        // This is the ID in the FormQuestionTable (where we persist their response)
        [DataMember(Order = 7)]
        public int FormQuestionEntryID { get; set; }

        [DataMember(Order = 8)]
        public string ResponseValue
        {
            get => responseValue;
            set
            {
                responseValue = value;
                if (!string.IsNullOrEmpty(value))
                {
                    ResponseOptions?.ForEach(x => x.Selected = (x.ShortDescription == value) || (TypeOfResponse == Enums.QuestionResponseType.Checkbox && value.Split(',', StringSplitOptions.RemoveEmptyEntries).Contains(x.ShortDescription)));
                }
            }
        }

        [DataMember(Order = 9)]
        public List<ResponseOptionDTO> ResponseOptions { get; set; }
        [DataMember(Order = 10)]
        public string DefaultResponse { get; set; }
        [DataMember(Order = 11)]
        public DateTime? EndDate { get; set; }
    }

    public class ResponseOptionDTO
    {
        [DataMember(Order = 1)]
        public string ShortDescription { get; set; }
        [DataMember(Order = 2)]
        public string LongDescription { get; set; }
        [DataMember(Order = 3)]
        public int Sequence { get; set; }
        [DataMember(Order = 4)]
        public bool Selected { get; set; }
        [DataMember(Order = 5)]
        public List<FormQuestionDTO> FollowUpQuestions { get; set; }
    }
}
