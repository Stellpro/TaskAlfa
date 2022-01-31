namespace TaskAlfa.PageModels.Interface
{
    public interface IEditModel
    {
        public bool DialogIsOpen { get; set; }
        public string ErrorString { get; set; }
        public bool IsConcurrencyError { get; set; }
    }
}
