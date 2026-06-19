using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class ArduinoManager : MonoBehaviour
{
    [Header("Serial Settings")]
    public string portName = "COM5";
    public int baudRate = 9600;

    private SerialPort _serialPort;
    private Thread _readThread;
    private bool _isRunning = false;
    private string _lastMessage = "";

    void Start()
    {
        ConnectToArduino();
    }

    void ConnectToArduino()
    {
        try
        {
            _serialPort = new SerialPort(portName, baudRate);
            _serialPort.ReadTimeout = 100;
            _serialPort.Open();
            _isRunning = true;

            _readThread = new Thread(ReadSerialLoop);
            _readThread.IsBackground = true;
            _readThread.Start();

            Debug.Log($"Connected to Arduino on {portName}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to connect: {e.Message}");
        }
    }

    void ReadSerialLoop()
    {
        while (_isRunning && _serialPort != null && _serialPort.IsOpen)
        {
            try
            {
                string line = _serialPort.ReadLine();
                _lastMessage = line.Trim();
            }
            catch (System.TimeoutException) { }
            catch (System.Exception e)
            {
                Debug.LogWarning($"Read error: {e.Message}");
            }
        }
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(_lastMessage))
        {
            Debug.Log($"Arduino says: {_lastMessage}");
            _lastMessage = "";
        }
    }

    public void SendToArduino(string message)
    {
        if (_serialPort != null && _serialPort.IsOpen)
            _serialPort.WriteLine(message);
    }

    public void SendRGB(int r, int g, int b)
    {
        r = Mathf.Clamp(r, 0, 255);
        g = Mathf.Clamp(g, 0, 255);
        b = Mathf.Clamp(b, 0, 255);
        SendToArduino($"RGB:{r},{g},{b}");
    }

    void OnApplicationQuit()
    {
        _isRunning = false;
        if (_serialPort != null && _serialPort.IsOpen)
            _serialPort.Close();
        if (_readThread != null && _readThread.IsAlive)
            _readThread.Join();
    }
}