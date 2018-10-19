using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSurvey.Models;

namespace SimpleSurvey.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        public SurveyContext ctx { get; set; }

        public SurveyController(SurveyContext context)
        {
            this.ctx = context;

        }

        [HttpGet("surveydata")]
        public IActionResult SurveyData()
        {
            var userId = (User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);

            var model = GetModel(userId);


            return Ok(model);

        }



        [HttpPost("surveyentry")]
        public IActionResult PostSurvey([FromBody] List<SurveyItem> model)
        {
            var userId = (User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);

            var items = model.Select(x => new SurveyResponse { SurveyId = x.Id, ResponseId = x.ResponseId.Value, ResponseDate = DateTime.Now, UserEmail = userId }).ToList();


             var entry = ctx.SurveyResponses.FirstOrDefault(x => x.UserEmail == userId);

            if(entry == null)
            {
                ctx.SurveyResponses.AddRange(items);
                ctx.SaveChanges();
            }


            var data = GetModel(userId);
            

            return Ok(data);

        }


        private SurveyModel GetModel(string userId)
        {
            var model = new SurveyModel();

            var items = ctx.SurveyEntries.Select(x => new SurveyItem { Id = x.Id, Question = x.SurveyQuestion }).ToList();

            var pagedItems = new List<List<SurveyItem>>();

            pagedItems.Add(items.OrderBy(x => x.Id).Take(5).ToList());
            pagedItems.Add(items.OrderBy(x => x.Id).Skip(5).Take(5).ToList());

            model.Items = pagedItems;

            var options = Enum.GetValues(typeof(SurveyOption));

            var list = new List<Option>();

            foreach (var option in options)
            {
                var entry = new Option { Id = ((int)option), OptionText = Util.GetEnumDescription((SurveyOption)option) };
                list.Add(entry);

            }

            model.Options = list;

            model.SurveyTaken = ctx.SurveyResponses.Any(x => x.UserEmail == userId);

            return model;
        }

        [HttpGet("surveyresults")]
        public IActionResult GetResults()
        {

            


            var items = ctx.SurveyResponses.GroupBy(x => new { x.SurveyId, x.Survey.SurveyQuestion })
    .Select(x => new
    {
        x.Key.SurveyId,
        x.Key.SurveyQuestion,
        SAG = x.Count(y => y.ResponseId == 1),
        AG = x.Count(y => y.ResponseId == 2),
        UND = x.Count(y => y.ResponseId == 3),
        DAG = x.Count(y => y.ResponseId == 4),
        SDAG = x.Count(y => y.ResponseId == 5)
    }).ToList();


            return Ok(items);

        }
    }
}
