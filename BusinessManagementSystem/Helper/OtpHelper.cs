using TattooAppointmentSystem.Data;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Helper
{
    public static class OtpHelper
    {
        public static async Task<int> GenerateOtpAsync(ApplicationDBContext dbContext, int userId, string email)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            // Generate a random 6-digit OTP
            var otpCode = new Random().Next(100000, 999999);

            // Create an OTP entity
            var otp = new OTP
            {
                UserId = userId,
                Email = email,
                OtpCode = otpCode,
                ExpiresAt = DateTime.Now.AddMinutes(5), // OTP expires in 5 minutes
                IsUsed = false
            };

            // Insert the OTP into the database
            await dbContext.OTPs.AddAsync(otp);
            await dbContext.SaveChangesAsync();

            return otpCode;
        }
    }
}

