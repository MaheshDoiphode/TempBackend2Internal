using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainProject.DomainModels
{
    public class Visits
    {
        public int VisitID { get; set; }
        public int VisitorID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string HostEmail { get; set; }
        public bool IsCompleted { get; set; }
        public string HostName { get; set; }
        public string Purpose { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public DateTime ExpectedDepart { get; set; }
        public int VisitDuration { get; set; }
    }

}
