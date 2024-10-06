using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domains.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.AddTrainer
{
    public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
    {
        private readonly IGymRepository _gymRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTrainerCommandHandler(IGymRepository gymRepository, IUnitOfWork unitOfWork)
        {
            _gymRepository = gymRepository;
            _unitOfWork = unitOfWork;   
        }

        public async Task<ErrorOr<Success>> Handle(AddTrainerCommand command, CancellationToken cancellationToken)
        {
            Gym? gym =  await _gymRepository.GetByIdAsync(command.gymId);

            if(gym is { })
            {
                return Error.NotFound(description: "Gym not found");
            }


            var addTrainerResult = gym.AddTrainer(command.trainerId);
            if (addTrainerResult.IsError)
            {
                return addTrainerResult.Errors;
            }

            await _gymRepository.UpdateGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return Result.Success;
        }
    }
}
