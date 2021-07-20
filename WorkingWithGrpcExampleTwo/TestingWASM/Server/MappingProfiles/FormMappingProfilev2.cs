using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using System;
using TestingWASM.Services.v2;
using TestingWASM.Shared.Data.Entities;

namespace TestingWASM.Server.Api.Form.MappingProfiles
{
    public class FormMappingProfilev2 : Profile
    {
        public FormMappingProfilev2()
        {
            CreateMap<DateTime, Timestamp>().ConvertUsing(new TimestampTypeConverter());
            CreateMap<Timestamp, DateTime>().ConvertUsing(new DateTimeTypeConverter());

            CreateMap<FormQuestion, TestingWASM.Services.v2.FormEntryResponse.Types.FormQuestion>()
                  .ForMember(dest => dest.AddTimeStamp, options => options.MapFrom(src => src.AddTimeStamp))
              .ForMember(dest => dest.AddUser, options => options.MapFrom(src => src.AddUser))
                .ForMember(dest => dest.ChangeTimeStamp, options => options.MapFrom(src => src.ChangeTimeStamp))
              .ForMember(dest => dest.ChangeUser, options => options.MapFrom(src => src.ChangeUser))
             .ForMember(dest => dest.FormQuestionEntryID, options => options.Ignore())
            .ForMember(dest => dest.FormQuestionId, options => options.MapFrom(src => src.QuestionId))
            .ForMember(dest => dest.FormSection, options => options.MapFrom(src => src.FormSection))
            .ForMember(dest => dest.DefaultResponse, options => options.MapFrom(src => src.DefaultResponse))
            .ForMember(dest => dest.EndDate, options => options.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Required, options => options.MapFrom(src => src.Required))
            .ForMember(dest => dest.Sequence, options => options.MapFrom(src => src.Sequence))
            .ForMember(dest => dest.ResponseValue, options => options.Ignore())
            .ForMember(dest => dest.TypeOfResponse, options => options.Ignore())
            .ForMember(dest => dest.TypeOfResponseText, options => options.MapFrom(src => src.QuestionResponse.TypeOfResponse))
            .ForMember(dest => dest.Question, options => options.MapFrom(src => src.Question.Text))
            .ForMember(dest => dest.FormQuestionEntryID, options => options.MapFrom(src => src.Id))
            .ForMember(dest => dest.ResponseOptions, options => options.MapFrom(src => src.QuestionResponse.QuestionResponseOptions));

            CreateMap<TestingWASM.Services.v2.FormEntryResponse.Types.FormQuestion, FormQuestion>()
             .ForMember(dest => dest.AddTimeStamp, options => options.MapFrom(src => src.AddTimeStamp))
              .ForMember(dest => dest.AddUser, options => options.MapFrom(src => src.AddUser))
                .ForMember(dest => dest.ChangeTimeStamp, options => options.MapFrom(src => src.ChangeTimeStamp))
              .ForMember(dest => dest.ChangeUser, options => options.MapFrom(src => src.ChangeUser))
            .ForMember(dest => dest.QuestionId, options => options.MapFrom(src => src.FormQuestionId))
            .ForMember(dest => dest.FormSection, options => options.MapFrom(src => src.FormSection))
            .ForMember(dest => dest.DefaultResponse, options => options.MapFrom(src => src.DefaultResponse))
            .ForMember(dest => dest.EndDate, options => options.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Required, options => options.MapFrom(src => src.Required))
            .ForMember(dest => dest.Sequence, options => options.MapFrom(src => src.Sequence))
       //     .ForMember(dest => dest.QuestionResponse.QuestionResponseOptions, options => options.MapFrom(src => src.ResponseOptions))
         //   .ForMember(dest => dest.Question.Text, options => options.MapFrom(src => src.Question))
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.FormQuestionEntryID))
        //    .ForMember(dest => dest.QuestionResponse.TypeOfResponse, options => options.MapFrom(src => src.TypeOfResponseText))
            .ForMember(dest => dest.Question, options => options.Ignore())
              .ForMember(dest => dest.QuestionResponse, options => options.Ignore())
             .ForMember(dest => dest.QuestionResponseOptionFollowUpQuestionMaps, options => options.Ignore());

            CreateMap<FormEntryResponse.Types.FormQuestion.Types.QuestionResponseType, TestingWASM.Shared.Enums.QuestionResponseType>().ReverseMap();
            CreateMap<FormEntryResponse.Types.ResponseOption, QuestionResponseOption>().ReverseMap();
        }

        public class StringToEnumConverter : ITypeConverter<String, int>
        {
            public int Convert(string source, int destination, ResolutionContext context)
            {
                if (System.Enum.IsDefined(typeof(TestingWASM.Shared.Enums.QuestionResponseType), source))
                {
                    foreach (int i in System.Enum.GetValues(typeof(TestingWASM.Shared.Enums.QuestionResponseType)))
                    {
                        if (source.ToLower().Equals(((TestingWASM.Shared.Enums.QuestionResponseType)i).ToString().ToLower()))
                            return i;
                    }
                }

                return 0;
            }
        }

        public class EnumValueToStringConverter : ITypeConverter<int, string>
        {
            public string Convert(int source, string destination, ResolutionContext context)
            {
                return System.Enum.GetName(typeof(TestingWASM.Shared.Enums.QuestionResponseType), source);
            }
        }

        public class TimestampTypeConverter : ITypeConverter<DateTime, Timestamp>
        {
            public Timestamp Convert(DateTime source, Timestamp destination, ResolutionContext context)
            {
                return Timestamp.FromDateTime(DateTime.SpecifyKind(source, DateTimeKind.Utc));
            }
        }

        public class DateTimeTypeConverter : ITypeConverter<Timestamp, DateTime>
        {
            public DateTime Convert(Timestamp source, DateTime destination, ResolutionContext context)
            {
                return source.ToDateTime();
            }
        }
    }
}