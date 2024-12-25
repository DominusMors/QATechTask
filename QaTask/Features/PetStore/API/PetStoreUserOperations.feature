Feature: Pet Store User Operations
Manage users in the pet store system

    @api
    Scenario: Get User by Username
        Given I have a user with the following data
          | id     | username | firstName | lastName | email                | password | phone    | userStatus |
          | 394545 | user1    | John      | Gewrgiou | johngewrg@tester.com | test1234 | 34663453 | 0          |
        When I request the user "user1 " from the API
        Then the response should contain user data for "user1"

    @api
    Scenario: Delete a user by username
        Given I have a user with the following data
          | id     | username   | firstName | lastName | email                | password | phone    | userStatus |
          | 394545 | Testuser02 | John      | Gewrgiou | johngewrg@tester.com | test1234 | 34663453 | 0          |
        When I delete the user "Testuser02" from the API
        Then the response should confirm the user "Testuser02" is deleted

    @api
    Scenario: Create User and Verify Creation
        When I create a user with the following data
          | id     | username   | firstName | lastName | email                | password | phone    | userStatus |
          | 394545 | Testuser02 | John      | Gewrgiou | johngewrg@tester.com | test1234 | 34663453 | 0          |
        Then assert that the user "Testuser02" has been created

    @api
    Scenario: Update user details
        Given I have a user with the following data
          | id     | username   | firstName | lastName | email                | password | phone    | userStatus |
          | 394545 | Testuser02 | John      | Gewrgiou | johngewrg@tester.com | test1234 | 34663453 | 0          |
        When I update the user "Testuser02" with the following data
          | id     | username   | firstName | lastName  | email                 | password | phone    | userStatus |
          | 394545 | Testuser02 | Maria     | Vasilikou | mariavasik@tester.com | test1234 | 99463453 | 0          |
        Then the response should confirm the user details are updated as follows
          | id     | username   | firstName | lastName  | email                 | password | phone    | userStatus |
          | 394545 | Testuser02 | Maria     | Vasilikou | mariavasik@tester.com | test1234 | 99463453 | 0          |