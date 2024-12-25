Feature: Complete a purchase on the site

    Background:
        * I navigate to the login page
        * I try to login with valid credentials
@gui
    Scenario: Purchase - 001  |  Purchase an item
        Given i add an item to my cart to purchase
        When i navigate to the cart to checkout my purchase
        And i complete my personal information
        And overview my order
        Then i can assert that my order has been placed
@gui
    Scenario: Purchase - 002 | Attempt to purchase without providing personal information
        Given i add an item to my cart to purchase
        When i navigate to the cart to checkout my purchase
        And I skip filling in my personal information
        Then I should see error messages indicating that personal information is required for each field
@gui
    Scenario: Purchase - 003 | Purchase multiple items and verify total price
        Given I add multiple items to my cart
        When i navigate to the cart to checkout my purchase
        And i complete my personal information
        Then I can assert that the total price matches the sum of the items' prices
        And i can assert that my order has been placed