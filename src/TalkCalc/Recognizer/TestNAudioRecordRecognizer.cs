
using System.Linq.Expressions;

using NAudio.Wave;

namespace TalkCalc.Recognizer
{
    public class TestNAudioRecordRecognizer : Recognizer
    {
        WaveIn _wave;
        WaveFileWriter _writer;

        protected override void StartCore()
        {
            _wave = new WaveIn();
            _writer = new WaveFileWriter(@"C:\temp\test.wav", _wave.WaveFormat);

            _wave.DataAvailable += (sender, e) =>
                _writer.WriteData(e.Buffer, 0, e.BytesRecorded);

            _wave.StartRecording();
        }

        protected override string StopCore()
        {
            _wave.StopRecording();
            _writer.Flush();
            _writer.Close();

            _wave.Dispose();
            _writer.Dispose();

            return "Recorded c:\temp\test.wav";
        }
    }
}
