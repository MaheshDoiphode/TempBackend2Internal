using System.Collections.Generic;
using System.Linq;
using DomainProject.DomainModels;
using InfraProject.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraProject.Repositories
{
    public class VisitorRepository
    {
        private readonly VisitorManagementDbContext _context;

        public VisitorRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Visitor> GetAllVisitors()
        {
            return _context.Visitors.ToList();
        }

        public Visitor GetVisitorById(string id)
        {
            return _context.Visitors.Find(id);
        }

        public void AddVisitor(Visitor visitor)
        {
            _context.Visitors.Add(visitor);
            _context.SaveChanges();
        }

        public void UpdateVisitor(Visitor visitor)
        {
            _context.Entry(visitor).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteVisitor(string id)
        {
            var visitor = _context.Visitors.Find(id);
            if (visitor != null)
            {
                _context.Visitors.Remove(visitor);
                _context.SaveChanges();
            }
        }
        public void PreRegisterVisitor(Visitor visitor)
        {
            visitor.IsPreRegistered = true;
            _context.Visitors.Add(visitor);
            _context.SaveChanges();
        }

        public void OnSiteRegisterVisitor(Visitor visitor)
        {
            visitor.IsOnSiteRegistered = true;
            _context.Visitors.Add(visitor);
            _context.SaveChanges();
        }
        public void AddToBlacklist(string visitorId)
        {
            var visitor = _context.Visitors.Find(visitorId);
            if (visitor != null)
            {
                visitor.IsBlacklisted = true;
                _context.SaveChanges();
            }
        }
        public void RemoveFromBlacklist(string visitorId)
        {
            var visitor = _context.Visitors.Find(visitorId);
            if (visitor != null)
            {
                visitor.IsBlacklisted = false;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Visitor> GetVisitorsByHost(string hostEmail)
        {
            return _context.Visitors.Where(v => v.HostEmail == hostEmail).ToList();
        }


    }
}
