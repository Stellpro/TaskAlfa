using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskAlfa.Data;
using TaskAlfa.Data.ItemViewModels;
using TaskAlfa.Data.Services;
using TaskAlfa.PageModels;
using TaskAlfa.Shared;


namespace TaskAlfa.Pages.TaskStatusTable
{
    public class ItemViewModel : BaseViewModel
    {
        [Inject] private TaskStatusService StatusService { get; set; }
        [Inject] private TaskService TaskService { get; set; }
        public List<TaskStatusItemViewModel> StatusModel { get; set; } = new List<TaskStatusItemViewModel>();
        public List<TaskItemViewModel> DeletedModel { get; set; } = new List<TaskItemViewModel>();
        protected EditTaskStatusItemViewModel mEditViewModel = new EditTaskStatusItemViewModel();
        public TaskStatusItemViewModel mCurrentItem;
        public ConfirmationDialogModel ConfirmDialogModel = new ConfirmationDialogModel();
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    DeletedModel = TaskService.GetList();
                    StatusModel = StatusService.GetList();
                    foreach (var item in StatusModel)
                    {


                        var temp = DeletedModel.Where(x => x.TaskStatusId == item.TaskStatusId).ToList();
                        item.Count = temp.Count();
                        
                    }

                }

                catch (Exception e)
                {
                    ExceprionProcessing(e, FunctionModelEnum.OnAfterRenderAsync, null, null);
                }

                finally
                {
                    StateHasChanged();
                }
            }
        }
        public void CreateItem()
        {
            mCurrentItem = new TaskStatusItemViewModel();
            mEditViewModel.DialogIsOpen = true;
            mEditViewModel.TaskStatusModel = mCurrentItem;
        }
        public void Edit(TaskStatusItemViewModel param1)
        {
            mCurrentItem = param1.Clone() as TaskStatusItemViewModel;
            mEditViewModel.TaskStatusModel = mCurrentItem;
            mEditViewModel.DialogIsOpen = true;

        }
        protected async System.Threading.Tasks.Task Remove(TaskStatusItemViewModel item)
        {
            try
            {   
                item.IsDeleted = true;
                ConfirmDialogModel.IsOpenConfirmation = true;
                mCurrentItem = item;
                await System.Threading.Tasks.Task.CompletedTask;
                //Service.Remove(item);
                //Model.Remove(item);
                //BoardItem.Clear();
                //foreach (var y in StatusModel)
                //{
                //    BoardItem.Add(y.TaskStatusId, Model.Where(x => x.TaskStatusId == y.TaskStatusId).ToList());
                //    StateHasChanged();
                //}
                //StateHasChanged();
                
            }
            catch (Exception e)
            {
            }
        }

        protected async System.Threading.Tasks.Task ConfirmRemove(bool answer)
        {
            try
            {
                if (answer)
                {
                    StatusService.Remove(mCurrentItem);
                    StatusModel.Remove(mCurrentItem);
                    StateHasChanged();
                    await System.Threading.Tasks.Task.CompletedTask;
                }
            }
            catch (Exception e)
            {

                ConfirmDialogModel.PrimaryKey = true;
            }
        }
        protected void Restore(TaskStatusItemViewModel item)
        {
            try
            {
                item.IsDeleted = false;

                StateHasChanged();
            }
            catch (Exception e)
            {
                //    mCurrentItem = item;
                ExceprionProcessing(e, FunctionModelEnum.Restore, item, null);
            }
        }

        protected void Deleted()
        {
            ConfirmDialogModel.IsOpenConfirmation = true;
        }

        protected void Save(TaskStatusItemViewModel item)
        {
            try
            {

                if (item.TaskStatusId > 0)
                {
                    var newItem = Update(item);
                    var index = StatusModel.FindIndex(x => x.TaskStatusId == this.mCurrentItem.TaskStatusId);
                    StatusModel[index] = newItem;
                    //BoardItem.Clear();
                    //foreach (var i in StatusModel)
                    //{
                    //    BoardItem.Add(i.TaskStatusId, Model.Where(x => x.TaskStatusId == i.TaskStatusId).ToList());
                    //    StateHasChanged();
                    //}
                    StateHasChanged();
                }
                else
                {
                    var newItem = StatusService.Create(item);
                    if (newItem != null)
                    {
                        StatusModel.Add(newItem);
                        //BoardItem.Clear();
                        //foreach (var i in StatusModel)
                        //{
                        //    BoardItem.Add(i.TaskStatusId, Model.Where(x => x.TaskStatusId == i.TaskStatusId).ToList());
                        //    StateHasChanged();
                        //}
                        StateHasChanged();

                    }
                }
                if (mEditViewModel.IsConcurrencyError)
                {
                    StateHasChanged();
                }
                else
                {
                    mEditViewModel.DialogIsOpen = false;
                    StateHasChanged();
                }

            }
            catch (Exception e)
            {
                mCurrentItem = item;
                ExceprionProcessing(e, FunctionModelEnum.Save, mCurrentItem, mEditViewModel);
            }
        }


        public TaskStatusItemViewModel Update(TaskStatusItemViewModel item)
        {
            if (item.TaskStatusId == 0)
            {

                return item;

            }
            else
            {

                try
                {
                    var index = StatusModel.FindIndex(x => x.TaskStatusId == item.TaskStatusId);
                    StatusModel[index] = item;
                    return StatusService.Update(item);


                }


                catch (DbUpdateConcurrencyException e)
                {
                    ExceprionProcessing(e, FunctionModelEnum.Update, mCurrentItem, mEditViewModel, "Update");

                    mEditViewModel.IsConcurrencyError = true;
                    return mCurrentItem;

                }

            }
        }
        public void Reload()
        {
            mEditViewModel.IsConcurrencyError = false;
            mEditViewModel.DialogIsOpen = false;
        }
        //protected void Trash(TaskStatusItemViewModel item)
        //{
        //    try
        //    {
        //        //item.IsDeleted = true;
        //        var newItem = StatusService.UpdateIsDelete(item);
        //        var index = StatusModel.FindIndex(x => x.TaskStatusId == item.TaskStatusId);
        //        StatusModel[index] = newItem;
        //        StateHasChanged();
        //    }
        //    catch (Exception e)
        //    {
        //        mCurrentItem = item;
        //        ExceprionProcessing(e, FunctionModelEnum.Trash, item, null);
        //    }
        //}
        protected void Sort(KeyValuePair<string, string> pair)
        {
            StatusModel = pair.Value == "desc" ? StatusModel.OrderByDescending(x => x.GetType().GetProperty(pair.Key).GetValue(x, null)).ToList()
                : StatusModel.OrderBy(x => x.GetType().GetProperty(pair.Key).GetValue(x, null)).ToList();
            StateHasChanged();
        }
    }
}
