@SET CURPATH=%~dp0

@SET EXENAME=MapCreator

@TITLE: DEBUG Build %EXENAME% for Windows

::##########

dotnet build -c Debug

@ECHO:
@ECHO: Done!
@ECHO:

@PAUSE

@CLS

::##########
