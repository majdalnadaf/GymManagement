using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository,
            IUnitOfWork unitOfWork,
            IAdminRepository adminRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(command.AdminId);
            if(admin is null)
            {
                return Error.NotFound(description: "Admin not found");
            }

            var subscription = new Subscription(

                subscriptionType:command.SubscriptionType ,
                adminId: command.AdminId

                );

            if(admin.SubscriptionId is not null)
            {
                return Error.Conflict(description: "Admin already has an active subscription");
            }

            admin.SetSubscription(subscription);

            await _subscriptionRepository.AddSubscriptionAsync(subscription);
            await _adminRepository.UpdateAdminAsync(admin);
            await _unitOfWork.CommitChangesAsync();

            return subscription;
        }
    }
}
