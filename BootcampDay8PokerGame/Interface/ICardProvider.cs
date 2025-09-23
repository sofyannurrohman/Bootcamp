// Interfaces/ICardProvider.cs
using System;
using BootcampDay8.PokerGame.Core;
namespace BootcampDay8.PokerGame.Interfaces
{
    public interface ICardProvider
    {
        Card DrawCard();
        void Shuffle();
        int CardsRemaining();
    }
}