using AutoMapper;
using System;
using TestingWASM.Shared;
using TestingWASM.Shared.Data.Entities;
using TestingWASM.Shared.DTOs;
using TestingWASM.Services;

namespace TestingWASM.Server.Api.Form.MappingProfiles
{
    public class FormMappingProfile : Profile
    {
        public FormMappingProfile()
        {
          
              //  .ForMember(dest => dest..FormQuestions, options => options.Ignore());
            CreateMap<FormQuestionDTO, FormQuestion>();
            CreateMap<FormQuestion, FormEntryResponse.Types.Questions>();
            CreateMap<FormEntryResponse.Types.Questions,FormQuestion>();
           
           

            //CreateMap<FormQuestionDTO, FormFollowUpQuestion>()
            //    .ForMember(dest => dest.ID, options => options.MapFrom(src => src.FormQuestionID))
            //    .ForMember(dest => dest.FollowUpQuestionID, options => options.MapFrom(src => src.FormQuestionID))
            //    .ForMember(dest => dest.FormQuestionID, options => options.Ignore());

            //CreateMap< FormEntry, FormDTO >()
            //    .ForMember(dest => dest.Questions, options => options.Ignore());

            CreateMap<FormQuestion, FormQuestionIntermediaryDTO>();
            CreateMap<QuestionResponseOption, ResponseOptionIntermediaryDTO>();
            CreateMap<QuestionResponseOptionFollowUpQuestionMap, FormQuestionIntermediaryDTO>();
            CreateMap<QuestionResponseOption, FormEntryResponse.Types.Responseoptions>();
            CreateMap<FormEntryResponse.Types.Responseoptions, QuestionResponseOption>();
            CreateMap<ResponseOptionIntermediaryDTO, ResponseOptionDTO>()
                .ForMember(dest => dest.FollowUpQuestions, options => options.MapFrom(src => src.FollowUpQuestionMaps));

            CreateMap<FormQuestionIntermediaryDTO, FormQuestionDTO>()
                .ForMember(dest => dest.FormQuestionId, options => options.MapFrom(src => src.ID))
                .ForMember(dest => dest.TypeOfResponse, options => options.MapFrom(src => Enum.Parse(typeof(Enums.QuestionResponseType), src.TypeOfResponse)))
                .ForMember(dest => dest.ResponseValue, options =>
                    {
                        options.MapFrom(src => src.ResponseValue ?? src.DefaultResponse);
                        options.SetMappingOrder(100);
                    })
                .ForMember(dest => dest.ResponseOptions, options => options.MapFrom(src => src.QuestionResponseOptions));
        }
    }
}
