namespace BusinessManagementSystem.Dto
{
    public class MedicalInsuranceDto
    {
        public int Id { get; set; }
        public string INumber { get; set; }
        public string EmployeeName { get; set; }
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalClaim { get; set; }
        public int AmountPaid { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? SentToInsuranceDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string BillsFilePath { get; set; }
        public string ReportsFilePath { get; set; }
        public string DoctorPrescriptionsFilePath { get; set; }
        public string OthersFilePath { get; set; }
        public string InsurancePersonName { get; set; }
        public string InsurancePersonPhoneNumber { get; set; }
        public string RelationWithEmployee { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
    }
}
