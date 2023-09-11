using DomainProject.DomainModels;
using InfraProject.Context;
using System;

namespace InfraProject.Repositories
{
    public class NotificationRepository
    {
        private readonly VisitorManagementDbContext _context;

        public NotificationRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }

        public void AddNotification(string userId, string type)
        {
            string message = type switch
            {
                "CheckIn" => "A visitor has checked in.",
                "CheckOut" => "A visitor has checked out.",
                "PreRegister" => "A visitor has been pre-registered.",
                "OnSiteRegister" => "A visitor has been registered on-site.",
                "AddToBlacklist" => "A visitor has been added to the blacklist.",
                "RemoveFromBlacklist" => "A visitor has been removed from the blacklist.",
                _ => "Unknown notification type."
            };

            var notification = new Notification
            {
                NotificationID = Guid.NewGuid().ToString(),
                UserID = userId,
                Message = message,
                Time = DateTime.Now
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
        public IEnumerable<Notification> GetNotificationsByUser(string userId)
        {
            return _context.Notifications.Where(n => n.UserID == userId).ToList();
        }
    }
}
