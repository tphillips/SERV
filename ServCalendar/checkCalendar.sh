#!/bin/bash

function checkCover
{

	offset=$1
	offset1=`expr $offset + 1`
	day=`date -d "$offset days"  +'%d'`
	month=`date -d "$offset days" +'%b'`
	year=`date -d "$offset days" +'%y'`
	tday=`date -d "$offset1 days" +'%d'`
	tmonth=`date -d "$offset1 days" +'%b'`
	tyear=`date -d "$offset1 days" +'%y'`

	#echo Checking cover for $day $month $year - 1st second of $tday $tmonth $tyear
	
	pass=`cat .calPasswd`

	gcalcli --user serv.surrey@gmail.com --pw $pass --cal "Air Ambulance" agenda "$day $month 20$year 01:00" "$tday $tmonth 20$tyear" > .riders

	night1=$(checkForCover "Night" 1)
	night2=$(checkForCover "Night" 2)
	day1=$(checkForCover "Day" 1)
	day2=$(checkForCover "Day" 2)
	day3=$(checkForCover "Day" 3)

	if [[ "$night1" -eq 0 ]]
	then
		if [[ "$night2" -eq 0 ]]
		then
			echo NO NIGHT 1 or 2 COVER ON $day $month $year. 
		fi
	fi

	if [[ "$day1" -eq 0 ]]
	then
		if [[ "$day2" -eq 0 ]]
		then
			if [[ "$day3" -eq 0 ]]
			then
				echo NO DAY 1, 2 or 3 COVER ON $day $month $year. 
			fi
		fi
	fi

}

function checkForCover
{
	nocover=`cat .riders | grep "$1 $2" | grep "!"`
	if [ "$nocover" != "" ]
	then
		echo 0
	else
		echo 1
	fi
}

rm .cover -f

for i in 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40
do
	checkCover $i >> .cover
done

cover=`cat .cover`

if [ "$cover" != "" ]
then

	echo > .cover
	echo "Dear Air Ambulance Volunteers," >> .cover
	echo >> .cover
	echo "Here is the output of my AA calendar analysis for the next few weeks:" >> .cover
	echo >> .cover

	echo $cover >> .cover

	echo >> .cover
	echo "If you can cover a slot, please let Tristan Phillips know by replying to this forum post." >> .cover
	echo >> .cover
	echo "Thanks in advance," >> .cover
	echo >> .cover
	echo "Calendar Bot" >> .cover

else
	echo "All covered!" > .cover
fi

mail -s "Air Ambulance Cover Analysis" "Tris.Phillips@gmail.com" < .cover

