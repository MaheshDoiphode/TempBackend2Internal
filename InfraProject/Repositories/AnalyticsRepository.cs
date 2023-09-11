using System;
using System.Linq;
using InfraProject.Context;
using System.Collections.Generic;

namespace InfraProject.Repositories
{
    public class AnalyticsRepository
    {
        private readonly VisitorManagementDbContext _context;

        public AnalyticsRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }

        public int GetTotalVisitors()
        {
            return _context.Visitors.Count();
        }

        public int GetVisitorsPerDay(DateTime date)
        {
            return _context.Visits.Count(v => v.CheckIn.Date == date.Date);
        }

        public double GetAverageVisitDuration()
        {
            return _context.Visits.Where(v => v.IsCompleted).Average(v => (v.CheckOut - v.CheckIn).TotalMinutes);
        }

        public Dictionary<string, int> GetVisitsPerHost()
        {
            return _context.Visits.GroupBy(v => v.HostEmail)
                                   .ToDictionary(g => g.Key, g => g.Count());
        }

        public int GetMonthlyVisitors(int month, int year)
        {
            return _context.Visits.Count(v => v.CheckIn.Month == month && v.CheckIn.Year == year);
        }

        public TimeSpan GetPeakVisitTime()
        {
            return _context.Visits.GroupBy(v => v.CheckIn.TimeOfDay)
                                   .OrderByDescending(g => g.Count())
                                   .FirstOrDefault()?.Key ?? TimeSpan.Zero;
        }

        public string GetMostFrequentVisitor()
        {
            return _context.Visits.GroupBy(v => v.VisitorID)
                                   .OrderByDescending(g => g.Count())
                                   .FirstOrDefault()?.Key;
        }

        public string GetMostActiveHost()
        {
            return _context.Visits.GroupBy(v => v.HostEmail)
                                   .OrderByDescending(g => g.Count())
                                   .FirstOrDefault()?.Key;
        }
        public Dictionary<string, int> GetAnalyticsByRole(string role)
        {
            return _context.Users.Where(u => u.RoleID == role)
                                 .GroupBy(u => u.RoleID)
                                 .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
