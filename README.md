# QA Analyst | Course 2 
## API Testing Assignment 

## #2: Automated API Testing: C# 
Choose a public API (this GitHub page is an excellent resource) and create automated tests for it in C#. 
You may use any Nuget packages, unit testing frameworks, or assertion libraries you choose, but your solution must be in C#. 
Create as many automated tests as required in order to complete all of the following criteria: 

- [x] 1. Test at least 3 different HTTP methods. 
  - Tested `GET`, `POST`, `PUT`, `PATCH`, and `DELETE` for JSON Placeholder API 
  - Base URL: `https://jsonplaceholder.typicode.com/`
- [x] 2. Include at least one test for an endpoint that uses query string parameters.
  - `Test JSONPlaceholder_GET_Params()` uses query params on `/comments?postId=1`
- [x] 3. Include at least one test for an endpoint that requires a request body.
  - `Test JSONPlaceholder_POST()` sends a request body for a new Post 
- [x] 4. Include at least one negative test.
  - `Test JSONPlaceholder_Negative_Invalid_Method()` uses incorrect `DELETE` method on `/posts` endpoint

Build your code and run your tests. All tests should run and all assertions pass in order to receive credit. 
If your code requires any setup in order to build or run your tests, include a readme file with instructions to build and run your code. 
Check your completed tests into a GitHub repository. 
