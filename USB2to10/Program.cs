using USB2to10;
using System;
using System.Text.RegularExpressions;

class Program
{
    static ISerialPortService? serialService;

    static void Main()
    {
        // ↓ ここで切り替え可能（実機 or モック）
        bool useMock = true;

        if (useMock)
        {
            serialService = new MockSerialPortService();
        }
        else
        {
            // 実際はDetectSerialPort()を使ってポート名を取得
            string detectedPort = DetectSerialPort();
            if (detectedPort == null)
            {
                Console.WriteLine("適切なポートが見つかりませんでした。");
                return;
            }
            serialService = new RealSerialPortService(detectedPort);
        }

        serialService.DataReceived += OnDataReceived;
        serialService.Open();

        Console.WriteLine("データ受信中...（終了するにはEnter）");
        Console.ReadLine();

        serialService.Close();
    }

    static void OnDataReceived(string data)
    {
        if (IsBinaryString(data))
        {
            int decimalValue = Convert.ToInt32(data, 2);
            Console.WriteLine($"受信: {data} → {decimalValue}");
        }
        else
        {
            Console.WriteLine($"無効なデータ: {data}");
        }
    }

    static string? DetectSerialPort()
    {
        // 元のDetectSerialPortの中身をコピーしてください
        return null; // ここは省略
    }

    static bool IsBinaryString(string data)
    {
        return Regex.IsMatch(data, @"^[01]+$");
    }
}
