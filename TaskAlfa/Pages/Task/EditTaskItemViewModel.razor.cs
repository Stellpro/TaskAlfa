using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAlfa.Data;
using TaskAlfa.Data.ItemViewModels;
using TaskAlfa.Data.Services;
using TaskAlfa.PageModels;
using TaskAlfa.PageModels.Interface;

namespace TaskAlfa.Pages.Task
{
    public class EditTaskItemViewModel: IEditModel
    {
        public bool DialogIsOpen { get; set; }
        public TaskDocumentService DocumentService { get; set; }
        public List<TaskDocumentItemViewModel> DocumentModel { get; set; }
        public bool IsConcurrencyError { get; set; }
        public TaskItemViewModel Model { get; set; }
        public bool Answer { get; set; } = false;
        public List<TaskStatusItemViewModel> StatusModel { get; set; }
        public TaskStatusItemViewModel TaskStatusIdModel { get; set; }
        public bool dialogIsOpenAdd { get; set; }
        public string ErrorString { get; set; }
        public void ConfirmCeate(bool answer)
        {
            Answer = answer;
        }
        public void CreateOrUpdate(List<TaskDocumentItemViewModel> param)
        {
            List<TaskDocumentItemViewModel> collect = param.Where(x => x.TaskDocumentId == 0).ToList();
            foreach (var item in collect)
            {
                if (item.Answer)
                {
                    Answer = item.Answer;
                    item.Answer = false;
                }
                if (Answer)
                {
                    var newItem = DocumentService.Create(item);
                    if (newItem != null)
                    {
                        DocumentModel.Add(newItem);
                        DocumentModel.Remove(item);
                    }
                }
                else
                { 
                        var index = DocumentModel.FindIndex(x => x.TaskId == item.TaskId);
                        item.TaskDocumentId = DocumentModel[index].TaskDocumentId;
                        DocumentModel[index] = item;
                        DocumentService.Update(item);
                        dialogIsOpenAdd = false;        
                }
            }


        }
        public void CreateOrUpdate(TaskDocumentItemViewModel item)
        {

            if (item.Answer)
            {
                Answer = item.Answer;
                item.Answer = false;
            }
            if (Answer)
            {
                var newItem = DocumentService.Create(item);
                if (newItem != null)
                {
                    DocumentModel.Add(newItem);
                }
            }
            else
            {                
                    var index = DocumentModel.FindIndex(x => x.TaskId == item.TaskId);
                    item.TaskDocumentId = DocumentModel[index].TaskDocumentId;
                    DocumentModel[index] = item;
                    DocumentService.Update(item);
                    dialogIsOpenAdd = false;
            }
        }
        public void DeleteFile(string param)
        {

            var deletDocument = DocumentModel.FirstOrDefault(x => x.Comment == param);
            DocumentModel.Remove(deletDocument);
            DocumentService.Remove(deletDocument);
            
        }
        public void DeleteFile(int param)
        {
            if (param != 0)
            {
                var deletDocument = DocumentModel.FirstOrDefault(x => x.TaskDocumentId == param);
                DocumentModel.Remove(deletDocument);
                DocumentService.Remove(deletDocument);
            }

        }
        public byte[] GetFileStream(int id)
        {
            var param = DocumentModel.LastOrDefault(x => x.TaskDocumentId == id);

            var fileStream = param.Dokument;

            return fileStream;
        }
    }
}
