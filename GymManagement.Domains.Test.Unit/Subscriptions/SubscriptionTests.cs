using ErrorOr;
using FluentAssertions;
using GymManagement.Domains.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Gyms;
using TestCommon.Subscriptions;



namespace GymManagement.Domains.Test.Unit.Subscriptions
{
    public class SubscriptionTests
    {
        [Fact]
        public void AddGym_ShouldFail_WhenMoreThanSubscripionAllows()
        {
            //Arrange
            var subscription = SubscriptionFactory.CreateSubscription();

            var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
                .Select(_=> GymFactory.CreateGym(id:Guid.NewGuid()))
                .ToList();

            //Act
           var addGymResults  = gyms.ConvertAll(gym => subscription.AddGym(gym));

            //Assert
            var allButLastGymResult = addGymResults[..^1];
            allButLastGymResult.Should().AllSatisfy(addGymResult => addGymResult.Value.Should().Be(Result.Success));
            
            var lastAddGymResult = addGymResults.Last();
            lastAddGymResult.IsError.Should().BeTrue();
            lastAddGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows);



        }


    }
}
