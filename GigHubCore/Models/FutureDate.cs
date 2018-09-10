using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHubCore.Models
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;

            bool isValid = DateTime.TryParse(Convert.ToString(value),
                out dateTime);

            return (isValid && dateTime > DateTime.Now);
        }
    }
}
