#!/bin/bash

cd /var/www
sudo cp web.config /root/web.config
sudo rm SERVWeb.zip
sudo rm * -r
sudo wget https://www.dropbox.com/s/l5us91s9m752fw7/SERVWeb.zip
sudo unzip SERVWeb.zip
sudo mv SERVWeb/* ./
sudo rm *.cs
sudo rm *.csproj
sudo echo "" > SERVLog.txt
sudo chmod 777 SERVLog.txt
sudo cp /root/web.config ./web.config
rm -r __MACOSX
rm -r SERVWeb
cd /root/
/etc/init.d/apache2 restart
sudo wget http://localhost/Default.aspx
sudo rm Default.aspx
