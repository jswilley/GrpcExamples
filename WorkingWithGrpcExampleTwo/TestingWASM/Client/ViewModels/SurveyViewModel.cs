namespace Example2.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="SurveyViewModel" />.
    /// </summary>
    public class SurveyViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyViewModel"/> class.
        /// </summary>
        public SurveyViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the FormTypeID.
        /// </summary>
        public int FormTypeID { get; set; }

        /// <summary>
        /// Gets or sets the FormType.
        /// </summary>
        public string FormType { get; set; }

        /// <summary>
        /// Gets or sets the Question.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the QuestionID.
        /// </summary>
        public int QuestionID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the FormSection.
        /// </summary>
        public string FormSection { get; set; }

        /// <summary>
        /// Gets or sets the Sequence.
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the TypeOfResponse.
        /// </summary>
        public string TypeOfResponse { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CommentRequired.
        /// </summary>
        public bool CommentRequired { get; set; }

        /// <summary>
        /// Gets or sets the DefaultResponse.
        /// </summary>
        public string DefaultResponse { get; set; }

        /// <summary>
        /// Gets or sets the FormQuestionId.
        /// </summary>
        public int FormQuestionId { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the MCLNbr.
        /// </summary>
        public int MCLNbr { get; set; }

        /// <summary>
        /// Gets or sets the ResponseValue.
        /// </summary>
        [Required]
        public string ResponseValue { get; set; }

        /// <summary>
        /// Gets or sets the Comment.
        /// </summary>
        public string Comment { get; set; }

        //used for checkboxes
        /// <summary>
        /// The IsSelected.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSelected()
        {
            return !string.IsNullOrEmpty(ResponseValue) && ResponseValue == "Y";
        }
    }
}