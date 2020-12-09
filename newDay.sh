#!/bin/bash

year=$1
day=$(printf "%02d" $2)

dayName=Day${day}

eventName=Event${year}

dotnet new console -o "${eventName}.${dayName}" -f netcoreapp3.1

cat ./Templates/Program.cs | sed "s/Day00/${dayName}/g" | sed "s/Event0000/${eventName}/g" > ./${eventName}.${dayName}/Program.cs 
cat ./Templates/Day00.cs | sed "s/Day00/${dayName}/g" | sed "s/Event0000/${eventName}/g" > ./${eventName}.${dayName}/${dayName}.cs
cat ./Templates/Day00.csproj | sed "s/Day00/${dayName}/g" | sed "s/Event0000/${eventName}/g" > ./${eventName}.${dayName}/${eventName}.${dayName}.csproj
cp -rf  ./Templates/Input ./${eventName}.${dayName}  

aoc in --year $1 --day $2 > ./${eventName}.${dayName}/Input/part1.txt

dotnet sln add ./${eventName}.${dayName}  

