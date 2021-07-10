using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestingWASM.Shared.DTOs
{

    [DataContract]
    public class FormSummaryDTO
    {
        [DataMember(Order = 1)] 
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public int BookingNumber { get; set; }

        [DataMember(Order = 3)]
        public int SwisID { get; set; }

        [DataMember(Order = 4)] 
        public int FormTypeID { get; set; }
        [DataMember(Order = 5)]
        public string FormTitle { get; set; }

        [DataMember(Order = 6)]
        public DateTime? FormDate { get; set; }

        [DataMember(Order = 7)]
        public string Status { get; set; }
    }
}
