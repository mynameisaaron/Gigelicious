using Gigelicious.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gigelicious.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [FutureDate]
        [Required]
        public string Date { get; set; }

        [FutureTime]
        [Required]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Heading { get; set; }

        public string Action {
            get
            {
                return (this.Id != 0) ? "Update" : "Create";
            }
        }

        

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));

        }
    }
}