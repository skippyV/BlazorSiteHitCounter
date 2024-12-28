Trying to use Carl Franklin's guidance from 
User Tracking in Blazor Server: Carl Franklin's Blazor Train Ep 72

https://www.youtube.com/watch?v=YWYUIjWKh7w

For some reason the CircuitHandler.OnCircuitOpenedAsync() is never called.

Within the above site is a download link for Franklin's sources, which work as expected.

However, Franklin's sources originally target an older version of Blazor - NET6.0
Note- I updated a copy of Franklin's sources to adjust the target to NET9.0, and it still worked.

This project targets NET9.0. Otherwise, this project's code is the identical to 
Franklin's sources. (Note-I did rename CircuitHandlerService to CircuitHandlingService.)

