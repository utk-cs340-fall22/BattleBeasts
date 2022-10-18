#!/bin/bash

rm commits.txt
for i in *; do
  if [ -d "$i" ]; then
        cd "$i"
        for dir2 in *; do
            if [[ $dir2 == *.cs ]]; then
	      if [ $# == 2 ]; then  
		echo "git blame -w $i/$dir2 | grep -e $1 -e $2 >> ../commits.txt"
		git blame -w "$dir2" | grep -e $1 -e $2 >> ../commits.txt	
      	      else
		echo "git blame -w $i/$dir2 | grep $1 >> ../commits.txt"
		git blame -w "$dir2" | grep $1 >> ../commits.txt
      	      fi
            fi
        done
        cd ..
    else
    if [[ $i == *.cs ]]; then
      if [ $# == 2 ]; then
        echo "git blame -w $i | grep -e $1 -e $2 >> commits.txt"
	git blame -w $i | grep -e $1 -e $2 >> commits.txt
      else
	echo "git blame -w $i | grep $1 >> commits.txt" 
	git blame -w $i | grep $1 >> commits.txt
      fi
    fi
  fi
done
