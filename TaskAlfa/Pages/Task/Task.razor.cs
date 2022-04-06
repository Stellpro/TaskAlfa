using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskAlfa.Data;
using TaskAlfa.Data.ItemViewModels;
using TaskAlfa.Data.Models;
using TaskAlfa.Data.Services;
using TaskAlfa.PageModels;
using TaskAlfa.Reporting;
using TaskAlfa.Reporting.Models;
using TaskAlfa.Shared;


namespace TaskAlfa.Pages.Task
{
    public class ItemViewModel : BaseViewModel
    {
        public bool dialogIsOpenAdd { get; set; }
        [Inject] private TaskService Service { get; set; }
        [Inject] private TaskDocumentService DocumentService { get; set; }
        [Inject] private TaskStatusService StatusService { get; set; }
        public List<TaskItemViewModel> Model { get; set; } = new List<TaskItemViewModel>();
        public List<TaskDocumentItemViewModel> DocumentModel { get; set; } = new List<TaskDocumentItemViewModel>();
        public TaskDocumentItemViewModel AddModel { get; set; } = new TaskDocumentItemViewModel();
        public List<TaskStatusItemViewModel> StatusModel { get; set; } = new List<TaskStatusItemViewModel>();

        protected EditTaskItemViewModel mEditViewModel = new EditTaskItemViewModel();

        public TaskItemViewModel mCurrentItem;

        public TaskItemViewModel Isdelete;

        public bool Answer;

        protected bool dialogIsOpen = false;

        public Dictionary<int, List<TaskItemViewModel>> BoardItem = new Dictionary<int, List<TaskItemViewModel>>();

        public ConfirmationDialogModel ConfirmDialogModel = new ConfirmationDialogModel()

        {
            IsOpenConfirmation = false,
            Text = "Are you sure?",
            Title = "Warning!"
        };

        public UpdateOrCreateDilogModel updateOrCreateDilog = new UpdateOrCreateDilogModel()
        {
            IsOpenUpdateOrCreate = false
        };
        protected int bezeichnungFiltr { get; set; }
        protected string taskNameFiltr { get; set; }
        protected string TasknameFiltr
        {
            get { return taskNameFiltr; }
            set { taskNameFiltr = value; StartSearchName(); }
        }
        protected int BezeichnungFiltr
        {
            get { return bezeichnungFiltr; }
            set { bezeichnungFiltr = value; StartSearch(); }
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    Model = Service.GetList();
                    DocumentModel = DocumentService.GetList();
                    StatusModel = StatusService.GetList();
                    CreateBoard(StatusModel);



                }

                catch (Exception e)
                {
                    ExceprionProcessing(e, FunctionModelEnum.OnAfterRenderAsync, null, null);
                }

                finally
                {


                }
            }
        }
        private void CreateBoard(List<TaskStatusItemViewModel> param)
        {
            foreach (var item in param)
            {
                BoardItem.Add(item.TaskStatusId, Model.Where(x => x.TaskStatusId == item.TaskStatusId).ToList());
                foreach (var i in BoardItem)
                {
                    if (i.Key == item.TaskStatusId)
                        foreach (var y in i.Value.Where(x => x.TaskStatusId == i.Key).ToList())
                        {
                            y.Filename = null;
                        }
                }
            }
            StateHasChanged();
        }
        public void Visible(TaskStatusItemViewModel item)
        {
            item.IsVisible = false;
            var index = StatusModel.FindIndex(x => x.TaskStatusId == item.TaskStatusId);
            StatusModel[index] = item;
            StatusService.Update(item);
            StateHasChanged();
        }
        protected async void StartSearch()
        {
            try
            {
                Model.Clear();
                Model = await Service.GetFiltering(bezeichnungFiltr);
            }
            catch (Exception e)
            {
                ExceprionProcessing(e, FunctionModelEnum.Other, null, null, "StartSearch");
            }
            finally
            {
                StateHasChanged();
            }
            return;
        }
        protected async void StartSearchName()
        {



            try
            {
                Model.Clear();
                Model = await Service.GetNameFiltering(taskNameFiltr);
            }
            catch (Exception e)
            {
                ExceprionProcessing(e, FunctionModelEnum.Other, null, null, "StartSearch");
            }
            finally
            {
                StateHasChanged();
            }
            return;

        }
        public async void HandleKeyUp(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                StartSearchName();
            }
        }
        public void CreateItem(int i)
        {

            var index = StatusModel.FindIndex(x => x.TaskStatusId == i);
            mCurrentItem = new TaskItemViewModel();
            mEditViewModel.StatusModel = StatusModel;
            mEditViewModel.TaskStatusIdModel = StatusModel[index];
            mCurrentItem.TaskStatusId = StatusModel[index].TaskStatusId;
            mEditViewModel.DocumentModel = DocumentModel;
            mEditViewModel.DocumentService = DocumentService;
            mEditViewModel.DialogIsOpen = true;
            mEditViewModel.Model = mCurrentItem;
            StateHasChanged();


        }
        public void CreateItem()
        {

            mCurrentItem = new TaskItemViewModel();
            mEditViewModel.StatusModel = StatusModel;
            mEditViewModel.DialogIsOpen = true;
            mEditViewModel.Model = mCurrentItem;



        }
        public void CreateReport()
        {
            TaskAlfaReportModel model = new TaskAlfaReportModel();
            model.Block = GetTaskReportList();

            byte[] file = new XtraReportEngine().CreateAnschreibenEmpfehlungReport(model, 2);

            System.IO.File.WriteAllBytes(@"C:\Users\stellpro\Desktop\TaskAlfa.pdf", file);
        }
        private List<TaskAlfaItemReportModel> GetTaskReportList()
        {
            var returnList = new List<TaskAlfaItemReportModel>();
            foreach (var item in Model)
            {
                returnList.Add(new TaskAlfaItemReportModel
                {
                    TaskName = item.TaskName,
                    PlanDuration = item.PlanDuration,
                    RealDuration = item.RealDuration,
                    StatusName = StatusModel.Where(X => X.TaskStatusId == item.TaskStatusId).FirstOrDefault()?.StatusName
                });
            }
            return returnList;
        }
        public void Edit(TaskItemViewModel param1)
        {
            mCurrentItem = param1.Clone() as TaskItemViewModel;
            mEditViewModel.StatusModel = StatusModel;
            mEditViewModel.Model = mCurrentItem;
            mEditViewModel.DocumentModel = DocumentModel;
            mEditViewModel.DocumentService = DocumentService;
            mEditViewModel.DialogIsOpen = true;

        }
        protected void RemoveTask(TaskItemViewModel item)
        {
            try
            {
                Service.Remove(item);
                var deletDocument = DocumentModel.Where(x => x.TaskId == item.TaskId).ToList();
                foreach (var i in deletDocument)
                {
                    DocumentService.Remove(i);
                }

                Model.Remove(item);

            }
            catch (Exception e)
            {
            }
        }
        protected async System.Threading.Tasks.Task Remove(TaskItemViewModel item)
        {
            try
            { 
                ConfirmDialogModel.IsOpenConfirmation = true;
                Isdelete = item;
                await System.Threading.Tasks.Task.CompletedTask;
            }
            catch (Exception e)
            {
            }
        }
        protected async System.Threading.Tasks.Task ConfirmRemove(bool answer)
        {
            if (answer)
            {
                Service.Remove(Isdelete);
                Model.Remove(Isdelete);
                UpdateBoard(StatusModel);
                StateHasChanged();
                await System.Threading.Tasks.Task.CompletedTask;


            }
        }
        protected void Restore(TaskItemViewModel item)
        {
            try
            {
                item.IsDeleted = false;

                StateHasChanged();
            }
            catch (Exception e)
            {

                ExceprionProcessing(e, FunctionModelEnum.Restore, item, null);
            }
        }
        protected void Deleted()
        {
            ConfirmDialogModel.IsOpenConfirmation = true;
        }
        protected void Save(TaskItemViewModel item)
        {
            try
            {

                if (item.TaskId > 0)
                {
                    var newItem = UpdateTask(item);
                    var index = Model.FindIndex(x => x.TaskId == this.mCurrentItem.TaskId);
                    Model[index] = newItem;
                    UpdateBoard(StatusModel);

                }
                else
                {
                    var newItem = Service.Create(item);
                    if (newItem != null)
                    {
                        Model.Add(newItem);
                        UpdateBoard(StatusModel);

                    }
                }
                if (mEditViewModel.IsConcurrencyError)
                {
                    StateHasChanged();
                }
                else
                {
                    mEditViewModel.DialogIsOpen = false;

                }

            }
            catch (Exception e)
            {
                mCurrentItem = item;
                ExceprionProcessing(e, FunctionModelEnum.Save, mCurrentItem, mEditViewModel);
            }
            StateHasChanged();
        }
        private void UpdateBoard(List<TaskStatusItemViewModel> item)
        {
            BoardItem.Clear();
            foreach (var i in item)
            {
                BoardItem.Add(i.TaskStatusId, Model.Where(x => x.TaskStatusId == i.TaskStatusId).ToList());
            }
        }
        public TaskItemViewModel UpdateTask(TaskItemViewModel item)
        {
            try
            {
                var index = Model.FindIndex(x => x.TaskId == item.TaskId);
                Model[index] = item;
                return Service.Update(item);
            }
            catch (DbUpdateConcurrencyException e)
            {
                ExceprionProcessing(e, FunctionModelEnum.Update, mCurrentItem, mEditViewModel, "Update");
                mEditViewModel.IsConcurrencyError = true;
                return mCurrentItem = Service.Reload(item.Item);
            }


        }
        public void Reload()
        {
            mEditViewModel.IsConcurrencyError = false;
            mEditViewModel.DialogIsOpen = false;
        }
        protected void Trash(TaskItemViewModel item)
        {
            try
            {
                item.IsDeleted = true;
                var newItem = Service.UpdateIsDelete(item);
                var index = Model.FindIndex(x => x.TaskId == item.TaskId);
                Model[index] = newItem;
                StateHasChanged();
            }
            catch (Exception e)
            {
                mCurrentItem = item;
                ExceprionProcessing(e, FunctionModelEnum.Trash, item, null);
            }
        }
        protected void Sort(KeyValuePair<string, string> pair)
        {
            Model = pair.Value == "desc" ? Model.OrderByDescending(x => x.GetType().GetProperty(pair.Key).GetValue(x, null)).ToList()
                : Model.OrderBy(x => x.GetType().GetProperty(pair.Key).GetValue(x, null)).ToList();
            StateHasChanged();
        }
    }
}
