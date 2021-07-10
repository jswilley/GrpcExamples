using System.Runtime.Serialization;

namespace TestingWASM.Shared.DTOs
{

    [DataContract]
    public class LookupDTO
    {
        [DataMember(Order = 1)]
        public int ID { get; set; }
        [DataMember(Order = 2)] 
        public string Title { get; set; }
        [DataMember(Order = 3)]
        public int SortOrder { get; set; }
    }
}
