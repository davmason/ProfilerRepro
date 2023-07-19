@echo off
setlocal

set CORECLR_ENABLE_PROFILING=1
set CORECLR_PROFILER={cf0d821e-299b-5307-a3d8-b283c03916dd}
set CORECLR_PROFILER_PATH=%~dp0\CorProfiler.dll

rem if not exist %~dp0\runtime\corerun.exe (
rem     echo runtime\CoreRun.exe does not exist, you have to copy it from your local coreclr build!
rem     goto :EOF
rem )

:: C:\dbg\x64\windbg
rem %~dp0\runtime\corerun.exe
%~dp0\sampleapp.exe