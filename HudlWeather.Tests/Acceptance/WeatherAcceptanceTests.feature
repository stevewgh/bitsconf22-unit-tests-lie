Feature: Hudl Weather
A very user specific weather app.

Scenario: Weather conditions at home are shown first
    When the default page is shown
    Then the home weather conditions are displayed
    
Scenario: Weather conditions at work are shown on demand
    When the default page is shown
    Then the home weather conditions are displayed