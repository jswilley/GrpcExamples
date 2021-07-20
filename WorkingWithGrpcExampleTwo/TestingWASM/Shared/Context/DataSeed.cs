using System.Collections.Generic;
using System.Linq;
using TestingWASM.Shared.Data.Entities;

namespace TestingWASM.Shared.Context
{
    public class DataSeed
    {
        public static FormType GenerateFormType()
        {
            return new FormType()
            {
                Id = 1,
                Title = "Team A",
                ShortName = "TeamA",
                EndDate = System.DateTime.Parse("2049-01-01T00:00:00"),
                SortOrder = 1,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed"
            };
        }

        public static FormQuestionEntry GenerationFormQuestionEntry()
        {
            return new FormQuestionEntry()
            {
                FormQuestionId = 0,
                FormId = 1,
                Id = 0,
                ResponseValue = "",
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
              
            };
        }

        public static FormQuestion GenFormQuestion1()
        {
            var que = GenQuestionList();
            var resp1 = GenListQuestionResponse().First(a => a.TypeOfResponse == "Select");

            var result =  new FormQuestion()
            {
                Id = 0,

                Required = false,
                FormSection = "Meetings",
                Sequence = 1,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                DefaultResponse = null,
                EndDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                FormTypeId = 1,
                FormType = GenerateFormType(),
                Question = que.First(),
                QuestionResponse = resp1,
                QuestionResponseId = 0
            };

            return result;
        }

        public static FormQuestion GenFormQuestion2()
        {
            var que = GenQuestionList();

            return new FormQuestion()
            {
                Id = 0,

                Required = false,
                FormSection = "Meetings",
                Sequence = 2,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                DefaultResponse = null,
                EndDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                FormTypeId = 1,
                FormType = GenerateFormType(),
                Question = GenQuestionList().Skip(1).First(),
                 QuestionResponse = GenListQuestionResponse().First(a => a.TypeOfResponse == "Text"),
                 QuestionResponseId = 0
                 
            };
        }

        public static FormQuestion GenFormQuestion3()
        {
            var que = GenQuestionList();

            return new FormQuestion()
            {
                Id = 0,

                Required = false,
                FormSection = "Meetings",
                Sequence = 3,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                DefaultResponse = null,
                EndDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                FormTypeId = 1,
                FormType = GenerateFormType(),
                Question = GenQuestionList().Skip(2).First(),
                QuestionResponse = GenListQuestionResponse().First(a => a.TypeOfResponse == "Date"),
                QuestionResponseId = 0

            };
        }

        public static FormQuestion GenFormQuestion4()
        {
            var que = GenQuestionList();

            return new FormQuestion()
            {
                Id = 0,

                Required = false,
                FormSection = "Meetings",
                Sequence = 4,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                DefaultResponse = null,
                EndDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                FormTypeId = 1,
                FormType = GenerateFormType(),
                Question = GenQuestionList().Skip(3).First(),
                QuestionId = 0,
               
                QuestionResponse = GenListQuestionResponse().First(a => a.TypeOfResponse == "Number"),
                QuestionResponseId = 0
            };
        }

        public static List<QuestionResponse> GenListQuestionResponse()
        {
            var result = new List<QuestionResponse>();
            var resp = GenQuestionResponseOption();
            result.Add(new QuestionResponse()
            {
                Id = 0,
                TypeOfResponse = "Radio",
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                QuestionResponseOptions = resp.Where( a => a.Id == 1 || a.Id == 2).ToList()
            });
            result.Add(new QuestionResponse()
            {
                Id = 0,
                TypeOfResponse = "Text",

                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed"
            });
            result.Add(new QuestionResponse()
            {
                Id = 0,
                TypeOfResponse = "Checkbox",

                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed"
            });

            result.Add(new QuestionResponse()
            {
                Id = 0,
                TypeOfResponse = "Select",

                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                QuestionResponseOptions = GenQuestionResponseOption().Where(a => a.Id > 2).ToList()

                
            });

            result.Add(new QuestionResponse()
            {
                Id = 1,
                TypeOfResponse = "Number",

                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed"
            });

            result.Add(new QuestionResponse()
            {
                Id = 0,
                TypeOfResponse = "Date",

                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed"
            });

            return result;
        }

        public static List<QuestionResponseOption> GenQuestionResponseOption()
        {
            var result = new List<QuestionResponseOption>();
            result.Add(new QuestionResponseOption()
            {
                Id = 1,
                //QuestionResponse = GenListQuestionResponse().First(),
                EffectiveDate = System.DateTime.Parse("2020-02-21T10:07:23"),
                ExpirationDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                ShortDescription = "Y",
                LongDescription = "Yes",
                SortOrder = 1,
                QuestionResponseId = 0,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
            });


            result.Add(new QuestionResponseOption()
            {
                Id = 2,
                //QuestionResponse = GenListQuestionResponse().Skip(1).First(),
                EffectiveDate = System.DateTime.Parse("2020-02-21T10:07:23"),
                ExpirationDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                ShortDescription = "N",
                LongDescription = "No",
                SortOrder = 2,
                QuestionResponseId = 0,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
            });

          

            result.Add(new QuestionResponseOption()
            {
                Id = 3,
                //QuestionResponse = GenListQuestionResponse().First(a => a.TypeOfResponse == "Select"),
                EffectiveDate = System.DateTime.Parse("2020-02-21T10:07:23"),
                ExpirationDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                ShortDescription = "Meeting A",
                LongDescription = "Meeting Type A",
                SortOrder = 1,
                QuestionResponseId = 0,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
            });

            result.Add(new QuestionResponseOption()
            {
                Id = 4,
                //QuestionResponse = GenListQuestionResponse().First(a => a.TypeOfResponse == "Select"),
                EffectiveDate = System.DateTime.Parse("2020-02-21T10:07:23"),
                ExpirationDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                ShortDescription = "Meeting B",
                LongDescription = "Meeting Type B",
                SortOrder = 2,
                QuestionResponseId = 0,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
            });

            result.Add(new QuestionResponseOption()
            {
                Id = 5,
                //QuestionResponse = GenListQuestionResponse().First(a => a.TypeOfResponse == "Select"),
                EffectiveDate = System.DateTime.Parse("2020-02-21T10:07:23"),
                ExpirationDate = System.DateTime.Parse("2077-12-30T00:00:00"),
                ShortDescription = "Meeting C",
                LongDescription = "Meeting Type C",
                SortOrder = 3,
                QuestionResponseId = 0,
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
            });

            return result;
        }

        public static List<Question> GenQuestionList()
        {
            var result = new List<Question>();
            result.Add(
               new Question()
               {
                   Id = 1,
                   Text = "Meeting Type",
                   EndDate = System.DateTime.Parse("Feb 21 2071 10:08AM"),
                   AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                   AddUser = "seed",
                   ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                   ChangeUser = "seed"
               });

            result.Add(new Question()
            {
                Id = 2,
                Text = "Other Meeting Type",
                EndDate = System.DateTime.Parse("Feb 21 2071 10:08AM"),
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed"
            });

            result.Add(new Question()
            {
                Id = 3,
                Text = "Meeting Date",
                EndDate = System.DateTime.Parse("Feb 21 2071 10:08AM"),
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed"
            });

            result.Add(new Question()
            {
                Id = 4,
                Text = "Number of Attendess",
                EndDate = System.DateTime.Parse("Feb 21 2071 10:08AM"),
                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed"
            });

            return result;
        }

        public static FormEntry GenFormEntry()
        {
            return new FormEntry()
            {
                Id = 1,
                FormTypeId = 1,
                AddTimeStamp = System.DateTime.Now,
                ChangeTimeStamp = System.DateTime.Now,
                AddUser = "seed",
                ChangeUser = "seed",
                FormType = DataSeed.GenerateFormType()
            };
        }
        public static List<FormQuestionEntry> GenFormQuestionEntries()
        {
            var result = new List<FormQuestionEntry>();

            result.Add(new FormQuestionEntry()
            {

                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                FormId = 0,
                Id = 0,
                ResponseValue = string.Empty,
               FormQuestion = GenFormQuestion1(),
                FormQuestionId = 1
            });

            result.Add(new FormQuestionEntry()
            {

                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                FormId = 0,
                Id = 0,
                ResponseValue = string.Empty,
                FormQuestion = GenFormQuestion2(),
                FormQuestionId = 2
            });

            result.Add(new FormQuestionEntry()
            {

                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                FormId = 0,
                Id = 0,
                ResponseValue = string.Empty,
                FormQuestion = GenFormQuestion3(),
                FormQuestionId = 3
            });

            result.Add(new FormQuestionEntry()
            {

                AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                AddUser = "seed",
                ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
                ChangeUser = "seed",
                FormId = 0,
                Id = 0,
                ResponseValue = string.Empty,
                FormQuestion = GenFormQuestion4(),
                FormQuestionId = 4
            });

            return result;
        }
        //public static List<FormQuestionEntry> GenFormQuestionEntries()
        //{
        //    var result = new List<FormQuestionEntry>();

        //    result.Add(new FormQuestionEntry()
        //    {

        //        AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
        //        AddUser = "seed",
        //        ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
        //        ChangeUser = "seed",
        //        FormId = 0,
        //        Id = 0,
        //        ResponseValue = string.Empty,
        //        FormQuestion = GenFormQuestion1(),
        //        FormQuestionId = 0
        //    });

        //    result.Add(new FormQuestionEntry()
        //    {

        //        AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
        //        AddUser = "seed",
        //        ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
        //        ChangeUser = "seed",
        //        FormId = 0,
        //        Id = 0,
        //        ResponseValue = string.Empty,
        //        FormQuestion = GenFormQuestion2(),
        //        FormQuestionId = 0
        //    });

        //    result.Add(new FormQuestionEntry()
        //    {

        //        AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
        //        AddUser = "seed",
        //        ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
        //        ChangeUser = "seed",
        //        FormId = 0,
        //        Id = 0,
        //        ResponseValue = string.Empty,
        //        FormQuestion = GenFormQuestion3(),
        //        FormQuestionId = 0
        //    });

        //    result.Add(new FormQuestionEntry()
        //    {

        //        AddTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
        //        AddUser = "seed",
        //        ChangeTimeStamp = System.DateTime.Parse("2021-02-21T10:07:23"),
        //        ChangeUser = "seed",
        //        FormId = 0,
        //        Id = 0,
        //        ResponseValue = string.Empty,
        //        FormQuestion = GenFormQuestion4(),
        //        FormQuestionId = 0
        //    });

        //    return result;
        //}
    }
}