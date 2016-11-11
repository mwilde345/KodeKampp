using UnityEngine;
using System.Collections;

public class PortInit : MonoBehaviour {

    //Make a center node that is not the camera.
    public GameObject center;
    public GameObject port;
    public float radius;
    public int numberOfPorts;
    

	void Start () {
        //Get total number of ports to be simulated;
        if (numberOfPorts < 1) return;
        Vector3[] portPoints = initPortPoints( numberOfPorts );
        for (int i = 0; i < portPoints.Length; i++) {
            GameObject portInstance = Instantiate(port);
            portInstance.transform.position = portPoints[i];
            portInstance.transform.LookAt(center.transform);
            portInstance.transform.rotation *= Quaternion.Euler(90, 0, 0);
        }
        
       // for (int i = 0; i < numberOfPorts; i++) {
       //     portInstance.transform.eulerAngles = verticalSides;
       // }
	}

    Vector3[] initPortPoints(int numberOfPorts) {
        Vector3[] portPoints = new Vector3[numberOfPorts];
        //portPoints[0] = new Vector3(0, 0, radius);
        float n = numberOfPorts;
        float k = 1;
        float h = -1 + ( ( 2 * ( k - 1 ) ) / ( n - 1 ) );
        float theta = Mathf.Acos( h );
        float phi = 0;
        while(k <= n) {
            if (k == n) phi = 0;
            center.transform.position = new Vector3(0, 0, 0);
            center.transform.rotation = Quaternion.identity;
            center.transform.rotation *= Quaternion.Euler( ( ( 360 * phi ) / ( 2 * Mathf.PI ) ), ( ( 360 * theta ) / ( 2 * Mathf.PI ) ), 0 );
            center.transform.Translate(Vector3.forward * radius, Space.Self);
            portPoints[(int)k - 1] = new Vector3(center.transform.position.x, center.transform.position.y, center.transform.position.z);
            k++;
            h = -1 + ( (2 * ( k - 1 )) / ( n - 1 ) );
            theta = Mathf.Acos( h );
            phi = (float)(phi + (3.6 / Mathf.Sqrt(n)) * (1 / Mathf.Sqrt(1 - (h*h)))) % (2 * Mathf.PI);
        }
        
        center.transform.position = new Vector3( 0, 0, 0 );
        center.transform.rotation = Quaternion.identity;

        return portPoints;
    }


    void Update () {
	
	}
}
