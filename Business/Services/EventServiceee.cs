using Business.BusinessObjects;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class EventServiceee
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEstablishmentRepository _establishmentRepository;

        private const int EarthRadius = 6371;
        public EventServiceee(IEventRepository eventRepository, IEstablishmentRepository establishmentRepository)
        {
            _eventRepository = eventRepository;
            _establishmentRepository = establishmentRepository;
        }
        public ItemBO GetEventDetail(int id)
        {
            var dbEventDetail = _eventRepository.GetEventDetail(id, null);
            var eventDetail = new ItemBO() { Id = 123 };
            return eventDetail;
        }
    }
}
