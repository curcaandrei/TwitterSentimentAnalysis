using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using static Domain.TweetML;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictMLController : Controller
    {
        [HttpPost]
        public Dictionary<string, float> Analysis(Tweet tweet)
        {
            ModelInput modelInput = new ModelInput();
            modelInput.Text = tweet.Text;
            var result = TweetML.Predict(modelInput);
            Dictionary<string, float> map = new Dictionary<string, float>();
            map.Add("sad", result.Score[0]);
            map.Add("happy",result.Score[1]);
            return map;
        }
    }
}
