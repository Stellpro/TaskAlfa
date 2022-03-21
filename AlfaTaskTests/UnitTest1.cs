using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TaskAlfa.Data.ItemViewModels;
using static ServiceStack.LicenseUtils;

namespace AlfaTaskTests
{
    class TestsHelper
    {

        internal static void ValidateObject<T>(T obj)
        {




            var type = typeof(T);
            var meta = type.GetCustomAttributes(false).OfType<MetadataTypeAttribute>().FirstOrDefault();
            if (meta != null)
            {
                type = meta.MetadataClassType;
            }
            var propertyInfo = type.GetProperties();
            foreach (var info in propertyInfo)
            {
                var attributes = info.GetCustomAttributes(false).OfType<ValidationAttribute>();
                foreach (var attribute in attributes)
                {
                    var objPropInfo = obj.GetType().GetProperty(info.Name);
                    attribute.Validate(objPropInfo.GetValue(obj, null), info.Name);
                }
            }



        }
    }


    [MetadataType(typeof(TaskItemViewModel))]
    public partial class Service
    {
        public int TaskStatusId { get; set; }
        public string TaskName { get; set; }
        public double PlanDuration { get; set; }
        public double RealDuration { get; set; }



    }
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Range()
        {
            var fiz = new TaskItemViewModel
            {
                TaskStatusId = 1,
                TaskName = "Name",
                PlanDuration = -5,
                RealDuration=2,

            };
            Assert.IsTrue(ValidateModel(fiz).Any(
                v =>v.ErrorMessage.Contains("The field PlanDuration must be between 0,01 and 10000")));
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }


    [TestClass]
    public class ServiceModelTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException), "Required")]
        public void Name_Not_Present()
        {
            var serv = new Service { PlanDuration = 2, TaskStatusId = 2, RealDuration = 2, TaskName = string.Empty };
            TestsHelper.ValidateObject(serv);
        }
        //[TestMethod]
        //[ExpectedException(typeof(ValidationException),"The field PlanDuration must be between 0,01 and 10000")]
        //public void Plane_Range_Present()
        //{
        //    var serv = new Service { PlanDuration = 2, TaskStatusId = 2, RealDuration = -2, TaskName = "name" };
        //    TestsHelper.ValidateObject(serv);
        //}
    }
}