# QADemo
This is a demo project to demontrate the use of RestSharp library to access DropBox API

The endpoint that is used in this demo : 
https://api.dropboxapi.com/2/cloud_docs/get_metadata
https://api.dropboxapi.com/2/files/list_folder
https://content.dropboxapi.com/2/files/download

The dropbox api explorer https://dropbox.github.io/dropbox-api-v2-explorer/ can be used to test this endpoint manually

Framework - This has the RestSharpHelper class where we abstract all the details needed to access the endpoint

DropBoxAPI - Has all the classes that use the Framework to test the dropbox api.We can create more such folders for every new API set we are testing. Note the Models folder in this that has the models used in deserializing the response directly into an object. The tests include endpoints for get_metadata for a file, list_folder and downloading a file.

The demo uses GET, POST methods along with Bearer Authentication
