using System;

namespace PSS.Models
{
    public class BookingModel
    {
        public string FName { get; set; }

        public string LName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime DateTime { get; set; }

        public string SessionType { get; set; }

        public string IP { get; set; }

        public BookingModel()
        {
            DateTime = DateTime.Now.AddDays(7);
        }
    }
}