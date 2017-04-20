using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    public GameObject cat;
    static bool pause = true;
    static bool cats = false;
    static float timer, reset = 0.18f;
    static float timer2, reset2 = 0.18f;

    void Update() {
        
        if (timer >= reset) {
            if (Input.GetButton( "Start" )) {
                timer = 0f;
                pause = !pause;
            }
        }
        else timer += Time.deltaTime;

        if (timer2 >= reset2) {
            if (Input.GetButton( "Select" )) {
                timer2 = 0f;
                cats = !cats;
                print( cats );
                cat.transform.GetChild( 6 ).gameObject.SetActive( cat );
            }
        } else timer2 += Time.deltaTime;
    }
	
    public static bool isPaused() {
        return pause;
    }
}
