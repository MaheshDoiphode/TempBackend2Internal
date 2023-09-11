using System;
using DomainProject.DomainModels;
using InfraProject.Context;

namespace InfraProject.Repositories
{
    public class VisitsRepository
    {
        private readonly VisitorManagementDbContext _context;

        public VisitsRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }

        public void CheckInVisitor(Visits visit)
        {
            visit.CheckIn = DateTime.Now;
            _context.Visits.Add(visit);
            _context.SaveChanges();
        }

        public void CheckOutVisitor(string visitId)
        {
            var visit = _context.Visits.Find(visitId);
            if (visit != null)
            {
                visit.CheckOut = DateTime.Now;
                visit.IsCompleted = true;
                _context.SaveChanges();
            }
        }
        public void AddVisit(Visits visit)
        {
            _context.Visits.Add(visit);
            _context.SaveChanges();
        }
        public IEnumerable<Visits> GetVisitsByDate(DateTime date)
        {
            return _context.Visits.Where(v => v.CheckIn.Date == date.Date).ToList();
        }
    }
}
