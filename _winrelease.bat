@SET CURPATH=%~dp0

@SET EXENAME=MapCreator

@TITLE: RELEASE Build %EXENAME% for Windows

::##########

dotnet build -c Release

@ECHO:
@ECHO: Done!
@ECHO:

@PAUSE

@CLS

::##########
