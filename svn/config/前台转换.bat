@echo off

for %%i in (*.xlsx) do (
  echo Converting %%i to ..\..\client_CR\GameCR\Assets\Game\Config\%%~ni.csv...
  xlsx2csv\xlsx2csv -i -d ; -s 1 %%i ..\..\client_CR\GameCR\Assets\Game\Config\%%~ni.csv
)

pause