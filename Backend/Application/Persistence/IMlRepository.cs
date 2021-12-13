using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Persistence
{
    public interface IMlRepository
    {
        public Task<Dictionary<string, float>> PredictSentiment(string text);
    }
}