
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TalkCalc.Recognizer
{
    public class HTKRecognizer : Recognizer
    {
        public const string HtkStartFile = @"HTK\HVite.exe";
        public const string HtkArgs =
            @"-H HTK\thai.am -C HTK\liverecog.config " +
            @"-w HTK\calc.wdnet -p 0.0 -s 5.0 HTK\calc.dict HTK\tie.list";


        private string _htkPath;
        private ProcessStartInfo _startInfo;

        private HTKTranslator _translator;
        private Process _proc;


        public HTKRecognizer()
        {
            _htkPath = findHtkPath();
            _startInfo = buildHtkStartInfo(_htkPath);

            _translator = new HTKTranslator();
        }


        protected override void StartCore()
        {
            var alreadyRunning = Process.GetProcessesByName(Path.GetFileName(_startInfo.FileName));
            foreach (var process in alreadyRunning)
                process.Kill();

            _proc = new Process();
            _proc.StartInfo = _startInfo;

            _proc.EnableRaisingEvents = true;
            _proc.OutputDataReceived += htkDataReceived;
            _proc.ErrorDataReceived += htkDataReceived;

            _translator.Reset();

            _proc.Start();
            _proc.BeginOutputReadLine();
        }

        void htkDataReceived(object sender, DataReceivedEventArgs e)
        {
            var expr = _translator.Translate(e.Data);

            // avoids re-calculating same result or removing previous result
            var act = new Action(() =>
            {
                if (Result != expr && !string.IsNullOrEmpty(expr))
                    Result = expr;
            });

            if (Dispatcher.Thread != Thread.CurrentThread)
                Dispatcher.Invoke(act);
            else
                act();
        }


        protected override string StopCore()
        {
            _proc.CancelOutputRead();
            _proc.Kill();

            return Result;
        }


        private string findHtkPath()
        {
            var htkPath = Process.GetCurrentProcess()
                .MainModule.FileName;

            htkPath = Path.GetDirectoryName(htkPath);
            htkPath = Path.Combine(htkPath, HtkStartFile);

            return htkPath;
        }

        private ProcessStartInfo buildHtkStartInfo(string htkPath)
        {
            return new ProcessStartInfo
            {
                FileName = htkPath,
                Arguments = HtkArgs,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
        }
    }
}
