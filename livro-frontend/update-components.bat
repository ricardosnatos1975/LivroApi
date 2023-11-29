IF NOT EXIST %~dp0\node_modules call npm install

call npm install C:\Java\npm\frontend-components-1.0.74.tgz
IF %ERRORLEVEL% NEQ 0 GOTO ERROR

GOTO FIM

:ERROR

echo ERRO encontrado na compilacao
pause

:FIM

