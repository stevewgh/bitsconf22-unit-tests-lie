Feature: Hudl Weather
A weather app for me! ☀️🌧❄️.

    Scenario: Weather conditions are shown on demand
        Given the user selected the <location> option
        Then the location name <location name> is displayed

    Examples:
      | location | location name |
      | home     | Home          |
      | office   | Office        |
      | vacation | Vacation      |


    Scenario: Weather conditions are shown on demand using Antioch approach
        Given I run the test "tests/home.json"
        Then the location name Home is displayed