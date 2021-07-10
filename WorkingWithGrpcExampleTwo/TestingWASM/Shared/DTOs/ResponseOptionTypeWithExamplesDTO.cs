using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestingWASM.Shared.DTOs
{

    [DataContract]
    public class ResponseOptionTypeWithExamplesDTO
    {
        [DataMember(Order = 1)]
        public int ID { get; set; }
        [DataMember(Order = 2)]
        public string TypeOfResponse { get; set; }

        [DataMember(Order = 3)]
        public string ResponseOptionExamples { get; set; }
    }
}
