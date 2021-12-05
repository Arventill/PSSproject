using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace PSSCMS.Models.Cms
{
    public class CalendarModel
    {
        public List<ApprovedSessionModel> ApprovedSessionModel { get; set; }

        public List<PendingSessionModel> PendingSessionModel { get; set; }

        public List<CanceledSessionModel> CanceledSessionModel { get; set; }

        public List<OtherSessionModel> OtherSessionModel { get; set; }

        public CalendarModel()
        {
            ApprovedSessionModel = new List<ApprovedSessionModel>();
            PendingSessionModel = new List<PendingSessionModel>();
            CanceledSessionModel = new List<CanceledSessionModel>();
            OtherSessionModel = new List<OtherSessionModel>();
        }
    }

    public class ApprovedSessionModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public DateTime SessionDate { get; set; }

        public string PhoneNumber { get; set; }

        public int SessionTypeID { get; set; }

        public int SessionStatusID { get; set; }
    }

    public class PendingSessionModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public DateTime SessionDate { get; set; }

        public string PhoneNumber { get; set; }

        public int SessionTypeID { get; set; }

        public int SessionStatusID { get; set; }
    }

    public class CanceledSessionModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public DateTime SessionDate { get; set; }

        public string PhoneNumber { get; set; }

        public int SessionTypeID { get; set; }

        public int SessionStatusID { get; set; }
    }

    public class OtherSessionModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public DateTime SessionDate { get; set; }

        public string PhoneNumber { get; set; }

        public int SessionTypeID { get; set; }

        public int SessionStatusID { get; set; }
    }
}