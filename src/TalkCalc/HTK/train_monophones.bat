@echo off
IF NOT EXIST out MKDIR out
IF NOT EXIST out\mfc MKDIR out\mfc
IF NOT EXIST out\am MKDIR out\am
IF NOT EXIST out\am\hmm_0 MKDIR out\am\hmm_0

CALL compile.bat

rem Extract audio parameters via HCopy
bin\HCopy -T 1 -C in\code.config -S in\code.scp

rem Train the monophones
bin\HCompV -T 1 -C in\train.config -f 0.01 -m -S in\train.scp -M out\am\hmm_0 in\hmm_proto
