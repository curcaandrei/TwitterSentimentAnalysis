using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Persistence;
using Domain;

namespace Persistence.Repositories
{
    public class MlRepository : IMlRepository
    {
        public async Task<Dictionary<string, float>> PredictSentiment(string text)
        {
            TweetML.ModelInput modelInput = new TweetML.ModelInput();
            modelInput.Text = text;
            var result = TweetML.Predict(modelInput);
            Dictionary<string, float> map = new Dictionary<string, float>();
            map.Add("sad", result.Score[0]);
            map.Add("happy",result.Score[1]);
            return await Task.FromResult(map);
        }
    }
}