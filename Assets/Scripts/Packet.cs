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
    //��¦
    //public static byte[] Create(MessageType type, string message)
    //{
    //    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
    //    int length = 1 + messageBytes.Length;


    //    ///�إߤ@�ӡu�O���餤�������ɮסv�ΨӼȦs�G�i����
    //    ///using �|�۰�����O����귽�A�קK�O���鬪�|
    //    ///�����b�O���餤�}�@���{�ɪŶ��s����
    //    using (MemoryStream ms = new MemoryStream()) //�إ߰O����y�e�q

    //    ///�N BinaryWriter �j�w�� MemoryStream
    //    using (BinaryWriter bw = new BinaryWriter(ms)) //�إߤG�i��g�J�u��
    //    {
    //        bw.Write(length); //�g�J�T������(int)
    //        bw.Write((byte)type); //�g�J�T������(�ഫ��byte)
    //        bw.Write(messageBytes);//�g�J��ڰT��(byte[])
    //        return ms.ToArray();//��O����y�নbyte[]�Ǧ^
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
