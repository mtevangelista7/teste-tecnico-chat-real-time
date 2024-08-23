Feature: Authenticate User
In order to control access to real-time chat
As an authenticated user
I want to log in and receive a valid JWT token

    Scenario: Successful authentication with JWT token
        Given an existing user with access to the login endpoint
        When they provide a username "login" and password "password"
        Then the API should authenticate the user successfully