using System;
using Events.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Events.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> Logger;
        private readonly EventRepository EventRepository;
        public EventsController(ILogger<EventsController> logger, EventRepository eventRepository)
        {
            Logger = logger;
            this.EventRepository = eventRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var events = this.EventRepository.GetAll();
            return this.Ok(events);
        }

        [HttpGet("{id}")]
        public IActionResult GetEvent(Guid id)
        {
            var e = this.EventRepository.Get(id);
            return this.Ok(e);
        }
    }
}
