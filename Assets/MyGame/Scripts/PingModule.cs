using UnityEngine;

public class ConnectionTest : MonoBehaviour
{
    public ArduinoManager arduino;

    void Update()
    {
        // Press Space to send a test ping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            arduino.SendToArduino("PING");
            Debug.Log("Sent PING to Arduino");
        }
    }
}