@echo off
chcp 65001

SET BASH_PATH=%SYSTEMDRIVE%\Users\%USERNAME%\.babun\cygwin\bin\
SET PATH=%PATH%;%BASH_PATH%

SET MSBBUILD="C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\msbuild.exe"



whoami

git submodule update --init --recursive

rem "tools/nuget.exe" config -set http_proxy=""

"tools/nuget.exe" restore -verbosity quiet src

zsh tools/update_version.sh ./src/Properties/AssemblyInfo.cs

del /Q /S "bin/release/"
del /Q /S "bin/publish/"

%MSBBUILD% /consoleloggerparameters:ErrorsOnly /maxcpucount /nologo /verbosity:quiet src/jstp.sln /t:Build /p:Configuration=Release 

set BUILD_STATUS=%ERRORLEVEL%
if %BUILD_STATUS%==0 echo "======== Build success ==========="
if not %BUILD_STATUS%==0 echo "=======! Build failed !==========" && exit /b 1

zsh "tools/merge_release.sh" ./bin/release/jstp.exe ./bin/publish/jstp.exe

