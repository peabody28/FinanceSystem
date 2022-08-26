using entities.Interfaces;
using System;

namespace entities
{
    public class ChainEntity : IChain
    {
        public Guid Id { get; set; }
    }
}
