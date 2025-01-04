using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Utility
{
    public static class SD
    {
        public const string Role_Superadmin = "superadmin";

        public const string Role_TattooAdmin = "admin_tattoo";
        public const string Role_KaffeAdmin = "admin_kaffe";
        public const string Role_ApartmentAdmin = "admin_apartment";

        public const string Role_TattooEmployee = "employee_tattoo";
        public const string Role_KaffeEmployee = "employee_kaffe";
        public const string Role_ApartmentEmployee = "employee_apartment";

        public const string Gender_Male = "Male";
        public const string Gender_Female = "Female";
        public const string Status_Draft = "Draft";
        public const string Status_Submitted = "Submitted";
        public const string Status_Claim_Acknowledge = "Claim Acknowledged";
        public const string Status_Returned = "Returned";
        public const string Status_Resubmitted = "ReSubmitted";
        public const string Status_Approved = "Approved";
        public const string Status_Rejected = "Rejected";
        public const string Status_Reimbursed = "Reimbursed";

        public static readonly Dictionary<string, string> Occupations = new Dictionary<string, string>
        {
            { "Select Occupation", "Select Occupation" },
            { Occupation.TattooArtist.ToString(), "Tattoo Artist" },
            { Occupation.Barista.ToString(), "Barista" },
            { Occupation.Manager.ToString(), "Manager" },
            { Occupation.ChiefExecutiveOfficer.ToString(), "Chief Executive Officer" },
            { Occupation.Cleaner.ToString(), "Cleaner" },
            { Occupation.ChiefOperatingOfficer.ToString(), "Chief Operating Officer" },
            { Occupation.ChiefFinanceOfficer.ToString(), "Chief Finance Officer" }
        };

        public static readonly Dictionary<string, string> TattooCategories = new Dictionary<string, string>
        {
            { TattooCategory.Tattoo.ToString(), "Tattoo" },
            { TattooCategory.Dreadlock.ToString(), "DreadLock" },
            { TattooCategory.Piercing.ToString(), "Piercing" }
        };

        public static readonly Dictionary<string, string> ApointmentStatus = new Dictionary<string, string>
        {
            {AppointmentStat.All.ToString(),"All" },
            { AppointmentStat.InProgress.ToString(), "In Progress" },
            { AppointmentStat.Scheduled.ToString(), "Scheduled" },
            { AppointmentStat.Rescheduled.ToString(), "Re Scheduled" },
            { AppointmentStat.Confirmed.ToString(), "Confirmed" },
            { AppointmentStat.Completed.ToString(), "Completed" },
            { AppointmentStat.Cancelled.ToString(), "Cancelled" },
            { AppointmentStat.NotShown.ToString(), "Not Shown" }
            
        };

        public static readonly Dictionary<string, string> PaymentMethods = new Dictionary<string, string>
        {
            { PaymentMethod.BankQR.ToString(), "Bank QR" },
            { PaymentMethod.CardPayment.ToString(), "Card Payment" },
            { PaymentMethod.Cash.ToString(), "Cash" },
            { PaymentMethod.Esewa.ToString(), "ESewa" },
            { PaymentMethod.ImePay.ToString(), "ImePay" },
            { PaymentMethod.Khalti.ToString(), "Khalti" }
        };

        public static readonly Dictionary<string, string> OutletList = new Dictionary<string, string>()
        {
            {"Freak Street", "Freak Street" },
            {"Babar Mahal","Babar Mahal" }
        };


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
