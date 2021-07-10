using System;
using System.Runtime.Serialization;

namespace TestingWASM.Shared.DTOs
{
    [DataContract]
    public class FormQuestionBuilderDTO
    {
        [DataMember(Order = 1)]
        public int FormTypeId { get; set; }
        [DataMember(Order = 2)]
        public string Question { get; set; }
        [DataMember(Order = 3)]
        public string FormSection { get; set; }
        [DataMember(Order = 4)]
        public bool IsRequired { get; set; }
        [DataMember(Order = 5)]
        public int QuestionResponseId { get; set; }
        [DataMember(Order = 6)]
        public int Sequence { get; set; }

        [DataMember(Order = 7)]
        public string DefaultResponse { get; set; }
        [DataMember(Order = 8)]
        public DateTime? EndDate { get; set; }

        [DataMember(Order = 9)]
        public string ApplicationUserName { get; set; }

        [DataMember(Order = 10)]
        public string NullableQuestionResponseTypeId { 
            get { 
                return this.QuestionResponseId.ToString(); }
            set
            {
                if (int.TryParse(value, out int integer))
                    QuestionResponseId = integer;
                else
                    QuestionResponseId = 0;
            }
        }
        [DataMember(Order = 11)]
        public bool? IsRequiredResponse
        {
            get;set;
        }
    }
}
