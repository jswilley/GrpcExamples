using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestingWASM.Shared.DTOs
{

    [DataContract]
    public class ResponseOptionIntermediaryDTO
    {
        [DataMember(Order = 1)]
        public string ShortDescription { get; set; }
        [DataMember(Order = 2)] 
        public string LongDescription { get; set; }
        [DataMember(Order = 3)]
        public int Sequence { get; set; }
        [DataMember(Order = 4)]
        public IEnumerable<FormQuestionIntermediaryDTO> FollowUpQuestionMaps { get; set; }
    }
}
