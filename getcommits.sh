#!/bin/bash

rm commits.txt
for i in *; do
  if [ -d "$i" ]; then
        cd "$i"
        for dir2 in *; do
            if [[ $dir2 == *.cs ]]; then
	      if [ $# == 2 ]; then  
		echo "git blame -w $i/$dir2 | grep -e $1 -e $2 >> ../commits.txt"
                if git blame --since=2.weeks -w "$dir2" | grep -q $1; then
                  echo "\n$dir2\n" >> ../commits.txt
                fi
		git blame --since=2.weeks -w "$dir2" | grep -e $1 -e $2 >> ../commits.txt	
      	      else
		echo "git blame -w $i/$dir2 | grep $1 >> ../commits.txt"
                if git blame --since=2.weeks -w "$dir2" | grep -q $1; then
                  echo "\n$dir2\n" >> ../commits.txt
                fi
		git blame --since=2.weeks -w "$dir2" | grep $1 >> ../commits.txt
      	      fi
            fi
        done
        cd ..
    else
    if [[ $i == *.cs ]]; then
      if [ $# == 2 ]; then
        echo "git blame -w $i | grep -e $1 -e $2 >> commits.txt"
        if git blame --since=2.weeks -w $i | grep -q $1; then
          echo "\n$i\n" >> commits.txt
        fi
	git blame --since=2.weeks -w $i | grep -e $1 -e $2 >> commits.txt
      else
	echo "git blame -w $i | grep $1 >> commits.txt"
        if git blame --since=2.weeks -w $i | grep -q $1; then
          echo "\n$i\n" >> commits.txt 
	fi
        git blame --since=2.weeks -w $i | grep $1 >> commits.txt
      fi
    fi
  fi
done
