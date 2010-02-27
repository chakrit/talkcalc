@echo off
IF NOT EXIST out MKDIR out

rem Create wordnets from the grammar
bin\HParse in\calc.gram out\calc.wdnet

rem HTK requires the pronunciation file to be sorted
sort in\calc.dict > out\calc.dict
