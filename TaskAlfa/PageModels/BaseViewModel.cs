using TaskAlfa.Data;
using TaskAlfa.Data.Models;
using TaskAlfa.PageModels.Interface;
using TaskAlfa.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TaskAlfa.PageModels
{
    public class BaseViewModel : ComponentBase, IDisposable
    {
        [Inject] protected ILogger Logger { get; set; }
        [Inject] protected IJSRuntime Js { get; set; }
        [Inject] protected NavigationManager UriHelper { get; set; }

        protected bool IsFailed { get; set; } = false;
        protected string MsgError { get; set; }
        protected string ContextError { get; set; }
        public string RedirectUrl { get; set; }
        public ErrorComponentModel ErrorModel { get; set; } = new ErrorComponentModel();

        private bool disposed = false;
        protected string browserInfo = string.Empty;

        protected InformarionDialogViewModel mInformationDialog = new InformarionDialogViewModel() { Btn = "Schließen" };
        private string mNameUserAndPage;

        protected override async System.Threading.Tasks.Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (string.IsNullOrEmpty(browserInfo))
                {
                    browserInfo = await Js.InvokeAsync<string>("GetBrowserInfo");
                }
            }

            await base.OnAfterRenderAsync(firstRender);
        }
        //public async Task<string> GetUserNameAsync()
        //{
        //    var user = (await authenticationStateTask).User;
        //    return user?.Identity?.Name ?? "";
        //}

        //public string GetUserName()
        //{
        //    var task = Task.Run(async () => await GetUserNameAsync());
        //    return task.Result;
        //}

        //public string GetUserNameWithoutMail()
        //{
        //    var task = Task.Run(async () => await GetUserNameAsync());
        //    var name = task.Result;
        //    var index = name.IndexOf('@');
        //    if (index > -1)
        //    {
        //        return name.Substring(0, index);
        //    }
        //    return name;
        //}

        //public async Task<string> GetStandortIdAsync()
        //{
        //    var user = (await authenticationStateTask).User;
        //    return user?.Claims.FirstOrDefault(r => r.Type == "StandortId")?.Value ?? "0";
        //}

        //public string GetStandortId()
        //{
        //    var task = System.Threading.Tasks.Task.Run(async () => await GetStandortIdAsync());
        //    return task.Result;
        //}

        //public ClaimsPrincipal GetUser()
        //{
        //    var task = System.Threading.Tasks.Task.Run(async () => await authenticationStateTask);
        //    return task.Result.User;
        //}

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~BaseViewModel()
        {
            Dispose(false);
        }

        private void ExceptionType(ExeptionTypeEnum exeptionType, string message, FunctionModelEnum function, IEditModel editModel, IIsRefreshed currentItem)
        {
            if (editModel?.DialogIsOpen == true)//save//OldData Other
            {
                if (exeptionType != ExeptionTypeEnum.Other)
                {
                    mInformationDialog.IsOpenDialog = true;
                    mInformationDialog.Text = message;
                    mInformationDialog.Title = "Aktualisierung fehlgeschlagen";
                }
                else
                {
                    editModel.ErrorString = message;
                }

                return;
            }

            if (exeptionType == ExeptionTypeEnum.OldData && currentItem != null)//Restore Trash//OldData
            {
                currentItem.IsRefreshed = true;
            }

            StateHasChanged();
        }

        private void ExceptionDbUpdateConcurrency(FunctionModelEnum function, IEditModel editModel, IIsRefreshed currentItem)
        {
            if (editModel?.DialogIsOpen == true)
            {
                editModel.IsConcurrencyError = true;
            }

            if (currentItem != null)
            {
                currentItem.IsRefreshed = true;
            }

            StateHasChanged();
        }

        private void ExceptionOther(Exception e)
        {
            ErrorModel.IsOpen = true;
            ErrorModel.ErrorContext = e.StackTrace;
            ErrorModel.ErrorMessage = e.Message;
            IsFailed = true;
            StateHasChanged();
        }

        private void ExceptionDbUpdate(FunctionModelEnum function)
        {
            if (function == FunctionModelEnum.Remove)
            {
                mInformationDialog.IsOpenDialog = true;//remove
                mInformationDialog.Text = Globals.ExceptionText[ExeptionTypeEnum.RemoveItem];
                mInformationDialog.Title = "Löschen fehlgeschlagen";
            }
            else
            {
                mInformationDialog.IsOpenDialog = true;
                mInformationDialog.Text = "Fehler beim Aktualisieren der Datenbank";
                mInformationDialog.Title = "Aktualisierung fehlgeschlagen";
            }
            StateHasChanged();
        }

        //public void SetPageName(string name)
        //{
        //    mNameUserAndPage = $"{GetUserName()}*Error: {name}";
        //}

        public void ExceprionProcessing(Exception exception, FunctionModelEnum function, IIsRefreshed currentModel, IEditModel editModel, string functionName = null)
        {
            if (exception is DbUpdateConcurrencyException)
            {
                ExceptionDbUpdateConcurrency(function, editModel, currentModel);
                return;
            }

            if (exception is ExceptionByType)
            {
                ExceptionType(((ExceptionByType)exception).mExeptionType, exception.Message, function, editModel, currentModel);
                return;
            }

            var funName = functionName ?? function.ToString();
            var path = mNameUserAndPage + "/" + funName;
            Logger.LogError(exception, path + $"*{browserInfo}");

            if (exception is DbUpdateException)
            {
                ExceptionDbUpdate(function);
                return;
            }

            //exception
            ExceptionOther(exception);
        }
        protected void CatchException(Exception e, string additionalInfo)
        {
            Logger.LogError(e, $"Error: {additionalInfo}*{browserInfo}");
            ErrorModel.IsOpen = true;
            ErrorModel.ErrorContext = e.StackTrace;
            ErrorModel.ErrorMessage = e.Message;
            IsFailed = true;
        }
    }
}