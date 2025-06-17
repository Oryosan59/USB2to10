using USB2to10;
using System;
using System.IO.Ports;
using System.Threading;

public class RealSerialPortService : ISerialPortService
{
    private SerialPort? serialPort;
    public event Action<string>? DataReceived;

    private string portName;
    private int baudRate;

    public RealSerialPortService(string portName, int baudRate = 19200)
    {
        this.portName = portName;
        this.baudRate = baudRate;
    }

    public void Open()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.DataReceived += SerialPort_DataReceived;
        serialPort.Open();
    }

    public void Close()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            string data = serialPort!.ReadLine().Trim();
            DataReceived?.Invoke(data);
        }
        catch
        {
            // 受信エラーは無視
        }
    }
}
