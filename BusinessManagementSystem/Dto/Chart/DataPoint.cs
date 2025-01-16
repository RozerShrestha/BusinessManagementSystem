using System.Runtime.Serialization;

namespace BusinessManagementSystem.Dto.Chart
{
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class IncomeSegregationDataPoint
    {
        public IncomeSegregationDataPoint(string label, double y)
        {
            this.Label = label;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }

    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class PaymentTipsDataPoint
    {
        public PaymentTipsDataPoint(string label, double y)
        {
            this.Label = label;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }
}
