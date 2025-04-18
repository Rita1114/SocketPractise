using System;
using System.IO;
using System.Text;

public enum MessageType
{
    CHAT = 1,
    SYSTEM = 2,
}
public class Packet
{
    public int packetId;
    public string userName;
    public string message;

    #region 
    //基礎
    //public static byte[] Create(MessageType type, string message)
    //{
    //    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
    //    int length = 1 + messageBytes.Length;


    //    ///建立一個「記憶體中的虛擬檔案」用來暫存二進位資料
    //    ///using 會自動釋放記憶體資源，避免記憶體洩漏
    //    ///類似在記憶體中開一個臨時空間存放資料
    //    using (MemoryStream ms = new MemoryStream()) //建立記憶體流容量

    //    ///將 BinaryWriter 綁定到 MemoryStream
    //    using (BinaryWriter bw = new BinaryWriter(ms)) //建立二進位寫入工具
    //    {
    //        bw.Write(length); //寫入訊息長度(int)
    //        bw.Write((byte)type); //寫入訊息類型(轉換成byte)
    //        bw.Write(messageBytes);//寫入實際訊息(byte[])
    //        return ms.ToArray();//把記憶體流轉成byte[]傳回
    //    }
    //}
    //public static (MessageType, string) Parse(byte[] data)
    //{
    //    using (MemoryStream ms = new MemoryStream())
    //    using (BinaryReader reader = new BinaryReader(ms))
    //    {
    //        int length = reader.ReadInt32();
    //        MessageType type = (MessageType)reader.ReadByte();
    //        byte[] messageBytes = reader.ReadBytes(length-1);
    //        string message =Encoding.UTF8.GetString(messageBytes);
    //        return(type, message);
    //    }
    //}
    #endregion

    public byte[] Serialize()
    {
        using MemoryStream stream = new MemoryStream();
        using BinaryWriter writer = new BinaryWriter(stream);
        {
            writer.Write(packetId);
            writer.Write(userName);
            writer.Write(message);
            return stream.ToArray();
        }
    }
    public static Packet Deserialize(byte[] data)
    {
        using MemoryStream dataStream = new MemoryStream(data);
        using BinaryReader reader = new BinaryReader(dataStream);
        {
            return new Packet
            {
                packetId = reader.ReadInt32(),
                userName = reader.ReadString(),
                message = reader.ReadString(),
            };
        }
       
    }
}
