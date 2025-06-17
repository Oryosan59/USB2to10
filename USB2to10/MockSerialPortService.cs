using USB2to10;
using System;
using System.Threading;
using System.Threading.Tasks;

public class MockSerialPortService : ISerialPortService
{
    public event Action<string>? DataReceived;

    private bool isRunning = false;

    public void Open()
    {
        isRunning = true;
        Task.Run(async () =>
        {
            while (isRunning)
            {
                // 0か1のランダムな文字列を作成
                var randomData = GenerateRandomBinaryString(8);
                DataReceived?.Invoke(randomData);
                await Task.Delay(1000);  // 1秒ごとに送信
            }
        });
    }

    public void Close()
    {
        isRunning = false;
    }

    private string GenerateRandomBinaryString(int length)
    {
        var rand = new Random();
        char[] chars = new char[length];
        for (int i = 0; i < length; i++)
        {
            chars[i] = rand.Next(2) == 0 ? '0' : '1';
        }
        return new string(chars);
    }
}
