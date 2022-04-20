namespace TaskAlfa.PageModels.OverWeriteFile
{
    public class UpdateOrCreateDilogModel
    {
        public bool IsOpenUpdateOrCreate { get; set; } = false;
        public string Text { get; set; } = "Do you want to overwrite the file or create";
        
    }
}
