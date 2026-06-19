using UnityEngine;

public class RGBController : MonoBehaviour
{
    public ArduinoManager arduino;

    [Range(0, 255)] public int r = 0;
    [Range(0, 255)] public int g = 0;
    [Range(0, 255)] public int b = 0;

    private int _lastR, _lastG, _lastB;

    void Update()
    {
        if (r != _lastR || g != _lastG || b != _lastB)
        {
            arduino.SendRGB(r, g, b);
            _lastR = r; _lastG = g; _lastB = b;
        }
    }
}