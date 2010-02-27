@echo off
IF NOT EXIST out MKDIR out
IF NOT EXIST out\mfc MKDIR out\mfc
IF NOT EXIST out\am MKDIR out\am
IF NOT EXIST out\am\hmm_0 MKDIR out\am\hmm_0

CALL compile.bat

SETLOCAL ENABLEDELAYEDEXPANSION

rem Extract audio parameters via HCopy
bin\HCopy -T 1 -C in\code.config -S in\code.scp

rem Create a prototype HMM for acoustic model training
bin\HCompV -T 1 -C in\train.config -f 0.01 -m -S in\train.scp -M out\am\hmm_0 in\hmm_proto

rem Copy the prototype for each phonemes so we can train each of them
for /F %%f in (in\monophones.list) do (
	bin\replacetext hmm_proto "%%f" < out\am\hmm_0\hmm_proto > "out\am\hmm_0\%%f"
)

rem Calculate the probabilities for each phonemes and combine them into newMacros
for /L %%i in (1,1,3) do (
	IF NOT EXIST out\am\hmm_%%i MKDIR out\am\hmm_%%i
	IF %%i==1 (
		bin\HERest -T 1 -A -C in\train.config -I in\monophones.mlf -S in\train.scp -d out\am\hmm_0 -M out\am\hmm_1 in\monophones.list
	) ELSE (
		SET /A htk__j=%%i-1
		bin\HERest -T 1 -A -C in\train.config -I in\monophones.mlf -S in\train.scp -H out\am\hmm_!htk__j!\newMacros -M out\am\hmm_%%i in\monophones.list
	)
)


ENDLOCAL