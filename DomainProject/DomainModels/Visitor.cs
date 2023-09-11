using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainProject.DomainModels
{
    public class Visitor
    {
        public int VisitorID { get; set; }
        public bool IsPreRegistered { get; set; }
        public bool IsOnSiteRegistered { get; set; }
        public string Title { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Add { get; set; }
        public string IdentityType { get; set; }
        public string IdNum { get; set; }
        public byte[] Image { get; set; }
        public bool IsBlacklisted { get; set; }
    }

}
