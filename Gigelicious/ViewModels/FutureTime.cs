using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Gigelicious.ViewModels
{
    public class FutureTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            bool IsFormated = DateTime.TryParseExact(Convert.ToString(value),
                "HH:mm",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime
            );

            return (IsFormated && dateTime > DateTime.Now);


            
        }
    }
}