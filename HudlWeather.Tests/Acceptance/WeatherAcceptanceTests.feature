Feature: Hudl Weather
A weather app just for me! โ๏ธ๐งโ๏ธ.

    Scenario: Weather conditions are shown on demand
        Given the user selected the <location> option
        Then the location name <location name> is displayed

    Examples:
      | location | location name |
      | home     | Home          |
      | office   | Office        |
      | vacation | Vacation      |