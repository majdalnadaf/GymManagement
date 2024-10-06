using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domains.Common
{
    public  abstract class Entity
    {
        public Guid Id { get; init; }


        protected List<IDomainEvents> _domainEvents = new ();


        protected Entity(Guid id ) => Id = id;

        public List<IDomainEvents> PopDomainEvents()
        {
            var copy = new List<IDomainEvents>(_domainEvents);

            _domainEvents.Clear();

            return copy;
        }

        protected Entity() { }


    }
}
