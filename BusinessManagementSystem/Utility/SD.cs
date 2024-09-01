namespace BusinessManagementSystem.Utility
{
    public static class SD
    {
        public const string Role_HR = "hradmin";
        public const string Role_Admin = "admin";
        public const string Role_Normal = "employee";
        public const string Gender_Male = "Male";
        public const string Gender_Female = "Female";
        public const string Status_Draft = "Draft";
        public const string Status_Submitted = "Submitted";
        public const string Status_Claim_Acknowledge = "Claim Acknowledged";
        public const string Status_InsuranceSent = "Request sent to Insurance";
        public const string Status_Returned = "Returned";
        public const string Status_InsuranceReturned = "Returned By Insurance";
        public const string Status_Resubmitted = "ReSubmitted";
        public const string Status_Approved = "Approved";
        public const string Status_Rejected = "Rejected";
        public const string Status_Reimbursed = "Reimbursed";
    }

    public static class DocumentType
    {
        public const string Reports = "Reports";
        public const string Bills = "Bills";
        public const string DocPrescriptions = "DocPrescriptions";
        public const string OtherDocuments = "OtherDocuments";
        public const string InsuranceExcelUpload = "InsuranceExcelUpload";
    }
}
