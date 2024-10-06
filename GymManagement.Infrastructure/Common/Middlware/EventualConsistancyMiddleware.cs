using GymManagement.Domains.Common;
using GymManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Common.Middlware
{
    public class EventualConsistancyMiddleware(RequestDelegate next)
    {

        private readonly RequestDelegate _next = next;


        public async Task InvokeAsync(HttpContext context, IPublisher publisher , GymManagementDbContext dbContext)
        {

            var transaction = dbContext.Database.BeginTransaction();

            context.Response.OnCompleted(async () =>
            {

                try
                {

                    if (context.Items.TryGetValue("domainEventsQueue", out var value) &&
                    value is Queue<IDomainEvents> domainEventQueue)
                    {
                        while (domainEventQueue.TryDequeue(out var domainEvent))
                        {
                            await publisher.Publish(domainEvent);
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    await transaction.DisposeAsync();
                }

            });


            await _next(context);


        }




    }
}
