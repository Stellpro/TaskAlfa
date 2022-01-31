namespace TaskAlfa.Shared
{
    public class InformarionDialogViewModel
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public string Btn { get; set; }
        public bool IsOpenDialog { get; set; } = false;
        public string BtnClass { get; set; } = "btn btn-outline-danger btn-nav";
    }
}
