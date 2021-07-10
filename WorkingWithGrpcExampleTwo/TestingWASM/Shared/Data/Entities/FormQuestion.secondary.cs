using System.Collections.Generic;

namespace poc.Core.Data.Entities
{
    public partial class FormQuestion
    {
        public virtual ICollection<FormFollowUpQuestionEntry> FormFollowUpQuestions { get; set; }
    }
}
