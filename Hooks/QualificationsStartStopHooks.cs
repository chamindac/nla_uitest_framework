using BoDi;
using NLA.Tests.UI.POCOs;
using NLA.Tests.UI.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace NLA.Tests.UI.Hooks
{
    [Binding]
    class QualificationsStartStopHooks
    {
        private readonly IObjectContainer _objectContainer;
        public QualificationsStartStopHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario(Order = 2)]
        [Scope(Feature = "Qualification Management")]
        public void DataSetup(IConfiguration configs, DBHelper databaseHelper)
        {
            _objectContainer.RegisterInstanceAs(databaseHelper.RunDataSetup(configs["DataScripts:Qualification:DataSetupScript"]));
        }

        [AfterScenario(Order = 2)]
        [Scope(Feature = "Qualification Management")]
        public void DataCleanup(IConfiguration configs, GeneratedTestDataKeys generatedTestDataKeys, DBHelper databaseHelper)
        {
            databaseHelper.RunDataCleanup(configs["DataScripts:Qualification:DataCleanupScript"], generatedTestDataKeys.First(kvp => kvp.Key == "TestDataKey").Value.ToString());
        }
    }
}
