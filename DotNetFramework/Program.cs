using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DotNetFramework
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                // カスタム例外をスロー
                throw new CustomException("これはカスタム例外です", 404);
            }
            catch (CustomException ex)
            {
                Trace.WriteLine($"例外メッセージ: {ex.Message}");
                Trace.WriteLine($"エラーコード: {ex.ErrorCode}");

                // 例外をシリアル化
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream("ExceptionData.bin", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, ex);
                }

                // シリアル化された例外をデシリアル化
                using (Stream stream = new FileStream("ExceptionData.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    CustomException deserializedEx = (CustomException)formatter.Deserialize(stream);
                    Trace.WriteLine("デシリアル化された例外:");
                    Trace.WriteLine($"例外メッセージ: {deserializedEx.Message}");
                    Trace.WriteLine($"エラーコード: {deserializedEx.ErrorCode}");
                }
            }
        }
    }
}
