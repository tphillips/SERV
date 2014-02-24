#!/bin/bash

sudo wget https://www.dropbox.com/s/l5us91s9m752fw7/SERVWeb.zip
sudo unzip SERVWeb.zip
sudo mv SERVWeb/js/* /var/www/js/
rm SERVWeb.zip
rm -r SERVWeb
rm -r __MACOSX
sudo wget http://localhost/Default.aspx
sudo rm Default.aspx
