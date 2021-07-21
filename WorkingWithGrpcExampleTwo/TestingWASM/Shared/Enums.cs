namespace Example2.Shared
{
    public class Enums
    {
        public enum FormType
        {
            Group1 = 1,
            Group2 = 2
        }

        public enum QuestionResponseType
        {
            Checkbox,
            Radio,
            Select,
            Text,
            Number,
            Date
        }


        public enum QuestionSelectionMode
        {
            Checkbox = 2,
            DropDownList = 3,
            Matrix = 4,
            MultiMatrix = 6,
            RadioButton = 1,
            Static = 5
        }

        public enum AnswerTypeEnum
        {
            SelectionTextType = 1,
            SelectionOtherType = 2,
            FieldBasicType = 3,

            BooleanType = 20,
            FieldRequiredType = 21,
            FieldCalendarType = 22,
            FieldLargeType = 24,
            FieldEmailType = 26,
            FieldHiddenType = 27,
            FieldPasswordType = 28,

            ExtendedFreeTextBoxType = 31,
            FieldAddressType = 55,
            FieldSliderType = 56
        }

    }
}
