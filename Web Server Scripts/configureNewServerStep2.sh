#!/bin/bash

# this file:
# https://www.dropbox.com/s/ovtjbp0kibuuggh/configureNewServerStep2.sh
# run with
# su
# bash <(wget -qO- https://www.dropbox.com/s/ovtjbp0kibuuggh/configureNewServerStep2.sh)

a2enmod mod_mono_auto
cd /root
wget https://www.dropbox.com/s/8ri6v8l5g9pw18e/installSERV.sh
chmod +x installSERV.sh
echo "Remove trailing /html/ from the www path (press return)"
read bla
nano /etc/apache2/sites-available/000-default.conf
cd /var/www
wget https://www.dropbox.com/s/ackxiiqszytm5lz/web.config
cd
echo "Edit 4.0 to be 4.5 (press return)"
read bla
nano /etc/mono-server4/mono-server4-hosts.conf
echo "Now creating database . . ."
wget https://www.dropbox.com/s/13anr2svmcuqwv1/SERVDB.sql
mysql -p < SERVDB.sql
rm SERVDB.sql
/etc/init.d/apache2 restart
cd
./installSERV.sh