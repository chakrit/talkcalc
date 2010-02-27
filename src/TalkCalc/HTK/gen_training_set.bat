@echo off
IF NOT EXIST train MKDIR train
bin\HSGen -l -n 200 out\calc.wdnet out\calc.dict > train\prompts.txt