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

public class ReadInPacketsComma
{

    public List<packet> packetLst;
    public List<string> ipSourceLst; 
    public string fileLocation; 

    public ReadInPacketsComma(string fileLocation)
    {   // reads in a comma deliminated text file which contains the packet info we need. 
        this.fileLocation = fileLocation; 
        var reader = new StreamReader(File.OpenRead(fileLocation));
        List<packet> pktLst = new List<packet>();
        List<string> ipSceLst = new List<string>();

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            line = line.Replace('\"', ' ');
            line = line.Replace('"', ' ');
            line = line.Trim();
            


            string [] values = line.Split(',');

            if (!values[0].Contains("No."))  //Ignores header of txt file
            {
                string description = ""; 
                if (values.Length > 7)
                {
                    string[] descriptionArr = new string[(values.Length-6)];
                    Array.Copy(values, 6, descriptionArr, 0, descriptionArr.Length);
                    description = string.Join("", descriptionArr);
                }
                else
                {
                    description = values[6];
                }
                int num = Int32.Parse(values[0]);
                int len = Int32.Parse(values[5]);
                pktLst.Add(new packet(num, values[1], values[2], values[3], values[4], len, description));
                ipSceLst.Add(values[2]);
            }
        }      
        reader.Close();
        packetLst = pktLst;
        ipSourceLst = ipSceLst; 
    }   
}



