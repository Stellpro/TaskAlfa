namespace TaskAlfa.Data
{
    public enum UnterrichtsmodellEnum
    {
        VZ = 1,
        TZ = 2
    }

    public enum FunctionModelEnum
    {
        OnInitializedAsync,
        OnAfterRenderAsync,
        Save,
        Reload,
        Trash,
        Update,
        Remove,
        Restore,
        Other
    }

    public enum ExeptionTypeEnum
    {
        Concurrency = 1,
        OldData = 2,
        RemoveItem = 3,
        Other
    }
}
