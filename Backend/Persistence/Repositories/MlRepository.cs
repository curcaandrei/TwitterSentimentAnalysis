using System;
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
            Console.WriteLine(result);
            Dictionary<string, float> map = new Dictionary<string, float>();
            if(result.Score.Length > 1)
            {
                
                map.Add("sad", result.Score[0]);
                map.Add("happy", result.Score[1]);
            }
            else
            {
                if(result.Prediction == 4)
                {
                    map.Add("sad", 0);
                    map.Add("happy", result.Score[0]);
                }
                else
                {
                    map.Add("sad", result.Score[0]);
                    map.Add("happy", 0);
                }
            }

            return await Task.FromResult(map);
        }
    }
}