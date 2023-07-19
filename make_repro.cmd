@echo off
setlocal

echo.
echo Building test app (sampleapp.exe)

pushd sampleapp
dotnet publish -r win-x64 -c release
popd

if exist repro\ (
    rmdir /s /q repro
)

mkdir repro

rem echo.
rem echo Building profiler library

rem call build.cmd

rem echo.
rem echo Copying CorProfier.dll to repro folder
rem copy CorProfiler.dll repro\

pushd repro

mkdir runtime

echo.
echo Copying repro.cmd to repro folder
copy ..\src\windows\repro.cmd .

echo.
echo Copying published files to runtime folder
xcopy /y /e /q ..\sampleapp\bin\release\net7.0\win-x64\publish\* .

if ["%~1"]==[""] (
    echo Did not pass a path to a coreclr repo, skipping copying private bits
    goto :EOF
)

echo Copying coreclr from %1*
xcopy /y /e /u /q %1* .

echo Copying CorProfiler.dll
copy /y ..\CorProfiler.dll .

popd