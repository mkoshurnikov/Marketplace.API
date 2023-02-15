using System.Text.Json;

namespace Marketplace.API.ResourceModels
{
    public class ErrorInfo
    {
        public int StatusCode { get; set; }
        public DateTime OccurredAt { get; set; }
        public string ErrorMessage { get; set; }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
