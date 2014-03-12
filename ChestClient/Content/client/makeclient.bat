set buildbin="..\..\..\ChessConsole\bin\Release\"

copy /y %buildbin%"ChessConsole.exe" "PlayChess.exe"
copy /y %buildbin%"Newtonsoft.Json.dll" .
copy /y %buildbin%"Protocol.dll" .

7z a ChessClient.zip *.exe *.dll
7z d ChessClient.zip 7z.*

pause