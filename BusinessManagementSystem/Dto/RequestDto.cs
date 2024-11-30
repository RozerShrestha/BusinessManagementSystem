using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace BusinessManagementSystem.Dto
{
    public class RequestDto
    {
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }

    }
}
