using UnityEngine;
using System.Collections;

public class Menu : VRGUI {

    public Texture dot;

    public override void OnVRGUI() {
        if(GameState.isPaused()) {
            GUILayout.BeginArea( new Rect( Screen.width / 2, Screen.height / 2, 100f, 20f ) );
            GUILayout.TextField( "Paused" );
            GUILayout.EndArea();
        }else {
            GUI.DrawTexture( new Rect( Screen.width / 2, Screen.height / 2, 16f, 16f ), dot);
            GUILayout.BeginArea( new Rect( Screen.width / 2 - 150f, Screen.height / 2 + 32f,  300f, 300f ) );
            GUILayout.TextField( GetComponent<PlayerLookingAt>().currentIP );
            GUILayout.EndArea();

        }
       
    }

}
