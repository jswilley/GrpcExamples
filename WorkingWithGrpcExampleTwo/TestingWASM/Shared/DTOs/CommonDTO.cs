using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestingWASM.Shared.DTOs
{
    [DataContract]
    public class CommonDTO
    {
        [DataMember(Order = 1)]
        public int CommonID { get; set; }
        [DataMember(Order = 2)]
        public int TableNumber { get; set; }
        [DataMember(Order = 3)]
        public string Code { get; set; }
        [DataMember(Order = 4)]
        public string CodeDescription { get; set; }
        [DataMember(Order = 5)]
        public string AdditionalInfo1 { get; set; }
        [DataMember(Order = 6)]
        public string AdditionalInfo2 { get; set; }
        [DataMember(Order = 7)]
        public DateTime StartDate { get; set; }
        [DataMember(Order = 8)]
        public DateTime EndDate { get; set; }

        //public string AppIndicator { get; set; }

        //public DateTime AddTimestamp { get; set; }

        //public string AddUser { get; set; }

        //public DateTime ChgTimestamp { get; set; }

        //public string ChgUser { get; set; }
    }
}
