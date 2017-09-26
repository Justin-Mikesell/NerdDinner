using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    
    public partial class Dinner
    {
        
        
        public bool IsValid => !GetRuleViolations().Any();

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (string.IsNullOrEmpty(Title))
            {
                yield return new RuleViolation("Title is required", "Title");
            }
            if (string.IsNullOrEmpty(Description))
            {
                yield return new RuleViolation("Description Required", "Description");
            }
            if (string.IsNullOrEmpty(Host))
            {
                yield return new RuleViolation("Host Required", "Host");
            }
            if (string.IsNullOrEmpty(Address))
            {
                yield return new RuleViolation("Address Required", "Address");
            }
            if (string.IsNullOrEmpty(Country))
            {
                yield return new RuleViolation("Country Required", "Country");
            }
            if (string.IsNullOrEmpty(ContactPhone))
            {
                yield return new RuleViolation("Phone# Required", "ContactPhone");
            }
            //if (!PhoneValidator.IsValidNumber(ContactPhone, Country))
            //{
            //    yield return new RuleViolation("Phone# does not match country", "ContactPhone");
            //}
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
            {
                throw new ApplicationException("Rule violations prevent saving");
            }
        }
    }

   

    public class RuleViolation
    {
        public string ErrorMessage { get; }
        public string PropertyName { get; }

        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }
}