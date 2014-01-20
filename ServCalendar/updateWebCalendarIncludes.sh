#!/bin/bash

offset=0
offset1=`expr $offset + 1`
day=`date -d "$offset days"  +'%d'`
month=`date -d "$offset days" +'%b'`
year=`date -d "$offset days" +'%y'`
tday=`date -d "$offset1 days" +'%d'`
tmonth=`date -d "$offset1 days" +'%b'`
tyear=`date -d "$offset1 days" +'%y'`
pass=`cat .calPasswd`
gcalcli --nc --user serv.surrey@gmail.com --pw $pass --cal "Air Ambulance" agenda "$day $month 20$year 01:00" "$tday $tmonth 20$tyear" > /var/www/aaCalendarInclude.htm
gcalcli --nc --user serv.surrey@gmail.com --pw $pass --cal "Blood" agenda "$day $month 20$year 01:00" "$tday $tmonth 20$tyear" > /var/www/bloodCalendarInclude.htm
