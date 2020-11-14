IF NOT EXIST Install\ MD Install
DEL /F /S /Q Install\* || GOTO Error0

SET Config=%1%
IF [%1] == [] SET Config=Debug

NuGet.exe pack Source\Rhetos.nuspec -OutputDirectory Install || GOTO Error0
NuGet.exe pack Source\Rhetos.MSBuild.nuspec -OutputDirectory Install || GOTO Error0
NuGet.exe pack Source\Rhetos.TestCommon.nuspec -OutputDirectory Install || GOTO Error0
NuGet.exe pack CommonConcepts\Rhetos.CommonConcepts.nuspec -OutputDirectory Install || GOTO Error0

@REM ================================================

@ECHO.
@ECHO %~nx0 SUCCESSFULLY COMPLETED.
@EXIT /B 0

:Error0
@ECHO.
@ECHO %~nx0 FAILED.
@IF /I [%2] NEQ [/NOPAUSE] @PAUSE
@EXIT /B 1
