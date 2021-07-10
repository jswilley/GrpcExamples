using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingWASM.Shared.Data.Entities
{
    [Table("FormEntry")]
    public partial class FormEntry
    {
        public FormEntry()
        {
            #region Generated Constructor

            FormQuestionEntries = new HashSet<FormQuestionEntry>();

            #endregion Generated Constructor
        }

        #region Generated Properties
		[Key]
        [Column("ID")]

        public int Id { get; set; }

        public int FormTypeId { get; set; }

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

        
        #endregion Generated Properties

        #region Generated Relationships

         [InverseProperty(nameof(FormQuestionEntry.Form))]
        public virtual ICollection<FormQuestionEntry> FormQuestionEntries { get; set; }
        
		[ForeignKey(nameof(FormTypeId))]
        [InverseProperty("FormEntries")]
        public virtual FormType FormType { get; set; }
        #endregion Generated Relationships
    }
}