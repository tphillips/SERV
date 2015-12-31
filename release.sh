#!/bin/bash

# relies on node.js and zip

echo "MAKE SURE YOU DID A _RELEASE_ BUILD"
read version

cd SERVWeb/js
rm -f JS*min.js
rm -f opsMap*min.js
rm -f Calendar*min.js
rm -f ControllerLog*min.js
echo "Enter the version number (MUST match the version in SERVGlobal.cs): "
read version
echo OK, minifying to version $version
uglifyjs JS.js -o JS.$version.min.js
uglifyjs Calendar.js -o Calendar.$version.min.js
uglifyjs opsMap.js -o opsMap.$version.min.js
uglifyjs ControllerLog.js -o ControllerLog.$version.min.js
cd ..
cd css
cp style.css style.$version.css
cd ..
cd ..
Echo zipping and archiving release
rm SERVWeb.zip
zip -r -9 SERVWeb.zip SERVWeb
cp SERVWeb.zip RelArchive/SERVWeb.$version.zip
echo removing minified JS files . . .
cd SERVWeb/js
rm -f JS*min.js
rm -f opsMap*min.js
rm -f Calendar*min.js
rm -f ControllerLog*min.js
cd ..
cd css
rm style.$version.css
#echo To go LIVE - Wait for Dropbox to sync and press return or to ABORT ctrl+c. . .
#read bla
#ssh -i ~/Dropbox/Dev/Resources/AmazonAWSKeys/SERV-EU.pem ubuntu@system.servssl.org.uk "sudo /root/installSERV.sh"
echo Done . . .
