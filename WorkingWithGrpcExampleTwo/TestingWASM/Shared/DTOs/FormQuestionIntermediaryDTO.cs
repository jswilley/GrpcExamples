using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestingWASM.Shared.DTOs
{
    [DataContract]
    public class FormQuestionIntermediaryDTO
    {
        [DataMember(Order = 1)]
        public int ID { get; set; }
        [DataMember(Order = 2)]
        public int FormQuestionID { get; set; }
        [DataMember(Order = 3)]
        public int? FollowUpQuestionID { get; set; }

        [DataMember(Order = 4)]
        public string Question { get; set; }
        [DataMember(Order = 5)]
        public string FormSection { get; set; }

        [DataMember(Order = 6)]
        public bool Required { get; set; }

        [DataMember(Order = 7)]
        public int Sequence { get; set; }

        [DataMember(Order = 8)]
        public string TypeOfResponse { get; set; }

        [DataMember(Order = 9)]
        public string DefaultResponse { get; set; }

        [DataMember(Order = 10)]
        public string ResponseValue { get; set; }

        [DataMember(Order = 11)]
        public DateTime? EndDate { get; set; }

        [DataMember(Order = 12)]
        public IEnumerable<ResponseOptionIntermediaryDTO> QuestionResponseOptions { get; set; }
    }
}
