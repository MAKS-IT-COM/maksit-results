@echo off
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Generate-CoverageBadges.ps1"
pause
