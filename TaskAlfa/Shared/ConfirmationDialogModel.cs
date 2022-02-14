using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAlfa.Data.ItemViewModels;

namespace TaskAlfa.Shared
{
    public class ConfirmationDialogModel
    {
        public string Text { get; set; } = "Are you sure?";
        public string Title { get; set; } = "Zertifikat löschen";
        public string BtnYes { get; set; } = "Löschen";
        public bool IsOpenConfirmation { get; set; } = false;
        public bool PrimaryKey { get; set; }
    }
}
