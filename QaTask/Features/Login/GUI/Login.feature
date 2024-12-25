Feature: Login User
Valid and invalid login cases for browser

@gui
    Scenario: Login-001 | Successful login with valid credentials
        Given I navigate to the login page
        When  I try to login with valid credentials
        Then  I can assert that i have been logged in

@gui
    Scenario: Login-002 | Unsuccessful login with locked user
        Given I navigate to the login page
        When  I try to login with credentials of a locked user
        Then  I can assert that the correct error message appears


@gui
    Scenario: Login-003 | Successful login and logout 
        Given I navigate to the login page
        And I try to login with valid credentials
        When I try to log out from the site
        Then I can assert that i have been logged out
