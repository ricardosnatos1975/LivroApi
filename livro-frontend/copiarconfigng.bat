@echo off

set Workspace=%~dp0

echo %Workspace%
echo %AppData%

copy /y %Workspace%\ng.cmd %AppData%\npm