Rhetos - A DSL platform
============

Rhetos is a DSL framework that enables you to create your own domain-specific language to build sever applications.
After an application developer describes the application in that language (DSL script), Rhetos will
use it to generate the application, including the application's database,
business layer object model (C#) and web API (REST and SOAP).
It also has a robust built-in system for handling upgrades and customizations.

Rhetos comes with the CommonConcepts DSL package, a language that contains
many ready-to-use features for building applications.

Rhetos is based on Microsoft .NET technology stack.

See [rhetos.org](http://www.rhetos.org/) for more information.

Installation
============

Prerequisites
-----------------

* .NET framework 4.0 and installed IIS or IIS Express.
* Admin access to Microsoft SQL Express (version 2008 or above), Microsoft SQL Server or Oracle Database.

Basic information
-----------------

There are two important scripts in this project that enables Rhetos server and
give you jump start. One of them is `Build.bat` that builds source and places
some binaries in right place in directory hierarchy. Second one is 
`SetupRhetosServer.bat` that will attempt to set up a development environment.
It requires few parameters based on which it sets up database, configures 
Rhetos server and sets up IIS Express local config which is enough to start Rhetos
server and developing.

So, there are two steps to install and run Rhetos server:

    > Build.bat
    > SetupRhetosServer.bat <WebsiteName> <Port> <SqlServer> <DatabaseName>

SetupRhetosServer.bat arguments:

* WebsiteName - name of website in IISExpress config
    (not directly related to URL of Rhetos web interface)
* Port - port that Rhetos web interface will be listening to if using IIS Express
    (1234, for example)
* SqlServer - Rhetos requires database for data store, configuration, etc.
* DatabaseName - name of database that will Rhetos use, script will create
              database if it doesn't exist.               

Directory hierarchy
-------------------

You can check out source in any folder you like. It might be weird that above 
mentioned essential scripts aren't both in root folder. Only `Build.bat` is.
Reason lies in fact that after you build complete solution, it is not 
necessary to build it again. In other words, Rhetos server can be run as
website in IIS Express or IIS, and for that only subfolder Rhetos of Source is
required. Therefore `SetupRhetosServer.bat` script which sets up development 
environment is placed inside that folder. Following is directory hierarchy:

    * Rhetos
     \
      * CommonConcepts
      * External
      * Source
       \
        * Rhetos
         \
          Rhetos.csproj
          ApplyPackages.bat
          SetupRhetosServer.bat
          ...
      Build.bat
      ...
      Rhetos.sln
      ...
      Readme.md
      ...

Development environments
--------------------
As you may have read before, Rhetos can run on IIS/IIS Express. While IIS is 
much more configurable, `SetupRhetosServer.bat` prepares config for IIS Express.
Main reason is that IIS Express is very light, but has enough functionality for
Rhetos server. Same applies to database.

If one wants to use IIS instead of IIS Express or manually change database it is
possible and more information about Rhetos configuration can be found in this
readme and on [rhetos.org](http://www.rhetos.org/).

Configuring your project
------------------------
For basic usage or testing if Rhetos works, just run `SetupRhetosServer.bat`.
After running this script and before running Rhetos server, you can manually
configure `IISExpress.config`. It is template config for IIS Express site
with modified `<sites>` section and added `<location>` part at the end of config.
Those two defines which port on localhost Rhetos will be listening and that
security used is based on WindowsAuthentication.

If one prefers to use IIS following steps are required:

* Create ApplicationPool of .NET Framework v4.0
    * check "Enable 32-Bit Application"
    * set user with admin privileges as Identity
* Create new Website or use existing one
* Create new Application with following:
    * set ApplicationPool to one created in step one
    * set Physical path to Rhetos\Source\Rhetos folder
    * set Path credentials to user with admin privileges
    * disable directory browsing
    * in Authentication enable Windows authentication and disable others
* Set URL path for `<client> <endpoint address>` in 
    Rhetos\Source\Rhetos\Web.config accordingly

If one wants to use different database it is defined in ConnectionStrings.config
   in Rhetos\Source\Rhetos\bin folder.
   
Note that besides configuring IIS and Database, `SetupRhetosServer.bat` also
deploys Common and user defined packages to that database. If one chooses
manual setup it is necessary to run ApplyPackages.bat in Rhetos\Source\Rhetos.

Run Rhetos server
----------------
Once configured, Rhetos server can be started by running following command
in cmd while positioned in Rhetos\Source\Rhetos:

    > CALL "C:\Program Files (x86)\IIS Express\IISExpress.exe" /config:IISExpress.config

**Note:** Use the "Program Files" folder on 32-bit system.

If using IIS then just start site in IIS manager. 

License
============

The code in this repository is licensed under version 3 of the AGPL unless
otherwise noted.

Please see `LICENSE.txt` for details.

How to contribute
============

Contributions are very welcome. The easiest way is to fork this repo, and then
make a pull request from your fork. The first time you make a pull request, you
may be asked to sign a Contributor Agreement.
