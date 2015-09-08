# RiskApplication
A simple risk application tech challenge.

This application is meant to model a business case from the betting industry. 
More precisely, it is a risk assesment application that shows the user 2 pages:
1. Unsettled bets page - which displays all the bets not yet decided, and a minimal risk analysis on each bet. In grid format.
2. History page - which displays statistical information about the customers in grid format. 
  - Here you can see the number of bets the customer had so far with the company, how many were wins and what is the average stake.
  - If you click any row in the grid, a list of all the clients bets are displayed in a grid below this one.
  
  
How to use the app:
1. In the app_folder we expect 2 files: Unsettled.csv and Settled.csv. 
2. They must contain a header, or else we skip the first row.
3. The values are always expected to be in integger format and there must be 5 cells per row. Failing to comply to this will
mean that the line is skipped.
4. As a developer you may want to open the solution in VS IDE 2013 and run it with Ctrl+F5. Apart from the existence of the files
, which currenlty are there, no settings are required.
