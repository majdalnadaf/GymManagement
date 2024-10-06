using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Admins.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Events
{
    internal class DeleteSubscriptionEventHandler (
        IGymRepository gymReposoitry,
        IUnitOfWork unitOfWork)
        : INotificationHandler<DeleteSubscriptionEvent>
    {


        private readonly IGymRepository _gymRepository = gymReposoitry;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

 

        public async Task Handle(DeleteSubscriptionEvent notification, CancellationToken cancellationToken)
        {
            var gymsToDelete = await _gymRepository.ListBySubscriptionIdAsync(notification.subscriptionId);

            await _gymRepository.RemoveRangeAsync(gymsToDelete);
            await _unitOfWork.CommitChangesAsync();



        }
    }
}
