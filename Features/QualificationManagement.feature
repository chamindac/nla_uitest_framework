Feature: Qualification Management
	
@login
Scenario: Add qualification
	Given that I am in the qualifcation page of an employee
	And opened the qualification add dialog
	When the qualification is added with Institute, Qualification, Grade and Period
	Then the qualification should be shown in the list of qualifications

@login
Scenario: Modify qualification grade
	Given that I am in the qualifcation page of an employee
	And and selected a qualification to modify
	When the qualification grade is modified
	Then the modified qualification grade should be shown in the list of qualifications