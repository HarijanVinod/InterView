using InterviewDemo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestInsuranceClaim.Controllers.Tests
{
    [TestClass]
    public class LoadLossTypeDetailsControllerTest
    {
        [TestMethod]
        public void GetLossTypeDataTest()
        {
            LoadLossTypeDetailsController loadLossTypeDetailsController = new LoadLossTypeDetailsController();
            ViewResult result = loadLossTypeDetailsController.GetLossTypeData() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}