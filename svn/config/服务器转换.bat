@echo off

for %%i in (*.xlsx) do (
  echo Converting %%i to .\Config\%%~ni.csv...
  xlsx2csv\xlsx2csv -i -d ; -s 1 %%i \Config\%%~ni.csv
)

pause