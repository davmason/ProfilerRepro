@echo off
setlocal

if not defined BuildOS (
    set BuildOS=Windows
)

if not defined BuildArch (
    set BuildArch=x64
)

if not defined BuildType (
    set BuildType=Debug
)

if not defined CORECLR_PATH (
    set CORECLR_PATH=D:/git/runtime/src/coreclr
)

if not defined CORECLR_BIN (
    set CORECLR_BIN=D:/git/runtime/artifacts/bin/coreclr/%BuildOS%.%BuildArch%.%BuildType%
)

set VS_ENT_CMD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\Tools\VsDevCmd.bat"
set VS_COM_CMD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

if not defined VS_CMD_PATH (
    if exist %VS_ENT_CMD_PATH% (
        set VS_CMD_PATH=%VS_ENT_CMD_PATH%
    ) else if exist %VS_COM_CMD_PATH% (
        set VS_CMD_PATH=%VS_COM_CMD_PATH%
    ) else (
        echo No VS developer command prompt detected!
        goto :EOF
    )
)

echo   CORECLR_PATH : %CORECLR_PATH%
echo   BuildOS      : %BuildOS%
echo   BuildArch    : %BuildArch%
echo   BuildType    : %BuildType%
echo   VS PATH      : %VS_CMD_PATH%

echo   Building

if not exist bin\ (
    mkdir bin
)

pushd bin

cmake -G "Visual Studio 17 2022" ..\ -DCMAKE_BUILD_TYPE=Debug

echo Calling VS Developer Command Prompt to build
call %VS_CMD_PATH%

msbuild /v:m /m CorProfiler.sln

popd

echo.
echo.
echo.
echo Done building

echo Copying binary to main directory
copy /y  bin\Debug\CorProfiler.dll .

