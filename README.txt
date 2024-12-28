Trying to use Carl Franklin's guidance from 
User Tracking in Blazor Server: Carl Franklin's Blazor Train Ep 72

https://www.youtube.com/watch?v=YWYUIjWKh7w

For some reason the CircuitHandler.OnCircuitOpenedAsync() is never called.

Within the above site is a download link for Franklin's sources, which work as expected.

However, Franklin's sources originally target an older version of Blazor - NET6.0
Note- I updated a copy of Franklin's sources to adjust the target to NET9.0, and it still worked.

This project targets NET9.0. Otherwise, this project's code is the identical to 
Franklin's sources. (Note-I did rename CircuitHandlerService to CircuitHandlingService.)

==================
Further testing demonstrated the problem stemmed from the ServerPrerendered mode.

default NET6 web server project includes this in _Host.cshtml:
			<component type="typeof(App)" render-mode="ServerPrerendered" />
			
Franklin's version (which he pointed out in his video tutorial) was changed to:
			<component type="typeof(App)" render-mode="Server" />

In NET9/8 to turn off server prerendering for the entire app, adjust App.razor
by turning the prerender flag off. I.e.
 
   <Routes @rendermode="new InteractiveServerRenderMode(prerender: false)" />
   and
   <HeadOutlet @rendermode="new InteractiveServerRenderMode(prerender: false)" />
   
https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0#prerendering