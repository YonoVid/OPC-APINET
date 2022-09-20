using InternationalAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace InternationalAPI.Controllers
{
    public class PostController : Controller
    {
        private readonly IStringLocalizer<PostController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResources> _sharedResourcesLocalizer;

        public PostController(IStringLocalizer<PostController> stringLocalizer, IStringLocalizer<SharedResources> sharedResourcesLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _sharedResourcesLocalizer = sharedResourcesLocalizer;
        }

        [HttpGet]
        [Route("PostControllerResource")]
        public IActionResult GetUsingPostControllerResource()
        {
            //Find text
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("Welcome").Value ?? String.Empty;
            
            return Ok(new 
            { 
                PostType = article.Value,
                PostName = postName
            });
        }

        [HttpGet]
        [Route("SharedResource")]
        public IActionResult GetUsingSharedResource()
        {
            //Find text
            var article = _stringLocalizer["Article"];
            var postName = _stringLocalizer.GetString("Welcome").Value ?? String.Empty;
            var todayIs = String.Format(_sharedResourcesLocalizer.GetString("DateFormat"), DateTime.Now.ToLongDateString());

            return Ok(new
            {
                PostType = article.Value,
                PostName = postName,
                TodayIs = todayIs
            });
        }
    }
}
