namespace DbRepository.Models
{
    public interface IRowVersion
    {
        public byte[] RowVersion { get; set; }
    }
}