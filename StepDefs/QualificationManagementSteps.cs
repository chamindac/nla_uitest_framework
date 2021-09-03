using NLA.Tests.UI.PageObjects;
using NLA.Tests.UI.POCOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using Xunit;

namespace NLA.Tests.UI.StepDefs
{
    [Binding]
    public class QualificationManagementSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly GeneratedTestDataKeys _generatedTestDataKeys;
        private readonly QualificationPage _qualificationPage;
        private readonly IConfiguration _configs;
        private readonly string _testDataKey;
        private string _degree;
        private string _grade;

        public QualificationManagementSteps(ScenarioContext scenarioContext,
            GeneratedTestDataKeys generatedTestDataKeys,
            IConfiguration configs, QualificationPage qualificationPage)
        {
            _scenarioContext = scenarioContext;
            _configs = configs;
            _generatedTestDataKeys = generatedTestDataKeys;
            _testDataKey = _generatedTestDataKeys.First(Kvp => Kvp.Key == "TestDataKey").Value.ToString();
            _qualificationPage = qualificationPage;
        }

        [Given(@"that I am in the qualifcation page of an employee")]
        public void GivenThatIAmInTheQualifcationPageOfAnEmployee()
        {
            _qualificationPage.Navigate(_generatedTestDataKeys.First(kvp => kvp.Key == "EmployeeGuid").Value.ToString());
        }

        [Given(@"opened the qualification add dialog")]
        public void GivenOpenedTheQualificationAddDialog()
        {
            _qualificationPage.AddNew.Click();
            _qualificationPage.AddEditDialog.Title.WaitToLoadWithText("Create Qualifications");
        }

        [When(@"the qualification is added with Institute, Qualification, Grade and Period")]
        public void WhenTheQualificationIsAddedWithInstituteQualificationGradeAndPeriod()
        {
            _qualificationPage.AddEditDialog.Institute
                .Select(_generatedTestDataKeys.First(Kvp => Kvp.Key == "InstituteName").Value.ToString());

            _degree = string.Concat("Degree", _testDataKey, "New");

            _qualificationPage.AddEditDialog.Degree.Text = _degree;
            _qualificationPage.AddEditDialog.Grade.Text = string.Concat("Grade", _testDataKey);
            _qualificationPage.AddEditDialog.FromDate.Value = DateTime.Now.AddYears(-3);
            _qualificationPage.AddEditDialog.ToDate.Value = DateTime.Now.AddYears(-2);
            _qualificationPage.AddEditDialog.Remarks.Text = string.Concat("Remarks", _testDataKey);

            _qualificationPage.AddEditDialog.SaveChanges.Click();

            _qualificationPage.AddEditDialog.Title.WaitToBeInvisible();
        }

        [Then(@"the qualification should be shown in the list of qualifications")]
        public void ThenTheQualificationShouldBeShownInTheListOfQualifications()
        {
            _qualificationPage.Filter.Search(_degree);

            _qualificationPage.Info.WaitToLoadWithText(_configs["System:SingleRecordFilterMessage"]);

            Assert.Equal(_degree,
                _qualificationPage.FilteredQualification.Degree.Text);
        }


        [Given(@"and selected a qualification to modify")]
        public void GivenAndSelectedAQualificationToModify()
        {
            _degree = _generatedTestDataKeys.First(Kvp => Kvp.Key == "QualificationDegree").Value.ToString();
            _qualificationPage.Filter.Search(_degree);

            _qualificationPage.Info.WaitToLoadWithText(_configs["System:SingleRecordFilterMessage"]);
            _qualificationPage.FilteredQualification.Degree.WaitToLoadWithText(_degree);

            _qualificationPage.FilteredQualification.Edit.Click();

            _qualificationPage.AddEditDialog.Title.WaitToLoadWithText("Edit Qualifications");

        }

        [When(@"the qualification grade is modified")]
        public void WhenTheQualificationGradeIsModified()
        {
            _grade = string.Concat("Grade ", _testDataKey, " Modified");
            _qualificationPage.AddEditDialog.Grade.Text = _grade;

            _qualificationPage.AddEditDialog.SaveChanges.Click();

            _qualificationPage.AddEditDialog.Title.WaitToBeInvisible();
        }

        [Then(@"the modified qualification grade should be shown in the list of qualifications")]
        public void ThenTheModifiedQualificationGradeShouldBeShownInTheListOfQualifications()
        {
            _qualificationPage.Info.WaitToLoadWithText(_configs["System:SingleRecordFilterMessage"]);

            Assert.Equal(_grade,
                _qualificationPage.FilteredQualification.Grade.Text);

        }

    }
}
