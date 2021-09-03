using NLA.Tests.UI.Utils;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLA.Tests.UI.PageObjects
{
    public class QualificationPage : PageObjectBase
    {
        public QualificationPage(DriverFacade driverFacade, IConfiguration configs,
            AddEditDialogElement addEditDialog,
            AddNewElement addNew,
            FilterElement filter,
            InfoElement info,
            FilteredQualificationElement filteredQualification) : base(driverFacade, configs)
        {
            AddEditDialog = addEditDialog;
            AddNew = addNew;
            Filter = filter;
            Info = info;
            FilteredQualification = filteredQualification;
        }

        #region Element Defintions
        public class AddEditDialogElement : PageObjectBase
        {
            public AddEditDialogElement(DriverFacade driverFacade, IConfiguration configs,
                TitleElement title,
                InstituteElement institute,
                DegreeElement degree,
                GradeElement grade,
                FromDateElement fromDate,
                ToDateElement toDate,
                RemarksElement remarks,
                SaveChangesElement saveChanges
            ) : base(driverFacade, configs)
            {
                Title = title;
                Institute = institute;
                Degree = degree;
                Grade = grade;
                FromDate = fromDate;
                ToDate = toDate;
                Remarks = remarks;
                SaveChanges = saveChanges;
            }

            #region Add Edit Dialog Elements
            public class TitleElement : PageObjectBase
            {
                private readonly string _locator = "form-modal-title";

                public TitleElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public void WaitToLoadWithText(string expectedTitle)
                {
                    DriverFacade.WaitForWebElementTextByIdLocator(_locator, expectedTitle);
                }

                public void WaitToBeInvisible()
                {
                    DriverFacade.WaitForWebElementToBeInvisibletByIdLocator(_locator);
                }
            }
            public class InstituteElement : PageObjectBase
            {
                private readonly string _locatorDropdown = "span.select2-selection.select2-selection--single";
                private readonly string _locatorSelectedItem = "input.select2-search__field";

                public InstituteElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public void Select(string instituteName)
                {
                    DriverFacade.ClickWebElementByCssLocator(_locatorDropdown);
                    DriverFacade.SetWebElementTextByCssLocator(_locatorSelectedItem, instituteName);
                    DriverFacade.SetWebElementTextByCssLocator(_locatorSelectedItem, Keys.Enter);
                }

            }
            public class DegreeElement : PageObjectBase
            {
                private readonly string _locator = "Degree";
                public DegreeElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public string Text
                {
                    set
                    {
                        DriverFacade.ClearWebElementTextByIdLocator(_locator);
                        DriverFacade.SetWebElementTextByIdLocator(_locator, value);
                    }
                }
            }
            public class GradeElement : PageObjectBase
            {
                private readonly string _locator = "Grade";
                public GradeElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public string Text
                {
                    set
                    {
                        DriverFacade.ClearWebElementTextByIdLocator(_locator);
                        DriverFacade.SetWebElementTextByIdLocator(_locator, value);
                    }
                }
            }
            public class FromDateElement : PageObjectBase
            {
                private readonly string _locator = "FromDate";
                public FromDateElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public DateTime Value
                {
                    set
                    {
                        DriverFacade.SetWebElementValueAttributeByIdLocator(_locator, value.ToString(Configs["System:DateFormat"]));
                    }
                }
            }
            public class ToDateElement : PageObjectBase
            {
                private readonly string _locator = "ToDate";
                public ToDateElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public DateTime Value
                {
                    set
                    {
                        DriverFacade.SetWebElementValueAttributeByIdLocator(_locator, value.ToString(Configs["System:DateFormat"]));
                    }
                }
            }
            public class RemarksElement : PageObjectBase
            {
                private readonly string _locator = "Remarks";
                public RemarksElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public string Text
                {
                    set
                    {
                        DriverFacade.ClearWebElementTextByIdLocator(_locator);
                        DriverFacade.SetWebElementTextByIdLocator(_locator, value);
                    }
                }
            }
            public class SaveChangesElement : PageObjectBase
            {
                private readonly string _locator = "button.btn.green.btn-circle";
                public SaveChangesElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public void Click()
                {
                    DriverFacade.ClickWebElementByCssLocator(_locator);
                }
            }
            #endregion

            public TitleElement Title { get; }
            public InstituteElement Institute { get; }
            public DegreeElement Degree { get; }
            public GradeElement Grade { get; }
            public FromDateElement FromDate { get; }
            public ToDateElement ToDate { get; }
            public RemarksElement Remarks { get; }
            public SaveChangesElement SaveChanges { get; }

        }
        public class AddNewElement : PageObjectBase
        {
            private readonly string _locator = "a.btn.sbold.green.pull-right.btn-circle.btn-load-inner-form-modal";
            public AddNewElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

            public void Click()
            {
                DriverFacade.ClickWebElementByCssLocator(_locator);
            }
        }
        public class FilterElement : PageObjectBase
        {
            private readonly string _locator = "#qualification-table_filter > label > input";
            public FilterElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

            public void Search(string filter)
            {
                DriverFacade.SetWebElementTextByCssLocator(_locator, filter);
                DriverFacade.SetWebElementTextByCssLocator(_locator, Keys.Enter);
            }
        }
        public class InfoElement : PageObjectBase
        {
            private readonly string _locator = "qualification-table_info";

            public InfoElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

            public void WaitToLoadWithText(string expectedInfo)
            {
                DriverFacade.WaitForWebElementTextByIdLocator(_locator, expectedInfo);
            }

            public void WaitToBeInvisible()
            {
                DriverFacade.WaitForWebElementToBeInvisibletByIdLocator(_locator);
            }
        }
        public class FilteredQualificationElement : PageObjectBase
        {
            private readonly string _locator = "";

            public FilteredQualificationElement(DriverFacade driverFacade, IConfiguration configs,
                DegreeElement degree,
                GradeElement grade,
                EditElement edit) : base(driverFacade, configs) 
            {
                Degree = degree;
                Grade = grade;
                Edit = edit;
            }

            #region Filtered Qualification Element Definitions
            public class DegreeElement : PageObjectBase
            {
                private readonly string _locator = "#qualification-table > tbody > tr > td:nth-child(2)";

                public DegreeElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public string Text
                {
                    get
                    {
                        return DriverFacade.GetWebElementTextByCssLocator(_locator);
                    }
                }

                public void WaitToLoadWithText(string degree)
                {
                    DriverFacade.WaitForWebElementTextByCssLocator(_locator, degree);
                }

            }
            public class GradeElement : PageObjectBase
            {
                private readonly string _locator = "#qualification-table > tbody > tr > td:nth-child(3)";

                public GradeElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public string Text
                {
                    get
                    {
                        return DriverFacade.GetWebElementTextByCssLocator(_locator);
                    }
                }

            }
            public class EditElement : PageObjectBase
            {
                private readonly string _locator = "a.btn.btn-icon-only.blue.btn-circle.btn-load-inner-form-modal.remarkMore";
                public EditElement(DriverFacade driverFacade, IConfiguration configs) : base(driverFacade, configs) { }

                public void Click()
                {
                    DriverFacade.ClickWebElementByCssLocator(_locator);
                }
            }
            #endregion

            public DegreeElement Degree { get; }
            public GradeElement Grade { get; }
            public EditElement Edit { get; }
        }
        #endregion

        public AddEditDialogElement AddEditDialog { get; }
        public AddNewElement AddNew { get; }
        public FilterElement Filter { get; }
        public InfoElement Info { get; }
        public FilteredQualificationElement FilteredQualification { get; }

        public void Navigate(string employeeGuid)
        {
            DriverFacade.Navigate(Configs["PageUrls:EmployeeQualificationsPage"].Replace("{{EmployeeGuid}}", employeeGuid));
        }

    }
}
