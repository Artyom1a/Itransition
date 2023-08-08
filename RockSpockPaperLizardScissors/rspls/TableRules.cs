using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

namespace rspls
{
    public class TableRules
    {
        string[] Throws;
        public TableRules(string[] throws)
        {
            Throws = throws;
        }

        public void OnDisplay()
        {
            var cell = Throws.Prepend("v PC \\ User >");
            var sheet = new ConsoleTable(cell.ToArray());

            var referee = new Referee(Throws.Length);

            for (int i = 0; i < Throws.Length; i++)
            {
                var oneRow = new string[Throws.Length + 1];
                oneRow[0] = Throws[i];

                for (int j = 0; j < Throws.Length; j++)
                {
                    oneRow[j + 1] = Enum.GetName(typeof(PartyResult), referee.Result(j, i));
                }

                sheet.AddRow(oneRow.ToArray());
            }
            sheet.Write(Format.Alternative);
        
        }
    }
}
