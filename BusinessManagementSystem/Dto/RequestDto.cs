using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
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
        [DisplayName("Employee")]
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("settlement")]
        public string Settlement { get; set; }
        public string ParameterFilter { get; set; }

    }
}
