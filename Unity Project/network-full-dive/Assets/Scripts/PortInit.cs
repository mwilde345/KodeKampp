using UnityEngine;
using System.Collections;

public class PortInit : MonoBehaviour {
    

    public static Vector3[] initPortPoints(float radius, int numberOfPorts) {
        Vector3[] portPoints = new Vector3[numberOfPorts];
        GameObject center = GameObject.FindGameObjectWithTag("Center");
        float n = numberOfPorts;
        float k = 1;
        float h = -1 + ( ( 2 * ( k - 1 ) ) / ( n - 1 ) );
        float theta = Mathf.Acos( h );
        float phi = 0;
        while(k <= n) {
            if (k == n) phi = 0;            
            float x = radius * Mathf.Sin( phi ) * Mathf.Cos( theta );
            float y = radius * Mathf.Sin( phi ) * Mathf.Sin( theta );
            float z = radius * Mathf.Cos( phi );
            portPoints[(int) k - 1] = new Vector3(x, y, z);
            k++;
            h = -1 + ( (2 * ( k - 1 )) / ( n - 1 ) );
            theta = Mathf.Acos( h );
            phi += (float)((3.6 / Mathf.Sqrt(n)) * (1 / Mathf.Sqrt(1 - (h*h)))) % (2 * Mathf.PI);
        }

        center.transform.position = new Vector3( 0, 0, 0 );
        center.transform.rotation = Quaternion.identity;

        return portPoints;
    }
    
}
