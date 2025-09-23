using System.Collections.Generic;
using BootcampDay8.PokerGame.Enums;
using BootcampDay8.PokerGame.Core;
namespace BootcampDay8.PokerGame.Interfaces
{
    public interface IHandEvaluator
    {
        HandRank EvaluateHand(IReadOnlyList<Card> cards);
    }
}