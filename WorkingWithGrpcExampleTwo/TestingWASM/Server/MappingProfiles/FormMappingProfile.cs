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
            CreateMap<FormQuestion, FormEntryResponse.Types.Questions>()
                ;
            CreateMap<FormQuestionDTO, TestingWASM.Services.FormEntryResponse.Types.FollowUpQuestion>();
            CreateMap<FormEntryResponse.Types.Questions,FormQuestionDTO>();

            //CreateMap<FormDTO,TestingWASM.Shared.Data.Entities.FormEntry>()
            //    .ForMember(dest => dest.)

            //CreateMap< TestingWASM.Shared.Data.Entities.FormEntry, FormDTO >()
            //    .ForMember(dest => dest.Questions, options => options.Ignore());

            CreateMap<FormQuestion, FormQuestionIntermediaryDTO>();
            CreateMap<QuestionResponseOption, ResponseOptionIntermediaryDTO>();
            CreateMap<QuestionResponseOptionFollowUpQuestionMap, FormQuestionIntermediaryDTO>();
            CreateMap<QuestionResponseOption, TestingWASM.Services.FormEntryResponse.Types.Responseoptions>();
            CreateMap<TestingWASM.Services.FormEntryResponse.Types.Responseoptions, QuestionResponseOption>();
            CreateMap<ResponseOptionIntermediaryDTO, ResponseOptionDTO>()
                .ForMember(dest => dest.FollowUpQuestions, options => options.MapFrom(src => src.FollowUpQuestionMaps));

            CreateMap<FormQuestionIntermediaryDTO, FormQuestionDTO>()
                .ForMember(dest => dest.FormQuestionId, options => options.MapFrom(src => src.ID))
                .ForMember(dest => dest.TypeOfResponse, options => options.MapFrom(src => System.Enum.Parse(typeof(Enums.QuestionResponseType), src.TypeOfResponse)))
                .ForMember(dest => dest.ResponseValue, options =>
                    {
                        options.MapFrom(src => src.ResponseValue ?? src.DefaultResponse);
                        options.SetMappingOrder(100);
                    })
                .ForMember(dest => dest.ResponseOptions, options => options.MapFrom(src => src.QuestionResponseOptions));
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
