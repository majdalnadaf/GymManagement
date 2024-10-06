using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptonCommand, ErrorOr<Deleted>>
    {

        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IGymRepository _gymRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository,
            IAdminRepository adminRepository,
            IGymRepository gymRepository,
            IUnitOfWork unitOfWork

            )
        {
            _subscriptionRepository = subscriptionRepository;
            _adminRepository = adminRepository;
            _gymRepository = gymRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptonCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(command.subscriptionId);

            if (subscription is null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            var admin = await _adminRepository.GetByIdAsync(subscription._adminId);
            if (admin is null)
            {
                return Error.Unexpected(description: "Admin not found");
            }

            admin.DeleteSubscription(command.subscriptionId);

            await _adminRepository.UpdateAdminAsync(admin);
            await _unitOfWork.CommitChangesAsync();



            return Result.Deleted;
        }
    }
}
