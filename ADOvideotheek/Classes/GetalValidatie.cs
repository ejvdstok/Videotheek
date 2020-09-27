using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ADOvideotheek;

namespace ADOvideotheek
{
    public class GetalValidatie : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int getal = 0;
            decimal prijs = 0;

            if (value.ToString() == string.Empty)
            {
                return new ValidationResult(false, "Getal moet ingevuld zijn");
            }
            if (!int.TryParse(value.ToString(), out getal))
            {
                if (!decimal.TryParse(value.ToString(), NumberStyles.Currency, cultureInfo, out prijs))
                {
                    return new ValidationResult(false, "Waarde moet een getal zijn");
                }
            }

            if (getal <= 0 && prijs <= 0)
            {
                return new ValidationResult(false, "Waarde moet groter zijn dan 0");
            }

            return ValidationResult.ValidResult;
        }
    }
}