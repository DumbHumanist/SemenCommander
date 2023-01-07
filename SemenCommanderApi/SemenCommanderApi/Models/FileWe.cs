namespace SemenCommanderApi.Models
{
    public class FileWe
    {
        public FileWe(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
