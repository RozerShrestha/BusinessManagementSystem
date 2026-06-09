using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessManagementSystem.Helper
{
    public class RequiredIfAttribute:ValidationAttribute
    {
        public string PropertyName { get; set; }
        public Object Value { get; set; }

        public RequiredIfAttribute(string propertyName, object value, string errorMessage="")
        {
            PropertyName = propertyName;
            Value = value;
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();
            var propertyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            var mainPropertyvalue=type.GetProperty(validationContext.MemberName).GetValue(instance, null);
            if (propertyvalue.ToString().ToLower() == Value.ToString().ToLower())
            {
                return new ValidationResult(ErrorMessage); 
            }
            return ValidationResult.Success;
        }
    }
    public class RequiredIfValueMatchAttribute : ValidationAttribute
    {
        public string PropertyName { get; set; }
        public Object Value { get; set; }

        public RequiredIfValueMatchAttribute(string propertyName, object value, string errorMessage = "")
        {
            PropertyName = propertyName;
            Value = value;
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();
            var propertyvalue = type.GetProperty(PropertyName).GetValue(instance, null)==null?"": type.GetProperty(PropertyName).GetValue(instance, null);
            var tempMainValue = type.GetProperty(validationContext.MemberName).GetValue( instance, null)==null?"": type.GetProperty(validationContext.MemberName).GetValue(instance, null);
            string mainPropertyValue=tempMainValue==null?"":tempMainValue.ToString();

            if (propertyvalue.ToString().ToLower() == Value.ToString().ToLower())
            {
                
                if (string.IsNullOrEmpty(mainPropertyValue))
                {
                    return new ValidationResult(ErrorMessage);    
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
    public class RequiredIfHasValueAttribute : ValidationAttribute
    {
        public string PropertyToValidate { get; set; }
        public string PropertyToCompare { get; set; }
        public Object Value { get; set; }

        public RequiredIfHasValueAttribute(string propertyToValidate, string propertyToCompare, object value, string errorMessage = "")
        {
            PropertyToValidate = propertyToValidate;
            PropertyToCompare = propertyToCompare;
            Value = value;
            ErrorMessage = errorMessage;
        }
         
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(PropertyToValidate);
            var dependentValue = property.GetValue(validationContext.ObjectInstance, null);
            if (dependentValue != null && dependentValue as string != string.Empty)
            {
                if (value == null || value as string == string.Empty)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
    public class NotRequiredIfAttribute : ValidationAttribute
    {
        public string PropertyName { get; set; }
        public Object Value { get; set; }

        public NotRequiredIfAttribute(string propertyName, object value, string errorMessage = "")
        {
            PropertyName = propertyName;
            Value = value;
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();
            var propertyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (propertyvalue.ToString().ToLower() != Value.ToString().ToLower())
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
    public class InvalidIfAttribute : ValidationAttribute
    {
        public string PropertyName { get; set; }
        public Object Value { get; set; }

        public InvalidIfAttribute(string propertyName, object value, string errorMessage = "")
        {
            PropertyName = propertyName;
            Value = value;
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var instance = validationContext.ObjectInstance;
            var type = instance.GetType();
            var propertyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (propertyvalue.ToString() == Value.ToString())
            {
                if (value == null || value as string == string.Empty)
                {
                    return new ValidationResult(ErrorMessage);
                }

            }
            return ValidationResult.Success;
        }
    }
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null && file.Length > _maxFileSize)
            {
                return new ValidationResult($"Maximum allowed file size is {_maxFileSize / 1024 / 1024} MB.");
            }

            return ValidationResult.Success;
        }
    }
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedFileExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult($"This file type is not allowed. Allowed types: {string.Join(", ", _extensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class NoFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateOnly date)
            {
                if (date >= DateOnly.FromDateTime(DateTime.Today))
                {
                    return new ValidationResult("Date of Birth cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }

}
