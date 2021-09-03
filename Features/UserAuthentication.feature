Feature: User Authentication
	
Scenario: Login with correct username, password and account
	Given that I am in the login page
	When the correct username, password and account provided
	And logged in
	Then the system dashboard should be shown