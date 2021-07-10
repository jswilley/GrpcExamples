﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TestingWASM.Shared.Data.Entities
{
    [Table("QuestionResponseOptionFollowUpQuestionMap")]
    public partial class QuestionResponseOptionFollowUpQuestionMap
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("FormQuestionID")]
        public int FormQuestionId { get; set; }
        [Column("ResponseOptionID")]
        public int ResponseOptionId { get; set; }
        [Column("FollowUpQuestionID")]
        public int FollowUpQuestionId { get; set; }
        public int SortOrder { get; set; }
        [Required]
        public bool Required { get; set; }
        [Column(TypeName = "date")]
        public DateTime EffectiveDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ExpirationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AddTimeStamp { get; set; }
        [Required]
        [StringLength(50)]
        public string AddUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ChangeTimeStamp { get; set; }
        [Required]
        [StringLength(50)]
        public string ChangeUser { get; set; }

        [ForeignKey("FormQuestionID")]
        public virtual FormQuestion FormQuestion { get; set; }

        [ForeignKey("ResponseOptionID")]
        public virtual QuestionResponseOption QuestionResponseOption { get; set; }

        [ForeignKey("FollowUpQuestionID")]
        public virtual FollowUpQuestion FollowUpQuestion { get; set; }
    }
}