namespace TestingWASM.Server.Api.Form.Services
{
    using AutoMapper;
    using Grpc.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using TestingWASM.Services;
    using TestingWASM.Shared;
    using TestingWASM.Shared.Context;
    using TestingWASM.Shared.Data.Entities;
    using TestingWASM.Shared.DTOs;
    using static TestingWASM.Services.FormEntryResponse.Types;
    using Newtonsoft.Json;


    /// <summary>
    /// Defines the <see cref="FormEntryService" />.
    /// </summary>
    public class FormEntryService : TestingWASM.Services.FormEntry.FormEntryBase
    {
        /// <summary>
        /// Defines the _mapper.
        /// </summary>
        private readonly IMapper _mapper;
        private readonly ILogger<FormEntryService> logger;
        private readonly string cacheKey = "FormTypeKey";
        private readonly string cacheKeyAssessQuestion = "FormQuestionKey";
        private readonly string holdingLocationsCacheKey = "HoldingLocations";

        private readonly string allFormTypeCacheKey = "AllFormTypes";

        /// <summary>
        /// Defines the _context.
        /// </summary>
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
            this.logger = logger;
            _context = context;
        }

        public override async Task<FormEntryResponse> GetForm(FormEntryRequest request, ServerCallContext context)
        {
            //todo add cache.
            var id = (int)request.FormTypeId;
            var frm = DataSeed.GenFormEntry();
            //frm.FormType = DataSeed.GenerateFormType();
            var frment = DataSeed.GenFormQuestionEntries();
            foreach (var item in frment)
            {
                frm.FormQuestionEntries.Add(item);
            }
            

            var formTitle =  frm.FormType;
       
            var form = new FormEntryResponse
            {
                Id = id,
                FormTitle = formTitle.Title,
                 FormDate =Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc))
        };
            try
            {
                
                var mapped = frm.FormQuestionEntries.Select(a => _mapper.Map<TestingWASM.Services.FormEntryResponse.Types.FormQuestionEntries>(a));

                form.FormQuestionEntries.AddRange(mapped.AsEnumerable());
             
              
                return form;
            }

            catch (Exception)
            {
                throw;
            }
        }

        

        //public async Task<FormEntryResponse> GetForm(FormEntryRequest request)
        //{
        //    //todo add cache.
        //    var id = (int)request.FormTypeId;
        //    var formTitle = await _context.FormTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        //    var form = new FormEntryResponse
        //    {
        //        Id = id,
        //        FormTitle = formTitle.Title,
        //    };
        //    try
        //    {
        //        var result = await GetQuestionsByFormType(id);
        //        var ques = result.Select(a => _mapper.Map<FormEntryResponse.Types.Questions>(a));
        //        form.Questions.AddRange(ques.ToArray());
        //        return form;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// The GetFormRelatedTypes.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{FormType}}"/>.</returns>
        public async Task<IEnumerable<FormType>> GetFormRelatedTypes()
#pragma warning restore CS1584 // XML comment has syntactically incorrect cref attribute
        {
            DateTime dt = DateTime.Now;
            //Func<DateTime, Task<List<FormType>>> actionThatWeWantToCache = async (x) => {
            //   var result =  await Task.Run(() => _context.FormTypes.AsNoTracking().Where(a => a.EndDate > x).ToListAsync());
            //    return result;
            //    };

            //var result = await actionThatWeWantToCache.Invoke(dt);
            //var formTypes = cache.GetOrAdd(CacheEntryExtensions.SetValue(this,result)
            //return Task.FromResult(formTypes.AsEnumerable());
            return await _context.FormTypes.AsNoTracking().Where(a => a.EndDate > dt).ToListAsync();



        }



        public async Task<IEnumerable<FormType>> GetAllFormTypes()
        {
            //Func<List<FormType>> actionThatWeWantToCache = () => _context.FormTypes.AsNoTracking().ToList();
            //var formTypes = cache.GetOrAdd(allFormTypeCacheKey, actionThatWeWantToCache);
            //return Task.FromResult(formTypes.AsEnumerable());
            return await _context.FormTypes.AsNoTracking().ToListAsync();
        }

        ///// <summary>
        ///// The GetClientForm.
        ///// </summary>
        ///// <param name="formTypeId">The formTypeId<see cref="int"/>.</param>
        ///// <param name="bookingNumber">The bookingNumber<see cref="int"/>.</param>
        ///// <returns>The <see cref="FormDTO"/>.</returns>
        //public FormDTO GetClientForm(int formTypeId, int bookingNumber)
        //{
        //    //extract from cache
        //    var formTitle = GetFormTypes().Where(x => x.Id == formTypeId).Select(x => x.Title).FirstOrDefault();
        //    var form = _context.ClientForms.FirstOrDefault(x => x.FormTypeID == formTypeId && x.BookingNumber == bookingNumber);

        //    if (form == null)
        //    {
        //        var result = GetForm(formTypeId);
        //        result.BookingNumber = bookingNumber;
        //        return result;
        //    }

        //    var dto = _mapper.Map<FormDTO>(form);
        //    dto.FormTitle = formTitle;
        //    dto.Questions = GetQuestionsForClientForm(form.Id);

        //    return dto;
        //}

        /// <summary>
        /// The GetQuestionsByFormType.
        /// </summary>
        /// <param name="formTypeId">The formTypeId<see cref="int"/>.</param>
        /// <returns>The <see cref="List{FormQuestionDTO}"/>.</returns>
        public async Task<FormQuestionEntries> GetQuestionsByFormType(int formTypeId)
        {

            var x = _context.FormQuestions.Where(x => x.FormTypeId == formTypeId).ToList();
            var qs = _context.FormEntries.First(x => x.FormTypeId == formTypeId).FormQuestionEntries;
            var x1 = qs.ToList();
            // qs.FormQuestionEntries
            //var questionQuery = qs.FormQuestionEntries.Select
            //    (n )
            //    .Select(n => new FormQuestionIntermediaryDTO
            //    {
            //        ID = ApplicationConstants.NewEntity,
            //        FormQuestionID = n.Id,
            //        Question = n.Question.Text,
            //        FormSection = n.FormSection,
            //        Required = n.Required.GetValueOrDefault(),
            //        Sequence = n.Sequence,
            //        EndDate = n.EndDate,
            //        TypeOfResponse = n.QuestionResponse.TypeOfResponse,
            //        DefaultResponse = n.DefaultResponse,
            //        QuestionResponseOptions = n.QuestionResponse.QuestionResponseOptions
            //            .Select(x => new ResponseOptionIntermediaryDTO
            //            {
            //                ShortDescription = x.ShortDescription,
            //                LongDescription = x.LongDescription,
            //                Sequence = x.SortOrder//,
            //                //FollowUpQuestionMaps = x.FollowUpQuestionMaps
            //                //    .Where(x => x.FormQuestionId == n.Id)
            //                //    .Select(q => new FormQuestionIntermediaryDTO
            //                //    {
            //                //        ID = ApplicationConstants.NewEntity,
            //                //        FormQuestionID = q.FollowUpQuestionId, //FormQuestionID,
            //                //        //FollowUpQuestionID = q.FollowUpQuestionID,
            //                //        Question = q.FollowUpQuestion.Prompt,
            //                //        Sequence = q.SortOrder,
            //                //        Required = q.Required,
            //                //        TypeOfResponse = q.FollowUpQuestion.QuestionResponse.TypeOfResponse,
            //                //        QuestionResponseOptions = q.FollowUpQuestion.QuestionResponse.QuestionResponseOptions
            //                //            .Select(o => new ResponseOptionIntermediaryDTO
            //                //            {
            //                //                ShortDescription = o.ShortDescription,
            //                //                LongDescription = o.LongDescription,
            //                //                Sequence = o.SortOrder
            //                //            })
            //                //    })
            //            })
            //    });

            ////var questionQuery = _context.FormQuestions.Where(x => x.FormType == formTypeId).OrderBy(x => x.Sequence).ProjectTo<FormQuestionFormQuestionDTOIntermediary>(_mapper.ConfigurationProvider);
            //var questions = await questionQuery.ToListAsync();

            //return _mapper.Map<List<FormQuestionDTO>>(questions);
            return null;
        }



        public async Task<List<FormType>> GetFormTypes()
        {
            //Func<List<FormType>> actionThatWeWantToCache = () => _context.FormTypes.AsNoTracking().ToList();

            //var formTypes = cache.GetOrAdd(cacheKey, actionThatWeWantToCache);

            // return formTypes;
            return await _context.FormTypes.AsNoTracking().ToListAsync();
        }

        ///// <summary>
        ///// The GetQuestionsForClientForm.
        ///// </summary>
        ///// <param name="clientFormId">The clientFormId<see cref="int"/>.</param>
        ///// <returns>The <see cref="List{FormQuestionDTO}"/>.</returns>
        //private List<FormQuestionDTO> GetQuestionsForClientForm(int clientFormId)
        //{
        //    var questionQuery = _context.ClientFormQuestions.Where(x => x.ClientFormID == clientFormId)
        //        .Select(n => new FormQuestionIntermediaryDTO
        //        {
        //            ID = n.Id,
        //            FormQuestionID = n.FormQuestionID,
        //            Question = n.FormQuestionFormQuestions.QuestionQuestions.Text,
        //            FormSection = n.FormQuestionFormQuestions.FormSection,
        //            Required = n.FormQuestionFormQuestions.Required,
        //            Sequence = n.FormQuestionFormQuestions.Sequence,
        //            TypeOfResponse = n.FormQuestionFormQuestions.ResponseQuestionResponse.TypeOfResponse,
        //            DefaultResponse = n.FormQuestionFormQuestions.DefaultResponse,
        //            ResponseValue = n.ResponseValue,
        //            QuestionResponseOptions = n.FormQuestionFormQuestions.ResponseQuestionResponse.QuestionResponseOptions
        //                                .Select(x => new ResponseOptionIntermediaryDTO
        //                                {
        //                                    ShortDescription = x.ShortDescription,
        //                                    LongDescription = x.LongDescription,
        //                                    Sequence = x.SortOrder,
        //                                    FollowUpQuestionMaps = x.FollowUpQuestionMaps
        //                                        .Where(x => x.FormQuestionID == n.FormQuestionID)
        //                                        .Select(q => new FormQuestionIntermediaryDTO
        //                                        {
        //                                            FormQuestionID = q.FollowUpQuestionID, //FormQuestionID,
        //                                            //FollowUpQuestionID = q.FollowUpQuestionID,
        //                                            Question = q.FollowUpQuestion.Prompt,
        //                                            Sequence = q.SortOrder,
        //                                            Required = q.Required,
        //                                            TypeOfResponse = q.FollowUpQuestion.QuestionResponse.TypeOfResponse,
        //                                            ResponseValue = q.FollowUpQuestion.ClientFormFollowUpQuestions.First(x => x.ClientFormQuestionID == n.Id).ResponseValue,
        //                                            ID = q.FollowUpQuestion.ClientFormFollowUpQuestions.First(x => x.ClientFormQuestionID == n.Id).ID,
        //                                            QuestionResponseOptions = q.FollowUpQuestion.QuestionResponse.QuestionResponseOptions
        //                                                .Select(o => new ResponseOptionIntermediaryDTO
        //                                                {
        //                                                    ShortDescription = o.ShortDescription,
        //                                                    LongDescription = o.LongDescription,
        //                                                    Sequence = o.SortOrder
        //                                                })
        //                                        })
        //                                })
        //        }) ;

        //    var questions = questionQuery.ToList().OrderBy(x => x.Sequence);

        //    return _mapper.Map<List<FormQuestionDTO>>(questions);
        //}

        //public async Task<CounterDTO> GetHousingCounts()
        //{
        //    var count = new CounterDTO();

        //    var ignore = from s in _context.ClientForms
        //                 where s.Status == "Complete" && s.FormTypeID == 6
        //                 select s.BookingNumber;

        //    var result = from c in _context.ClientForms
        //                 where c.Status == "Complete" && c.ClientFormQuestions.Any(a => a.FormQuestionID == 60 && a.ResponseValue == "Y")  && ignore.Contains(c.BookingNumber) == false
        //                 group c.BookingNumber by c.BookingNumber into g
        //                 where g.Count() >= 5
        //                 select g.Key;

        //    count.Count = await result.CountAsync<int>();
        //    count.Name = "Housing";

        //    return count;

        //}
    }
}