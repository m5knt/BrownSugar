@echo off
pushd %~dp0
for /f "usebackq" %%t in (`dir /b /s *.sln 2^>nul`) do (
	set "SOLUTION=%%t"
)
::if "%SOLUTION%"=="" (
::	echo "Solution not found"
::	exit /b 1
::)
set "DEVENV=%VS140COMNTOOLS%../ide/devenv"
echo Visual Studio : "%DEVENV%"
echo Solution : "%SOLUTION%"
start "" "%DEVENV%" "%SOLUTION%" %*
ping -n 1 -w 5000 1.1.1.1 >nul
