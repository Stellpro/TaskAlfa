using TaskAlfa.Data.Services;
using TaskAlfa.PageModels;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAlfa.PagesModels.SystemPage;

namespace TaskAlfa.Pages.SystemPage
{
    public class UserMsgErrorLogPageViewModel : TaskAlfa.PageModels.BaseViewModel
    {
        [Inject] protected SystemPageService Service { get; set; }
        [Inject] private IMatDialogService MatDialogService { get; set; }
        protected List<ErrorLogModel> Model { get; set; }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            try
            {
                GetLogs();
            }
            catch (Exception e)
            {
                CatchException(e, "UserMsgErrorLogPageViewModel/OnInitializedAsync");
                StateHasChanged();
            }
        }

        public async System.Threading.Tasks.Task Remove(ErrorLogModel error)
        {
            var result = await MatDialogService.AskAsync("Möchten Sie den Eintrag wirklich löschen?", new string[] { "Ja", "Abbrechen" });

            if (result == "Ja")
            {
                try
                {
                    Service.RemoveUserMsgErrorByErrorId(error.Id);
                    Model.Remove(error);
                    StateHasChanged();
                }
                catch (Exception e)
                {
                    CatchException(e, "SystemErrorLogPage/Remove");
                    StateHasChanged();
                }
            }
        }

        protected void GetLogs()
        {
            Model = Service.GetModelUserMsg();
        }

        protected override void Dispose(bool disposing)
        {
            Model?.Clear();
            Model = null;
            ErrorModel = null;
        }
    }
}