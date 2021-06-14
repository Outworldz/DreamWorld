
rem Repair performance counters
c:
cd c:\windows\system32
lodctr /R
cd c:\windows\sysWOW64
lodctr /R
WINMGMT.EXE /RESYNCPERF
net stop pla
net start pla
net stop wmiApSrv
net start wmiApSrv

       