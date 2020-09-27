using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ADOvideotheek
{
    public class TekstValidatie : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string tekst = string.Empty;

            if (value == null || value.ToString() == string.Empty)
            {
                return new ValidationResult(false, "Waarde moet ingevuld zijn");
            }

            return ValidationResult.ValidResult;
        }
    }
}