# ErgastTask

This is a C# console application designed to address the question, 'For each of the constructors in the 2023 F1 season, at which historical circuits have they performed their best?' It accomplishes this task by utilizing data available through the ERGAST API (an open-source resource).

The program consists of two main functions:
*GetConstructorIds(string): This function retrieves a list of all constructors who participated in the 2023 F1 season.
*ManageCircuitID(string): Within this function, each of the retrieved constructors is passed to another API. This function returns information about all the races in which these constructors finished in the first position (Results=1). Additionally, within the same API call, we drill down to extract the circuit names associated with these races. Subsequently, duplicate circuit names are removed, and the unique names are printed alongside the corresponding constructor ID.
This effectively fulfills the second condition of the problem statement, which requires identifying all circuits where a given constructor has finished the race in the first place.

For future improvements, consider the following:
1-Ideally, the program should delve into the drivers of each constructor, looping through all races in which they have finished with the minimum lap time, and collect the respective circuit names.
2-Although the program already handles API exceptions, implementing unit test cases for each of these functions would ensure the program performs reliably under extreme conditions.
