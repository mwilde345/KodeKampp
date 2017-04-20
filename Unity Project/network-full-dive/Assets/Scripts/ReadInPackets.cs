using System;
using System.IO;
using System.Collections.Generic;
using System.Net;


public class packet
{
    public string protocol, ipSource, ipDest, time, length;   

   
    // A packet contains its number (in order read in), time read in after starting, source, Destination, protocol, length, and info
    // We might not need No or Time right now, but these values could be useful for future versions so i'm reading the info in just in case.
    public packet(string time, string source, string dest, string protocol, string length)
    {
        this.protocol = protocol;
        this.ipSource = source;
        this.ipDest = dest;                  
        this.time = time;
        this.length = length; 
    }
}

public class ReadInPackets
{

    public List<packet> packetLst;
    public List<string> ipSourceLst; 
    public string fileLocation; 

    public ReadInPackets(string fileLocation)
    {   // reads in a comma deliminated text file which contains the packet info we need. 
      
        this.fileLocation = fileLocation; 
        var reader = new StreamReader(File.OpenRead(fileLocation));
        List<packet> pktLst = new List<packet>();       
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            if (!values[1].Equals(""))
            {
                pktLst.Add(new packet(values[0], values[1], values[2], values[3], values[4]));
            }         
           
        }      
        reader.Close();
        packetLst = pktLst;
    }   

    static void readInFiles()
    {

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://192.168.1.1/");
        request.Method = WebRequestMethods.Ftp.ListDirectory;

        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        Stream responseStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(responseStream);
        List<string> directories = new List<string>();

        string line = reader.ReadLine();
        while (!string.IsNullOrEmpty(line))
        {
            directories.Add(line);
            line = reader.ReadLine();
        }
        reader.Close();

        using (WebClient ftpClient = new WebClient())
        {
            for (int i = 0; i < directories.Count; i++)
            {
                if (directories[i].Contains("hello"))
                {
                    string path = "ftp://192.168.1.1/" + directories[i].ToString();
                    string transfer = @"C:\Users\Sara\Documents\Visual Studio 2015\Projects\CodeCamp\CodeCamp\Assets\PacketData\" + directories[i].ToString();
                    ftpClient.DownloadFile(path, transfer);
                }
            }
        }

        response.Close();

        for (int i = 0; i < directories.Count; i++)
        {
            if (directories[i].Contains("hello"))
            {
                ReadInPackets rip = new ReadInPackets(@"C:\Users\Sara\Documents\Visual Studio 2015\Projects\CodeCamp\CodeCamp\Assets\PacketData\" + directories[i].ToString());
                List<packet> pktLst = rip.packetLst;             
            }
        }



    }
}



