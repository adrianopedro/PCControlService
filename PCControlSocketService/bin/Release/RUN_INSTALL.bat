@echo off
echo.
echo #########################################
echo ##                                     ##
echo ## ANYSOLABS EQUIPMENT CONTROL INSTALL ##
echo ##                                     ##
echo #########################################
echo.

SET /P ANSWER=Do you want to install Apache Service (Y/N)?
if /i {%ANSWER%}=={y} (goto :yesapache) 
if /i {%ANSWER%}=={yes} (goto :yesapache) 
goto :noapache
 
:yesapache
echo. 
echo ##### Installing Apache Service #####
echo.
%~dp0\binaries\httpserver\bin\httpd.exe -k install
%~dp0\binaries\httpserver\bin\httpd.exe -k start
echo Apache service install finished. 
echo.
pause
:noapache

echo.
SET /P ANSWER=Do you want to disable UAC (Y/N)?
if /i {%ANSWER%}=={y} (goto :yesdisuac) 
if /i {%ANSWER%}=={yes} (goto :yesdisuac) 
goto :nodisuac

:yesdisuac
echo.
echo ##### Disabling UAC #####
echo.
%windir%\System32\reg.exe ADD HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System /v EnableLUA /t REG_DWORD /d 0 /f
echo UAC disabled.
echo.
pause
:nodisuac

echo.
SET /P ANSWER=Do you want to change/tweak Windows Logon (Y/N)?
if /i {%ANSWER%}=={y} (goto :yeschangelogon) 
if /i {%ANSWER%}=={yes} (goto :yeschangelogon) 
goto :nochangelogon

:yeschangelogon
echo.
echo ##### Tweak Logon Window #####
echo.
::logon_setup.reg
%windir%\System32\reg.exe ADD HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System /v dontdisplaylastusername /t REG_DWORD /d 00000000 /f
%windir%\System32\reg.exe ADD HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System /v LogonType /t REG_DWORD /d 00000001 /f
echo Logon changed/tweaked
echo.
pause
:nochangelogon

exit /b 1