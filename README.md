TFS HipChat
===========

Send Team Foundation Server event notifications to HipChat.

## Getting started
TFS HipChat can be setup as a console app or as a windows service.

### Console app
1. Build the solution
2. Copy `SampleConfig.json` to `Config.json` and set your preferences
3. Run the executable somewhere your TFS server can access (by default it hosts the service at http://localhost:8731/TfsHipChat)
4. Configure your TFS instance to push notifications for the desired events

### Windows service
1. Build the solution
2. Copy `SampleConfig.json` to `Config.json` and set your preferences
3. Grant access to `NETWORK SERVICE` to register the url (requires elevated prompt):

        netsh http add urlacl url=http://+:8731/TfsHipChat user="NETWORK SERVICE"

4. Grant `NETWORK SERVICE` read access to the TFS HipChat files
5. Install the windows service (requires elevated prompt)

        TfsHipChat.WindowsService.exe /i    # run to install
        TfsHipChat.WindowsService.exe /u    # run to uninstall

6. Configure your TFS instance to push notifications for the desired events

NOTE: The windows service sends errors to the windows event log and not to the console.

### How to: Configure your TFS instance
#### Using the web UI
Notifications in TFS 2012 and [Team Foundation Service](http://tfs.visualstudio.com) can easily be configured through the web interface.

1. Sign in and browse to any team project
2. Click the profile link (top right) and select "My Alerts"
3. Click "Advanced Alerts Management"
4. Configure the events you want a notification for, ensuring you set:
 * Format: `SOAP`
 * Send To: the TfsHipChat endpoint e.g. `http://localhost:8731/TfsHipChat`

#### Using the command line utility
TFS comes with a command line tool called `BisSubscribe.exe` to configure services that TFS will push notifications to when an event occurs e.g. a check in.

Where this executable lives depends on your OS configuration and TFS version, however for a standard installation of TFS 2012 it should be located here:

    C:\Program Files\Microsoft Team Foundation Server 11.0\Tools\BisSubscribe.exe

Using this tool you can register TFS HipChat to be notified on specific events by executing the following for each event:

    BisSubscribe.exe /eventType <eventType> /address http://localhost:8731/TfsHipChat /deliveryType Soap /collection http://sometfsserver/tfs/MyCollection

### What is currently supported?
#### Event types
 * CheckinEvent
 * BuildCompletedEvent (also BuildCompletionEvent / BuildCompletionEvent2)

#### TFS Versions
 * TFS 2010
 * TFS 2012
 * Team Foundation Service

Might work with earlier versions; haven't tested them.

## Contributions
To put it simply: feel free to contribute!

Submit any issues and/or pull requests following github's pull request guide (https://help.github.com/articles/using-pull-requests)
