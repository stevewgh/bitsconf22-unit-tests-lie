Feature: Hudl Weather
A weather app for me! ☀️🌧❄️.

    Scenario: Weather conditions at home are shown first
        When the default page is shown
        Then the home weather conditions are displayed
        Then the location name Home is displayed

    Scenario: Weather conditions are shown on demand
        Given the user selected the <location> option
        Then the location name <location name> is displayed

    Examples:
      | location | location name |
      | home     | Home          |
      | office   | Office        |
      | vacation | Vacation      |