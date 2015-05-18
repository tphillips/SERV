#!/bin/bash

# this file:
# https://www.dropbox.com/s/q0ct4enqxjoublq/configureNewServerStep2.sh
# run with
# su
# bash <(wget -qO- https://www.dropbox.com/s/q0ct4enqxjoublq/configureNewServerStep2.sh)

a2enmod mod_mono_auto
cd /root
wget https://www.dropbox.com/s/4a4mspsf3gfltxn/installSERV.sh
chmod +x installSERV.sh
echo "Remove trailing /html/ from the www path (press return)"
read bla
nano /etc/apache2/sites-available/000-default.conf
cd /var/www
wget https://www.dropbox.com/s/agjnn6fiwvyitrn/web.config
cd
echo "Edit 4.0 to be 4.5 (press return)"
read bla
nano /etc/mono-server4/mono-server4-hosts.conf
echo "Now creating database . . ."
wget https://www.dropbox.com/s/h34x1wu2kq5yeoi/SERVDB.sql
mysql -p < SERVDB.sql
rm SERVDB.sql
/etc/init.d/apache2 restart
cd
./installSERV.sh