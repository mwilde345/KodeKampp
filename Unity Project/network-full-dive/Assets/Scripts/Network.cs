using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Network : MonoBehaviour {

    public GameObject port;
    public GameObject visualPacket;
    public float radius;

    List<GameObject> active;
    List<packet> queued;
    List<packet> logged;
    GameObject[] ports;
    float timer, timeStep = .05f;
    float resetTimer, resetTimeStep = 10f;
    string[] ips;

	void Start () {
        active = new List<GameObject>();
        logged = new List<packet>();
        ReadInPackets rip = new ReadInPackets( "Assets/PacketData/test.csv" );
        queued = rip.packetLst;
        initPorts();
        print( true );
    }

    void initPorts() {
        //Get total number of ports to be simulated;
        //Get an String array of ips
        ips = FilterData.allUniqueIPs( queued ).ToArray();
        if (ips.Length < 1) {
            print( "Problem with unique ips array" );
            return;
        }
        ports = new GameObject[ips.Length];
        Vector3[] portPoints = PortInit.initPortPoints( radius, ips.Length );
        for (int i = 0; i < portPoints.Length; i++) {
            GameObject portInstance = Instantiate( port );
            portInstance.GetComponent<Node>().ip = ips[i];
            portInstance.transform.position = portPoints[i];
            portInstance.transform.LookAt( GameObject.FindGameObjectWithTag( "Center" ).transform );
            portInstance.transform.rotation *= Quaternion.Euler( 270, 180, 0 );
            ports[i] = portInstance;
        }
    }

    //Null for unfound ip
    Transform getPortFromIP(string ip) {
        for(int i = 0; i < ips.Length; i++) {
            if (ips[i].Equals( ip )) return ports[i].transform;
        }
        return null;
    }
    
    void addPackets() {
        ReadInPackets rip = new ReadInPackets( "Assets/PacketData/test.csv" );
        queued.AddRange(rip.packetLst);
    }


    void Update() {
        if(!GameState.isPaused()) {
            timer += Time.deltaTime;
            if (timer >= timeStep) {
                timer = 0;
                if (queued.Count > 0) {
                    packet current = queued[0];
                    if (current != null) {
                        Transform src = getPortFromIP( current.ipSource );
                        Transform dst = getPortFromIP( current.ipDest );
                        if (src != null) {
                            GameObject packetInstance = Instantiate( visualPacket );
                            packetInstance.GetComponent<PacketAttributes>().init( current, src, dst );
                            active.Add( packetInstance );
                        }
                    }
                    logged.Add( current );
                    queued.Remove( current );
                }
            }
        }

        if (resetTimer >= resetTimeStep) {
            addPackets();
        } else resetTimer += Time.deltaTime;
    }
}