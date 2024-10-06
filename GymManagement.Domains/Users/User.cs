using ErrorOr;
using GymManagement.Domains.Common;
using GymManagement.Domains.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GymManagement.Domains.Users
{
    public class User : Entity
    {

        public string FirstNamme { get; } = null;
        public string LastName { get; } = null;

        public string Email { get; } = null;

        public readonly string _passwordHash = null;

        public Guid? AdminId { get; private set; }

        public Guid? TrainerId { get; private set; }

        public Guid? ParticipantId { get; private set; }

        public User(
            string firstName,
            string lastName,
            string email,
            string passwordHash,    
            Guid? adminId = null,
            Guid? trainerId = null,
            Guid? participantId = null,
            Guid? id = null

            ) : base(id??Guid.NewGuid())
        {
            FirstNamme = firstName;
            LastName = lastName;
            Email = email;
            _passwordHash = passwordHash;
            AdminId = adminId;
            TrainerId = trainerId;
            ParticipantId = participantId;
           

        }

        public bool IsCorrectPasswordHash(string password , IPasswordHasher passwordHasher)
        {
            return passwordHasher.IsCorrectPassword(password, _passwordHash);
        }

        public ErrorOr<Guid> CreateAdminProfile()
        {
            if(AdminId is not null)
            {
                return Error.Conflict(description: "User has already and admin profile");
            }

            AdminId = Guid.NewGuid();
            return AdminId.Value;
        }

        public ErrorOr<Guid> CreateTrainerProfile()
        {
            if (TrainerId is not null)
            {
                return Error.Conflict(description: "User has already and trainer profile");
            }

            TrainerId = Guid.NewGuid();
            return TrainerId.Value;
        }


        public ErrorOr<Guid> CreateParticipantProfile()
        {
            if (ParticipantId is not null)
            {
                return Error.Conflict(description: "User has already and participant profile");
            }

            ParticipantId = Guid.NewGuid();
            return ParticipantId.Value;
        }

        private User()
        {

        }

    }
}
