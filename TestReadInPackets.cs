using System;

using System.IO;

using System.Collections.Generic;

public class testReadInPackets
{

    static void Main(string[] args)
    {
        ReadInPackets test = new ReadInPackets(@"C:\Users\Sara\Documents\WiresharkData.txt");

        for (int i = 0; i < 50; i++)
        {
            Console.WriteLine(test.packetLst[i].ipSource+"\t\t\t"+test.packetLst[i].length);
        }

    }


}

