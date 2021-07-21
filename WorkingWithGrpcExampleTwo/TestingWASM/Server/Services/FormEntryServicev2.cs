using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TestingWASM.Services.v2;
using TestingWASM.Shared;
using TestingWASM.Shared.Context;

namespace TestingWASM.Server.Services.V2
{
    public class FormEntryService : TestingWASM.Services.v2.FormEntryService.FormEntryServiceBase
    {
        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;

        private readonly ILogger<FormEntryService> logger;
        private readonly pocContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormEntryService"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="pocContext"/>.</param>
        /// <param name="mapper">The mapper<see cref="IMapper"/>.</param>
        /// <param name="cache"></param>
        public FormEntryService(pocContext context, IMapper mapper, ILogger<FormEntryService> logger)
        {
            _mapper = mapper;
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
            this.logger = logger;
            _context = context;
        }

        public override async Task<TestingWASM.Services.v2.FormEntryResponse> GetForm(TestingWASM.Services.v2.FormEntryRequest request, ServerCallContext context)
        {
            var id = (int)request.FormTypeId;
            //pull seed data
            var frm = DataSeed.GenFormEntry();
            var frment = DataSeed.GenFormQuestionEntries();
            foreach (var item in frment)
            {
                frm.FormQuestionEntries.Add(item);
            }

            //map seed data to protobuf classes
            var form = new TestingWASM.Services.v2.FormEntryResponse
            {
                Id = id,
                FormTitle = frm.FormType.Title,
                FormDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc))
            };
            try
            {
                //TODO get mapping working var mapped = frm.FormQuestionEntries.Select(a => _mapper.Map<TestingWASM.Services.v2.FormEntryResponse.Types.FormQuestion>(a));

                foreach (var item in frm.FormQuestionEntries)
                {
                    var entry = new TestingWASM.Services.v2.FormEntryResponse.Types.FormQuestion()
                    {
                        FormQuestionEntryID = ApplicationConstants.NewEntity,
                        FormQuestionId = item.FormQuestionId,

                        Question = item.FormQuestion.Question.Text,
                        FormSection = item.FormQuestion.FormSection,
                        Required = item.FormQuestion.Required.GetValueOrDefault(),
                        Sequence = item.FormQuestion.Sequence,
                        EndDate = Timestamp.FromDateTime(DateTime.SpecifyKind(item.FormQuestion.EndDate.GetValueOrDefault(), DateTimeKind.Utc)),
                        //TypeOfResponse = TestingWASM.Shared.Enums.QuestionResponseType,
                        DefaultResponse = item.FormQuestion.DefaultResponse,
                        AddTimeStamp = Timestamp.FromDateTime(DateTime.SpecifyKind(item.AddTimeStamp, DateTimeKind.Utc)),
                        AddUser = item.AddUser,
                        ChangeTimeStamp = Timestamp.FromDateTime(DateTime.SpecifyKind(item.ChangeTimeStamp, DateTimeKind.Utc)),
                        ChangeUser = item.ChangeUser,
                        ResponseValue = item.ResponseValue,
                        TypeOfResponseText = item.FormQuestion.QuestionResponse.TypeOfResponse
                    };

                    if (item.FormQuestion.QuestionResponse.QuestionResponseOptions != null)
                        foreach (var opt in item.FormQuestion.QuestionResponse.QuestionResponseOptions)
                        {
                            var optGrpc = new FormEntryResponse.Types.ResponseOption()
                            {
                                LongDescription = opt.LongDescription,
                                ShortDescription = opt.ShortDescription,
                                SortOrder = opt.SortOrder
                            };
                            entry.ResponseOptions.Add(optGrpc);
                        }

                    switch (item.FormQuestion.QuestionResponse.TypeOfResponse)
                    {
                        case "Select":
                            entry.TypeOfResponse = FormEntryResponse.Types.FormQuestion.Types.QuestionResponseType.Select;
                            break;

                        case "Radio":
                            entry.TypeOfResponse = FormEntryResponse.Types.FormQuestion.Types.QuestionResponseType.Radio;
                            break;

                        case "Number":
                            entry.TypeOfResponse = FormEntryResponse.Types.FormQuestion.Types.QuestionResponseType.Number;
                            break;

                        case "Text":
                            entry.TypeOfResponse = FormEntryResponse.Types.FormQuestion.Types.QuestionResponseType.Text;
                            break;

                        case "Date":
                            entry.TypeOfResponse = FormEntryResponse.Types.FormQuestion.Types.QuestionResponseType.Date;
                            break;

                        default:
                            entry.TypeOfResponse = FormEntryResponse.Types.FormQuestion.Types.QuestionResponseType.Checkbox;
                            break;
                    }

                    form.Questions.Add(entry);
                }

                return form;
            }
            catch (Exception)
            {
                throw;
            }

            return await base.GetForm(request, context);
        }
    }
}