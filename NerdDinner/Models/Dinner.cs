using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public partial class Dinner
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Title))
            {
                yield return new RuleViolation("Title required", "Title");
            }
            if (String.IsNullOrEmpty(Description))
            {
                yield return new RuleViolation("Description Required", "Description");
            }
            if (String.IsNullOrEmpty(Host))
            {
                yield return new RuleViolation("Host Required", "Host");
            }
            if (String.IsNullOrEmpty(Address))
            {
                yield return new RuleViolation("Address Required", "Address");
            }
            if (String.IsNullOrEmpty(Country))
            {
                yield return new RuleViolation("Country Required", "Country");
            }
            if (String.IsNullOrEmpty(ContactPhone))
            {
                yield return new RuleViolation("Phone# Required", "ContactPhone");
            }
            if (!PhoneValidator.IsValidNumber(ContactPhone, Country))
            {
                yield return new RuleViolation("Phone# does not match country", "ContactPhone");
            }

            yield break;
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
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }

        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }
}