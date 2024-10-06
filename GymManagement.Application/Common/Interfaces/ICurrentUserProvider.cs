

using System.Threading.Tasks;
using GymMangement.Application.Common.Models;

namespace GymManagement.Application.Authentication.Common.interfaces;

public interface ICurrentUserProvider
{

    CurrentUser GetCurrentuser();

}
