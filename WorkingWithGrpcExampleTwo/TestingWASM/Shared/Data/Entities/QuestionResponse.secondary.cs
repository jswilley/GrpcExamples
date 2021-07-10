using System.Collections.Generic;

namespace poc.Core.Data.Entities
{
    public partial class QuestionResponse
    {
        public virtual ICollection<QuestionResponseOption> QuestionResponseOptions { get; set; }
    }
}
