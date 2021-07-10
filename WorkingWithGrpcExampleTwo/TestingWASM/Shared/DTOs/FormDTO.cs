﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TestingWASM.Shared.DTOs
{
    [DataContract]
    public class FormDTO
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2,  IsRequired =true)]
        public string FormTitle { get; set; }
        [DataMember(Order = 3)]
        public DateTime? FormDate { get; set; }

        [DataMember(Order = 4)]
        public List<FormQuestionDTO> Questions { get; set; }

        //public FormDTO()
        //{
        //    Id = ApplicationConstants.NewEntity;
        //    FormDate = DateTime.Now;
            
        //    Questions = new List<FormQuestionDTO>();
        //}
    }
}
