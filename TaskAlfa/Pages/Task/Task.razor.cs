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

namespace TaskAlfa.Pages.Task
{
    public class ItemViewModel : BaseViewModel
    {
        [Inject] private TaskService Service { get; set; }
        [Inject] private TaskStatusService StatusService { get; set; }
        public List<TaskItemViewModel> Model { get; set; } = new List<TaskItemViewModel>();
        public List<TaskStatusItemViewModel> StatusModel { get; set; } = new List<TaskStatusItemViewModel>();
        
        protected EditTaskItemViewModel mEditViewModel = new EditTaskItemViewModel();
        public TaskItemViewModel mCurrentItem;
        public TaskItemViewModel Isdelete;
        protected bool dialogIsOpen = false;
        public Dictionary<int, List<TaskItemViewModel>> BoardItem = new Dictionary<int,List<TaskItemViewModel>>();
        public ConfirmationDialogModel ConfirmDialogModel = new ConfirmationDialogModel()
        {
            IsOpenConfirmation = false,
            Text = "Are you sure?",
            Title = "Warning!"
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
                    StatusModel = StatusService.GetList();                 
                    foreach (var item in StatusModel)
                    {
                        BoardItem.Add(item.TaskStatusId,Model.Where(x=>x.TaskStatusId==item.TaskStatusId).ToList());
                        StateHasChanged();
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

        public void CreateItem()
        {

            mCurrentItem = new TaskItemViewModel();
            mEditViewModel.StatusModel = StatusModel;
            mEditViewModel.DialogIsOpen = true;
            mEditViewModel.Model = mCurrentItem;



        }
        public void CreateItem(int i)
        {
            
            var index = StatusModel.FindIndex(x => x.TaskStatusId == i);
            mCurrentItem = new TaskItemViewModel();
            mEditViewModel.StatusModel = StatusModel;
            mEditViewModel.TaskStatusIdModel = StatusModel[index];
            mCurrentItem.TaskStatusId = StatusModel[index].TaskStatusId;
            mEditViewModel.DialogIsOpen = true;
            mEditViewModel.Model = mCurrentItem;
            StateHasChanged();


        }

        public void Edit(TaskItemViewModel param1)
        {
            mCurrentItem = param1.Clone() as TaskItemViewModel;
            mEditViewModel.StatusModel = StatusModel;
            mEditViewModel.Model = mCurrentItem;
            mEditViewModel.DialogIsOpen = true;

        }


        //protected void Remove(TaskItemViewModel item)
        //{
        //    try
        //    {
        //        Service.Remove(item);
        //        Model.Remove(item);
                
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //}
        protected async System.Threading.Tasks.Task Remove(TaskItemViewModel item)
        {
            try
            {
                ConfirmDialogModel.IsOpenConfirmation = true;
                Isdelete = item;
                //Service.Remove(item);
                //Model.Remove(item);
                //BoardItem.Clear();
                //foreach (var y in StatusModel)
                //{
                //    BoardItem.Add(y.TaskStatusId, Model.Where(x => x.TaskStatusId == y.TaskStatusId).ToList());
                //    StateHasChanged();
                //}
                //StateHasChanged();
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
                BoardItem.Clear();
                foreach (var y in StatusModel)
                {
                    BoardItem.Add(y.TaskStatusId, Model.Where(x => x.TaskStatusId == y.TaskStatusId).ToList());
                    StateHasChanged();
                }
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
                //    mCurrentItem = item;
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
                    var newItem = Update(item);
                    var index = Model.FindIndex(x => x.TaskId == this.mCurrentItem.TaskId);
                    Model[index] = newItem;
                    BoardItem.Clear();
                    foreach (var i in StatusModel)
                    {
                        BoardItem.Add(i.TaskStatusId, Model.Where(x => x.TaskStatusId == i.TaskStatusId).ToList());
                        StateHasChanged();
                    }
                    StateHasChanged();
                }
                else
                {
                    var newItem = Service.Create(item);
                    if (newItem != null)
                    {
                        Model.Add(newItem);
                        BoardItem.Clear();
                        foreach (var i in StatusModel)
                        {
                            BoardItem.Add(i.TaskStatusId, Model.Where(x => x.TaskStatusId == i.TaskStatusId).ToList());
                            StateHasChanged();
                        }
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


        public TaskItemViewModel Update(TaskItemViewModel item)
        {
            if (item.TaskId == 0)
            {

                return item;

            }
            else
            {

                try
                {
                    var index = Model.FindIndex(x => x.TaskId == item.TaskId);
                    Model[index] = item;
                    return Service.Update(item);


                }


                catch (DbUpdateConcurrencyException e)
                {   ExceprionProcessing(e, FunctionModelEnum.Update, mCurrentItem, mEditViewModel,"Update");

                    mEditViewModel.IsConcurrencyError = true;
                    return mCurrentItem = Service.Reload(item.Item);
                    
                }

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
