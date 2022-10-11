#!/bin/bash

if [ $# != 2 ]; then
  echo 'usage: sh getcommits.sh First-Name Last-Name' >&2
  exit 1
fi

#read name
str="$1"
str=\"$str\"
str2="$2"
str2=\"$str\"
rm commits.txt
for i in *
do

  if [ -d "$i" ]; then
        cd "$i"
        for dir2 in *; do
            if [ "${dir2: -3}" == ".cs" ] 
            then
            echo "git blame -w $i/$dir2 | grep -e $1 -e $2 >> ../commits.txt"
            git blame -w "$dir2" | grep -e "$1" -e "$2" >> ../commits.txt
            fi
        done
        cd ..
    else
    if [ "${i: -3}" == ".cs" ]
    then
      echo "git blame -w $i | grep -e $1 -e $2 >> commits.txt"
      git blame -w $i | grep -e $str -e $str2 >> commits.txt
    fi
    fi
done