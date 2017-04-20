using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class FilterData
{

    

    static List<string> uniqueIpSrcStrings(List<packet> pktLst)
    {
        List<string> uniqueIPList = new List<string>();
        while (pktLst.Count != 0)
        {
            packet p = pktLst.First();
            
            pktLst = pktLst.Where(x => !x.ipSource.Trim().Contains(p.ipSource.Trim())).ToList();
            uniqueIPList.Add(p.ipSource);
        }
        return uniqueIPList;
    }

    static List<string> uniqueIpDestStrings(List<packet> pktLst)
    {
        List<string> uniqueIPList = new List<string>();
        while (pktLst.Count != 0)
        {
            packet p = pktLst.First();
            
            pktLst = pktLst.Where(x => !x.ipDest.Trim().Contains(p.ipDest.Trim())).ToList();
            uniqueIPList.Add(p.ipDest);
        }
        return uniqueIPList;
    }

    public static List<string> allUniqueIPs(List<packet> pktLst)
    {
        List<string> srcIps = uniqueIpSrcStrings(pktLst);
        List<string> destIps = uniqueIpDestStrings(pktLst);
        srcIps.AddRange(destIps);
        return srcIps;


    }


    public static List<packet> findOccurances(string uniqueIP, List<packet> pktLst)
    {
        pktLst = pktLst.Where(x => x.ipSource.Trim().Contains(uniqueIP.Trim())).ToList();
        return pktLst;
    }


    public static List<string> uniqueProtocols(List<packet> pktLst)
    {
        List<string> protocolLst = new List<string>();
        while (pktLst.Count != 0)
        {
            packet p = pktLst.First();

            pktLst = pktLst.Where(x => !x.protocol.Trim().Equals(p.protocol.Trim())).ToList();
            protocolLst.Add(p.protocol);
        }
        return protocolLst; 

    }

}
