using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskAlfa.Data.ItemViewModels;

namespace TestAlfaProject
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Test1()
        {   TaskItemViewModel testModel = new TaskItemViewModel();
            
                TaskAlfa.Pages.Task.ItemViewModel TestClass = new TaskAlfa.Pages.Task.ItemViewModel();
            var fixture = new Fixture();


            testModel.TaskName = "name";
                testModel.TaskStatusId = 2;
                testModel.PlanDuration = -1;
                testModel.RealDuration = 2;
              var temp =  fixture.Create<TaskItemViewModel>();
            Assert.IsNotNull(temp);
            


            
        }
    }
}