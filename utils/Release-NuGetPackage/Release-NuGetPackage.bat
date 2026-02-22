@echo off
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Release-NuGetPackage.ps1"
pause