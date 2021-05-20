using System;

namespace com.bitscopic.reportservice.src
{
    [Serializable]
    public class FileSystemFile
    {
        public DateTime created;
        public String fileName;
        public Int32 size;
        public byte[] data;

        public FileSystemFile() { }

        public FileSystemFile(String fileName, byte[] data)
        {
            this.fileName = fileName;
            this.data = data;

            this.size = data.Length;
            this.created = DateTime.Now;
        }
    }
}