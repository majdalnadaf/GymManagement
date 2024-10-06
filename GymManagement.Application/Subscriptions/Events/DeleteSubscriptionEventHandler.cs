using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Admins.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Events
{
    public class DeleteSubscriptionEventHandler : INotificationHandler<DeleteSubscriptionEvent>
    {


        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubscriptionEventHandler(IUnitOfWork unitOfWork, ISubscriptionRepository subscriptionRepository)
        {
            _unitOfWork = unitOfWork;
            _subscriptionRepository = subscriptionRepository;   
        }

        public async Task Handle(DeleteSubscriptionEvent notification, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(notification.subscriptionId)
                ?? throw new InvalidOperationException();

            await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
            await _unitOfWork.CommitChangesAsync();

        }
    }
}
