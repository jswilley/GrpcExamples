using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example2.Shared.Data.Entities
{
    [Table("FormQuestionEntry")]
    public partial class FormQuestionEntry
    {
        public FormQuestionEntry()
        {

        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int FormQuestionId { get; set; }
        public int FormId { get; set; }
        [Required]
        [StringLength(500)]
        public string ResponseValue { get; set; }
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

        [ForeignKey(nameof(FormId))]
        [InverseProperty(nameof(FormEntry.FormQuestionEntries))]
        public virtual FormEntry Form { get; set; }
        [ForeignKey(nameof(FormQuestionId))]
        [InverseProperty("FormQuestionEntries")]
        public virtual FormQuestion FormQuestion { get; set; }

    }
}
