#!/bin/bash

# this file:
# https://www.dropbox.com/s/zf6nnktno6gpt9y/configureNewServer.sh
# run with
# su
# bash <(wget -qO- https://www.dropbox.com/s/zf6nnktno6gpt9y/configureNewServer.sh)

apt-get update
apt-get upgrade
apt-get install apache2 mysql-server libapache2-mod-mono mono-apache-server4 mono-devel unzip postfix

# The server may hang now, do a reboot.

