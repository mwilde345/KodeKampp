using UnityEngine;
using System.Collections;

public class PacketAttributes : MonoBehaviour {

    public Material red, blue, green, yellow, purple, pink;
    public AudioClip[] beeps;
    public float speed;
   
    Transform dst;
    packet packet;
    float timer;

    void playSound() { 
        int rand = (int) ( 6 * Random.value );
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = beeps[rand];
        audio.Play();
    }

    public void init(packet packet, Transform src, Transform dst) {
        this.packet = packet;
        this.dst = dst;

        transform.localScale *= (int.Parse( packet.length ) / 100);

        string protocol = packet.protocol;

        if(protocol.Trim().Contains("MDNS")) GetComponent<Renderer>().material = red;
        else if (protocol.Trim().Contains( "UDP" )) GetComponent<Renderer>().material = blue;
        else if (protocol.Trim().Contains( "BJNP" )) GetComponent<Renderer>().material = green;
        else if (protocol.Trim().Contains( "DHCP" )) GetComponent<Renderer>().material = yellow;
        else if (protocol.Trim().Contains( "SSDP" )) GetComponent<Renderer>().material = purple;
        else GetComponent<Renderer>().material = pink;

        gameObject.transform.position = src.position;

        playSound();
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Port") Terminate();
    }

    void Terminate() {
        if (timer >= 0.1f) Destroy( gameObject );
    }


	public void Update() {
        if(!GameState.isPaused()) {
            timer += Time.deltaTime;
            gameObject.transform.LookAt(dst);
            gameObject.transform.Translate(Vector3.forward * speed, Space.Self);
        } 
	}
    public packet getRaw() {
        return packet;
    }
}
