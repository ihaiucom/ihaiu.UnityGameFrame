#!/bin/sh
cd `dirname $0`
path='./Config'
echo $path
python xlsx2csv/xlsx2csv.py -i -d ';' -b -s 1 . $path
