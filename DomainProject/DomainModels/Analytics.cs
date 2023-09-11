using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainProject.DomainModels
{
    public class Analytics
    {
        public int ReportID { get; set; }
        public string Type { get; set; }
        public int GeneratedBy { get; set; }
        public DateTime GeneratedOn { get; set; }
    }

}
