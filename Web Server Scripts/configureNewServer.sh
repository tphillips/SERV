#!/bin/bash

# this file:
# https://www.dropbox.com/s/wpwtt3zz15et32i/configureNewServer.sh
# run with
# su
# bash <(wget -qO- https://www.dropbox.com/s/wpwtt3zz15et32i/configureNewServer.sh)

apt-get update
apt-get upgrade
apt-get install nano wget apache2 mysql-server libapache2-mod-mono mono-apache-server4 mono-devel unzip postfix

# The server may hang now, do a reboot.

