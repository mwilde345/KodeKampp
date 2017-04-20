using System;
using System.IO;
using System.Collections.Generic; 

public class packet
{
    public string protocol, ipSource, ipDest, info, time;   
    public int num, length;
   
    // A packet contains its number (in order read in), time read in after starting, source, Destination, protocol, length, and info
    // We might not need No or Time right now, but these values could be useful for future versions so i'm reading the info in just in case.
    public packet(int num, string time, string source, string dest, string protocol, int length, string info)
    {
        this.protocol = protocol;
        this.ipSource = source;
        this.ipDest = dest;
        this.info = info;
        this.num = num;
        this.length = length;
        this.time = time; 
    }
}

public class ReadInPackets
{

    public List<packet> packetLst;
    public List<string> ipSourceLst; 
    public string fileLocation; 

    public ReadInPackets(string fileLocation)
    {   // reads in a tab deliminated text file which contains the packet info we need. 
        this.fileLocation = fileLocation; 
        var reader = new StreamReader(File.OpenRead(fileLocation));
        List<packet> pktLst = new List<packet>();
        List<string> ipSceLst = new List<string>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var values = line.Split('\t');

            if (!values[0].Contains("No."))  //Ignores header of txt file
            {
                if (values.Length > 7)
                {
                    string[] description = new string[(values.Length-6)];
                    values.CopyTo(description,6);
                }
                int num = Int32.Parse(values[0]);
                int len = Int32.Parse(values[5]);
                pktLst.Add(new packet(num, values[1], values[2], values[3], values[4], len, values[6]));
                ipSceLst.Add(values[2]);
            }
        }
        reader.Close();
        packetLst = pktLst;
        ipSourceLst = ipSceLst; 
    }   
}

