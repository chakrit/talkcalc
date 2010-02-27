@echo off
IF NOT EXIST out MKDIR out
IF NOT EXIST out\mfc MKDIR out\mfc
IF NOT EXIST out\am MKDIR out\am
IF NOT EXIST out\am\hmm_0 MKDIR out\am\hmm_0

CALL compile.bat

rem Extract audio parameters via HCopy
bin\HCopy -T 1 -C in\code.config -S in\code.scp

rem Create a prototype HMM for acoustic model training
bin\HCompV -T 1 -C in\train.config -f 0.01 -m -S in\train.scp -M out\am\hmm_0 in\hmm_proto

rem Copy the prototype for each phonemes so we can train each of them
for /F %%f in (in\monophones.list) do (
	bin\sed s/'hmm_proto'/'%%f'/ out\am\hmm_0\hmm_proto > "out\am\hmm_0\%%f"
)