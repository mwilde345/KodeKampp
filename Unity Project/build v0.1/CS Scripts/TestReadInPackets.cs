using System;

public class testReadInPackets
{
    static void Main(string[] args)
    {
        ReadInPacketsComma test = new ReadInPacketsComma(@"C:\Users\Sara\Documents\tst");

        for (int i = 0; i < 50; i++)
        {
            Console.WriteLine(test.packetLst[i].num+"\t"+test.packetLst[i].ipSource+"\n");
        }

    }
}

