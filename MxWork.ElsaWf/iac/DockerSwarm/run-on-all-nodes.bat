@echo off

title This runs the command you passed as the argument!
echo The command you passed is as follows.
REM echo %~1
set command=%~1
echo %command%
REM The following is for empty line in the output. These also should work. echo( and echo[
echo.
SETLOCAL ENABLEDELAYEDEXPANSION

FOR %%A IN (201 211 212 213) DO (
	REM ECHO %%A
	set DOCKER_HOST=192.168.99.%%~A
	ECHO !DOCKER_HOST!
	%command%
	echo.
)

REM pause