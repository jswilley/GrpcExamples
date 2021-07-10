

using System.Collections.Generic;
using System.Data;
using System.Linq;
using TestingWASM.Shared.DTOs;

namespace TestingWASM.ViewModels
{
   

    /// <summary>
    /// Defines the <see cref="FormViewModel" />.
    /// </summary>
    public class FormViewModel
    {
        /// <summary>
        /// Gets or sets the FormEntry.
        /// </summary>
        public FormDTO FormEntry { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormViewModel"/> class.
        /// </summary>
        public FormViewModel()
        {
            FormEntry = new FormDTO();
        }

       

        /// <summary>
        /// The Sections.
        /// </summary>
        /// <returns>The <see cref="List{string}"/>.</returns>
        public List<string> Sections()
        {
            return FormEntry.Questions.Any(a => !string.IsNullOrEmpty(a.FormSection)) ? FormEntry.Questions.Select(a => a.FormSection).Distinct().ToList() : new List<string>();
        }
    }
}
