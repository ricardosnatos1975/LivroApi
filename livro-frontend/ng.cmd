@IF EXIST "%~dp0\node.exe" (
  "%~dp0\node.exe" --max_old_space_size=12288 "%~dp0\node_modules\@angular\cli\bin\ng" %*
) ELSE (
  @SETLOCAL
  @SET PATHEXT=%PATHEXT:;.JS;=;%
  node --max_old_space_size=12288 "%~dp0\node_modules\@angular\cli\bin\ng" %*
)