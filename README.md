#spike.mef.plugins

This is the sample project for the following [blog post](http://blog.emirosmanoski.mk/Mef-Plugin-Loading/). 

## Important Things to note regarding Startup

- The client application specifies the C:/external_plugins folder as the plugin location
	- The plugins will have to be build from source and placed there with the config.xml files in separate folders.
- The config.xml files point to URLs of the 'Internal' APIs.
	- Make sure the addresses match to what you have running.
- Make sure the Internal APIs are up and running somewhere (IIS Express for example)

## Testing it out

The general use case is to start all the Web Api Projects. The public facing API defines an entry point that can be used to utilize the plugin manager and load up the plugins.

This is a sample request made to the public API which loads up plugin A:

*http://localhost:62060/api/public?requestIdentifier=ApiBPlugin*

The public API also defines the folder where its expecting the plugins to be located at the 
`PublicController`


