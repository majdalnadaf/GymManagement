using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Admins;
using GymManagement.Domains.Common;
using GymManagement.Domains.Gyms;
using GymManagement.Domains.Rooms;
using GymManagement.Domains.Subscriptions;
using GymManagement.Domains.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Common.Persistence
{
    public class GymManagementDbContext
        (DbContextOptions options, IHttpContextAccessor httpContextAccessor,IPublisher publisher)
        : DbContext(options) ,IUnitOfWork 
    {

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private readonly IPublisher _publisher = publisher;

        public virtual DbSet<Subscription> Subscriptions { get; set; }  
        public virtual DbSet<Gym> Gyms{ get; set; }  
        public virtual DbSet<User> Users{ get; set; }  
     
        public virtual DbSet<Admin> Admins { get; set; }  
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


        public async Task CommitChangesAsync()
        {

            // Get hold of all the domains events

            var domainEvents = ChangeTracker.Entries<Entity>()
                      .Select(entry => entry.Entity.PopDomainEvents())
                      .SelectMany(x => x) 
                      .ToList();


            if (IsUserWaitingOnline())
            {
                AddDomainEventsToOfflineProccessingQueue(domainEvents);
            }
            else
            {
                await PublishDomainEvents(domainEvents);
            }


            await base.SaveChangesAsync();
        }


        private async Task PublishDomainEvents(List<IDomainEvents> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }

        private bool IsUserWaitingOnline() => _httpContextAccessor is not null;

        private void AddDomainEventsToOfflineProccessingQueue(List<IDomainEvents> domainEvents)
        {
            // Check if there are exists events

            var domainEventQueue = _httpContextAccessor.HttpContext.Items.TryGetValue("domainEventsQueue", out var value)
                && value is Queue<IDomainEvents> existsDomainEvents
                ?  existsDomainEvents
                : new Queue<IDomainEvents>();



            foreach (var domainEvent in domainEvents)
            {
                domainEventQueue.Enqueue(domainEvent);
            }


            _httpContextAccessor.HttpContext.Items["domainEventsQueue"] = domainEventQueue;
        }
    }
}
