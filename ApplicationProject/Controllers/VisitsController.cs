using Microsoft.AspNetCore.Mvc;
using InfraProject.Repositories;
using DomainProject.DomainModels;

namespace ApplicationProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitsController : ControllerBase
    {
        private readonly VisitsRepository _visitsRepository;

        public VisitsController(VisitsRepository visitsRepository)
        {
            _visitsRepository = visitsRepository;
        }

        [HttpPost("CheckIn")]
        public IActionResult CheckInVisitor([FromBody] Visits visit)
        {
            _visitsRepository.CheckInVisitor(visit);
            return CreatedAtAction(nameof(CheckInVisitor), new { id = visit.VisitID }, visit);
        }

        [HttpPost("CheckOut/{visitId}")]
        public IActionResult CheckOutVisitor(string visitId)
        {
            _visitsRepository.CheckOutVisitor(visitId);
            return Ok();
        }

        [HttpPost("AddVisit")]
        public IActionResult AddVisit([FromBody] Visits visit)
        {
            _visitsRepository.AddVisit(visit);
            return CreatedAtAction(nameof(AddVisit), new { id = visit.VisitID }, visit);
        }
    }
}
