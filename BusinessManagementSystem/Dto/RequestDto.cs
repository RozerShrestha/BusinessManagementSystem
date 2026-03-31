using BusinessManagementSystem.Utility;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BusinessManagementSystem.Dto
{
    public class RequestDto
    {
        private string? _startDateNep;
        private string? _endDateNep;

        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("startDateNep")]
        public string? StartDateNep
        {
            get => _startDateNep;
            set
            {
                _startDateNep = value;
                if (!string.IsNullOrEmpty(value))
                    StartDate = NepaliDateService.NepToEng(value);
            }
        }

        [JsonPropertyName("endDateNep")]
        public string? EndDateNep
        {
            get => _endDateNep;
            set
            {
                _endDateNep = value;
                if (!string.IsNullOrEmpty(value))
                    EndDate = NepaliDateService.NepToEng(value);
            }
        }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [DisplayName("Employee")]
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("settlement")]
        public string? Settlement { get; set; }
        public string? ParameterFilter { get; set; }
    }
    
}
