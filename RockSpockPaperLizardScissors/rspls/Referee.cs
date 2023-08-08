using System;

namespace rspls
{
    public enum PartyResult
    {
        win=0,
        lose=1,
        draw=2
    }
    public class Referee
    {
        int AmountThrows;

        public Referee(int amountThrows)
        {
            AmountThrows = amountThrows;
        }

        public PartyResult Result(int firstthrow, int secondthrow)
        {
            if (firstthrow == secondthrow)
            {
                return PartyResult.draw;
            }

            if ((secondthrow > firstthrow && secondthrow - firstthrow <= AmountThrows / 2) || (secondthrow < firstthrow && firstthrow - secondthrow > AmountThrows / 2))
            {
                return PartyResult.win;
            }

            return PartyResult.lose;
        }
    }
}
