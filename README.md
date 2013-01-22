TFS HipChat
===========

Send Team Foundation Server event notifications to HipChat.

## Getting started
1. Build the solution
2. Edit the app.config and specify your:
 * HipChat API token
 * HipChat Room ID
3. Run the executable somewhere your TFS server can access (by default it hosts the service at http://localhost:8731/TfsHipChat)
4. Configure your TFS instance to push notifications to it for the desired events (NOTE: admin access to TFS is required)

### How to: Configure your TFS instance
TFS comes with a command line tool called `BisSubscribe.exe` to configure services that TFS will push notifications to when an event occurs e.g. a check in.

Where this executable lives depends on your OS configuration and TFS version, however for a standard installation of TFS 2012 it should be located here:

    C:\Program Files\Microsoft Team Foundation Server 11.0\Tools\BisSubscribe.exe

Using this tool you can register TFS HipChat to be notified on specific events by executing the following for each event:

    BisSubscribe.exe /eventType <eventType> /address http://localhost:8731/TfsHipChat /deliveryType Soap /collection http://sometfsserver/tfs/MyCollection

Event types currently supported:
 * CheckinEvent
 * BuildCompletionEvent

NOTE: it has only been tested using TFS 2012 thus far. Links in HipChat messages will likely only work in TFS 2012 at this point.

## Contributions
To put it simply, feel free to contribute. I'm keen to see it:
 * Be much simpler to configure and choose what events you care about; potentially even provide means to automatically wire and unwire it with TFS based on the desired events
 * Support TFS 2010 and Team Foundation Service? (formerly tfspreview.com)
 * Add support for other notification types with better messages
 * Not be a quick and dirty console app --> maybe a WCF service that could easily be deployed to various PaaS providers?
