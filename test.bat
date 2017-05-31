SET TOOL="src/packages/xunit.runner.console.2.1.0/tools/xunit.console.exe"

chcp 65001

%TOOL% test/Net.Shell.Test/bin/Release/Net.Shell.Test.dll || exit /b 1

