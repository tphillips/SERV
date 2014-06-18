#!/bin/bash

mono DbMetal.exe -c "Server=localhost;Database=SERV;User=root" --provider=MySql --with-dbconnection="MySql.Data.MySqlClient.MySqlConnection, MySql.Data, Version=6.5.4.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d" --code=../../SERVDataContract/DbLinq.cs --language=C# --namespace=SERVDataContract.DbLinq

tail -n +11 ../../SERVDataContract/DbLinq.cs > ../../SERVDataContract/DbLinq1.cs
mv ../../SERVDataContract/DbLinq1.cs ../../SERVDataContract/DbLinq.cs
echo "#define MONO_STRICT" > ../../SERVDataContract/DbLinq1.cs
echo "" >> ../../SERVDataContract/DbLinq1.cs
cat ../../SERVDataContract/DbLinq.cs >> ../../SERVDataContract/DbLinq1.cs
mv ../../SERVDataContract/DbLinq1.cs ../../SERVDataContract/DbLinq.cs
sed 's/SERv/SERVDB/g' ../../SERVDataContract/DbLinq.cs > ../../SERVDataContract/DbLinq1.cs
mv ../../SERVDataContract/DbLinq1.cs ../../SERVDataContract/DbLinq.cs
sed 's/Name="serv./Name="SERV./g' ../../SERVDataContract/DbLinq.cs > ../../SERVDataContract/DbLinq1.cs
mv ../../SERVDataContract/DbLinq1.cs ../../SERVDataContract/DbLinq.cs