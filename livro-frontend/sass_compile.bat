ECHO Compilando theme-tema.scss

call sass %~dp0src\assets\theme\theme-tema.scss:%~dp0src\assets\theme\theme-tema.css

ECHO theme-tema.css Gerado

ECHO Compilando layout-tema.scss

call sass %~dp0src\assets\layout\css\main-layout.scss:%~dp0src\assets\layout\css\main-layout.css

ECHO layout-tema.css Gerado

call rd .sass-cache /s/q

ECHO Compilacao SASS Concluida
