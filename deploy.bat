@echo off

chcp 65001

SET BASH_PATH=%SYSTEMDRIVE%\Users\%USERNAME%\.babun\cygwin\bin\
SET PATH=%PATH%;%BASH_PATH%

del /Q /S "\\DS\NET Disk L\Release\jstp\"

cp .\README.MD "\\DS\NET Disk L\Release\jstp\"
xcopy /s /y .\bin\publish "\\DS\NET Disk L\Release\jstp\"



