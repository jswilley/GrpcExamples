using AutoMapper;
using System;
using TestingWASM.Shared;
using TestingWASM.Shared.Data.Entities;
using TestingWASM.Shared.DTOs;
using TestingWASM.Services;
using Google.Protobuf.WellKnownTypes;

namespace TestingWASM.Server.Api.Form.MappingProfiles
{
    public class FormMappingProfile : Profile
    {
        public FormMappingProfile()
        {

            CreateMap<DateTime, Timestamp>().ConvertUsing(new TimestampTypeConverter());
            CreateMap<Timestamp, DateTime>().ConvertUsing(new DateTimeTypeConverter());
            CreateMap<FormQuestionDTO, FormQuestion>();
            CreateMap<FormQuestion, FormEntryResponse.Types.FormQuestionEntries.Types.FormQuestion>()
                ;
          
            CreateMap<FormEntryResponse.Types.FormQuestionEntries,FormQuestionEntry>().ReverseMap();
            //CreateMap<FormEntryResponse.Types.FormQuestionEntries..Types.FormQuestion, FormQuestion>().ReverseMap();
            CreateMap<FormEntryResponse.Types.FormQuestionEntries.Types.Question, Question>().ReverseMap();
            CreateMap<FormEntryResponse.Types.FormQuestionEntries.Types.QuestionResponse, QuestionResponse>().ReverseMap();
            CreateMap<FormEntryResponse.Types.FormQuestionEntries.Types.FResponseoptions, QuestionResponseOption>().ReverseMap();
            CreateMap<FormEntryResponse.Types.FormQuestionEntries.Types.FollowUpQuestion, FollowUpQuestion>().ReverseMap();
            CreateMap<FormEntryResponse.Types.FormQuestionEntries.Types.Responseoptions, QuestionResponseOption>().ReverseMap();
         
          
           
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
