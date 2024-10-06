using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon.Constants
{
    public static partial class Constant
    {

        public static class Gym
        {
            public static readonly Guid Id = Guid.NewGuid();
            public const string Name = "GymName";
        }
    }
}
