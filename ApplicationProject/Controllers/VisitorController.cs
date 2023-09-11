using Microsoft.AspNetCore.Mvc;
using InfraProject.Repositories;
using DomainProject.DomainModels;

namespace ApplicationProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorController : ControllerBase
    {
        private readonly VisitorRepository _visitorRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly VisitsRepository _visitsRepository;

        public VisitorController(VisitsRepository visitsRepository, VisitorRepository visitorRepository, NotificationRepository notificationRepository)
        {
            _visitsRepository = visitsRepository;
            _visitorRepository = visitorRepository;
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public IActionResult GetAllVisitors()
        {
            var visitors = _visitorRepository.GetAllVisitors();
            return Ok(visitors);
        }

        [HttpGet("{id}")]
        public IActionResult GetVisitorById(string id)
        {
            var visitor = _visitorRepository.GetVisitorById(id);
            if (visitor == null)
            {
                return NotFound();
            }
            return Ok(visitor);
        }

        [HttpPost]
        public IActionResult AddVisitor([FromBody] Visitor visitor)
        {
            _visitorRepository.AddVisitor(visitor);
            return CreatedAtAction(nameof(GetVisitorById), new { id = visitor.VisitorID }, visitor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVisitor(string id, [FromBody] Visitor visitor)
        {
            if (id != visitor.VisitorID)
            {
                return BadRequest();
            }

            _visitorRepository.UpdateVisitor(visitor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVisitor(string id)
        {
            _visitorRepository.DeleteVisitor(id);
            return NoContent();
        }

        [HttpPost("PreRegister")]
        public IActionResult PreRegisterVisitor([FromBody] Visitor visitor)
        {
            _visitorRepository.PreRegisterVisitor(visitor);
            _notificationRepository.AddNotification(visitor.HostEmail, "A visitor has pre-registered.");
            return CreatedAtAction(nameof(GetVisitorById), new { id = visitor.VisitorID }, visitor);
        }

        [HttpPost("OnSiteRegister")]
        public IActionResult OnSiteRegisterVisitor([FromBody] Visitor visitor)
        {
            _visitorRepository.OnSiteRegisterVisitor(visitor);
            _notificationRepository.AddNotification(visitor.HostEmail, "A visitor has registered on-site.");
            return CreatedAtAction(nameof(GetVisitorById), new { id = visitor.VisitorID }, visitor);
        }

        [HttpPost("AddToBlacklist/{visitorId}")]
        public IActionResult AddToBlacklist(string visitorId)
        {
            var visitor = _visitorRepository.GetVisitorById(visitorId);
            if (visitor != null)
            {
                _visitorRepository.AddToBlacklist(visitorId);
                _notificationRepository.AddNotification(visitor.HostEmail, "A visitor has been added to the blacklist.");
            }
            return Ok();
        }

        [HttpPost("RemoveFromBlacklist/{visitorId}")]
        public IActionResult RemoveFromBlacklist(string visitorId)
        {
            var visitor = _visitorRepository.GetVisitorById(visitorId);
            if (visitor != null)
            {
                _visitorRepository.RemoveFromBlacklist(visitorId);
                _notificationRepository.AddNotification(visitor.HostEmail, "A visitor has been removed from the blacklist.");
            }
            return Ok();
        }

        [HttpPost("CheckIn")]
        public IActionResult CheckInVisitor([FromBody] Visits visit)
        {
            _visitsRepository.CheckInVisitor(visit);
            _notificationRepository.AddNotification(visit.HostEmail, "A visitor has checked in.");
            return CreatedAtAction(nameof(CheckInVisitor), new { id = visit.VisitID }, visit);
        }

        [HttpPost("AddVisit")]
        public IActionResult AddVisit([FromBody] Visits visit)
        {
            _visitsRepository.AddVisit(visit);
            return CreatedAtAction(nameof(AddVisit), new { id = visit.VisitID }, visit);
        }
    }
}
