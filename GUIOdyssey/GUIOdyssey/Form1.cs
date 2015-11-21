using System;
using System.Windows.Forms;
using NAudio.Wave;
using System.Net;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Data;

namespace GUIOdyssey
{
       
    public partial class Form1 : Form
    {
        private Mp3Player _mp3Player;
        private bool local;
        private string source;
        string path;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
            this.BindDataGridView();
        }

        private void BindDataGridView()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("R", typeof(string)),
                                new DataColumn("Name", typeof(string)),
                                new DataColumn("Genero",typeof(string)) });
            dt.Rows.Add("+", "http://odysseycollection.blob.core.windows.net/odysseymusic/1bbe27bd-164f-4798-9e15-6f4fb2f4bbab/09 Eclipse.mp3", "Romanticas");
            dt.Rows.Add("+", @"C:\romanticas\ahora tu - malu(2).mp3", "Tropical");
            dt.Rows.Add("+", "http://odysseycollection.blob.core.windows.net/odysseymusic/1bbe27bd-164f-4798-9e15-6f4fb2f4bbab/09 Eclipse.mp3", "Romanticas");
            dt.Rows.Add("+", @"C:\romanticas\ahora tu - malu(2).mp3", "Tropical");
            this.dataGridView1.DataSource = dt;
        }

        private void setMp3Scope()
        {

            string root = Path.GetPathRoot(path);
            string indicator = @"C:\";


            if (root.Equals(indicator))
            {
                _mp3Player = new Mp3Player(path);
                _mp3Player.Repeat = true;
                local = true;
            }
            else
            {
                source = path;
                local = false;
            }
        }

       string[] files, paths;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        public volatile bool _QuitThread;

        private void button2_Click(object sender, EventArgs e)
        {
            if (local)
            {
                PauseLocal();
            }
            else
            {
                PauseUrl();
            }
            
        }

        private void PauseUrl()
        {
            if (playbackState == StreamingPlaybackState.Playing || playbackState == StreamingPlaybackState.Buffering)
            {
                waveOut.Pause();
                Debug.WriteLine(String.Format("User requested Pause, waveOut.PlaybackState={0}", waveOut.PlaybackState));
                playbackState = StreamingPlaybackState.Paused;
            }
        }

public async void button1_Click(object sender, EventArgs e)
        {
   
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
  

                for (int i = 0; i < files.Length; i++)
                {
                    string archivo = files[i];
  
                    

                    }
                }

        }

        enum StreamingPlaybackState
        {
            Stopped,
            Playing,
            Buffering,
            Paused
        }

        private BufferedWaveProvider bufferedWaveProvider;
        private IWavePlayer waveOut;
        private volatile StreamingPlaybackState playbackState;
        private volatile bool fullyDownloaded;
        private HttpWebRequest webRequest;
        private VolumeWaveProvider16 volumeProvider;

        delegate void ShowErrorDelegate(string message);

        private void ShowError(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ShowErrorDelegate(ShowError), message);
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void StreamMp3(object state)
        {
            fullyDownloaded = false;
            var url = (string)state;
            webRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp;
            try
            {
                resp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.RequestCanceled)
                {
                    ShowError(e.Message);
                }
                return;
            }
            var buffer = new byte[16384 * 4];

            IMp3FrameDecompressor decompressor = null;
            try
            {
                using (var responseStream = resp.GetResponseStream())
                {
                   var readFullyStream = new ReadFullyStream(responseStream);
                    do
                    {
                        if (IsBufferNearlyFull)
                        {
                            Debug.WriteLine("Buffer getting full, taking a break");
                            Thread.Sleep(500);
                        }
                        else
                        {
                            Mp3Frame frame;
                            try
                            {
                                frame = Mp3Frame.LoadFromStream(readFullyStream);
                            }
                            catch (EndOfStreamException)
                            {
                                fullyDownloaded = true;
                                break;
                            }
                            if (decompressor == null)
                            {
                                decompressor = CreateFrameDecompressor(frame);
                                bufferedWaveProvider = new BufferedWaveProvider(decompressor.OutputFormat);
                                bufferedWaveProvider.BufferDuration = TimeSpan.FromSeconds(20);
                            }
                            if (frame!= null) { 
                            int decompressed = decompressor.DecompressFrame(frame, buffer, 0);
                            bufferedWaveProvider.AddSamples(buffer, 0, decompressed);}
                        }
                    } while (playbackState != StreamingPlaybackState.Stopped);
                    Debug.WriteLine("Exiting");
                    decompressor.Dispose();
                }
            }
            finally
            {
                if (decompressor != null)
                {
                    decompressor.Dispose();
                }
            }
        }

        private static IMp3FrameDecompressor CreateFrameDecompressor(Mp3Frame frame)
        {
            WaveFormat waveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2,
                frame.FrameLength, frame.BitRate);
            return new AcmMp3FrameDecompressor(waveFormat);
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            
            setMp3Scope();

            if (local)
            {
                PlayLocal();
            }
            else
            {
            PlayUrl();    
            }
            
        }

        private void PlayUrl()
        {
            if (playbackState == StreamingPlaybackState.Stopped)
            {
                //string url = @"C:\Users\Kevin\Music\iTunes\iTunes Media\Music\Foo Fighters\Greatest Hits\14 Wheels";
                string url = "https://nyportalvhds1vwfglv3fyfp.blob.core.windows.net/odysseycloudmusiclibrary/Skyjelly_-_09_-_Sixes_Are_Silent_Alternate_Version%20(1).mp3";
                playbackState = StreamingPlaybackState.Buffering;
                bufferedWaveProvider = null;
                ThreadPool.QueueUserWorkItem(StreamMp3, source);
                timer1.Enabled = true;
            }
            else if (playbackState == StreamingPlaybackState.Paused)
            {
                playbackState = StreamingPlaybackState.Buffering;
            }
        }
        private void PlayLocal()
        {
            if (_mp3Player != null)
            {
                _mp3Player.Play();
            }   
        }

        private void PauseLocal()
        {
            if (_mp3Player!= null)
            {
                _mp3Player.Stop();
            }
        }

        private void StopLocal()
        {
            if (_mp3Player != null)
            {
                _mp3Player.Stop();
                _mp3Player.Dispose();
            }
        }

        private void StopPlayback()
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                if (!fullyDownloaded)
                {
                    webRequest.Abort();
                }

                playbackState = StreamingPlaybackState.Stopped;
                if (waveOut != null)
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                timer1.Enabled = false;
                // n.b. streaming thread may not yet have exited
                Thread.Sleep(500);
                //ShowBufferState(0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                if (waveOut == null && bufferedWaveProvider != null)
                {
                    Debug.WriteLine("Creating WaveOut Device");
                    waveOut = CreateWaveOut();
                    //waveOut.PlaybackStopped += OnPlaybackStopped;
                    volumeProvider = new VolumeWaveProvider16(bufferedWaveProvider);
                    //volumeProvider.Volume = volumeSlider1.Volume;
                    waveOut.Init(volumeProvider);
                }
                else if (bufferedWaveProvider != null)
                {
                    var bufferedSeconds = bufferedWaveProvider.BufferedDuration.TotalSeconds;
                    //ShowBufferState(bufferedSeconds);
                    // make it stutter less if we buffer up a decent amount before playing
                    if (bufferedSeconds < 0.5 && playbackState == StreamingPlaybackState.Playing && !fullyDownloaded)
                    {
                        Pause();
                    }
                    else if (bufferedSeconds > 4 && playbackState == StreamingPlaybackState.Buffering)
                    {
                        Play();
                    }
                    else if (fullyDownloaded && bufferedSeconds == 0)
                    {
                        Debug.WriteLine("Reached end of stream");
                        playbackState = StreamingPlaybackState.Stopped;
                        StopPlayback();
                    }
                }

            }
        }

        private void Play()
        {
            waveOut.Play();
            playbackState = StreamingPlaybackState.Playing;
        }

        private void Pause()
        {
            playbackState = StreamingPlaybackState.Buffering;
            waveOut.Pause();
        }

        private IWavePlayer CreateWaveOut()
        {
            return new WaveOut();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            setMp3Scope();

            if (local)
            {
                PlayLocal();
            }
            else
            {
                PlayUrl();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (local)
            {
                PauseLocal();
            }
            else
            {
                PauseUrl();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            StopPlayback();
            StopLocal();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {


                for (int i = 0; i < files.Length; i++)
                {
                    string archivo = files[i];



                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            StopPlayback();
            StopLocal();
            path = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            label2.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex-1].Value.ToString();
            label3.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex+1].Value.ToString();
            setMp3Scope();

            if (local)
            {
                PlayLocal();
            }
            else
            {
                PlayUrl();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private bool IsBufferNearlyFull
        {
            get
            {
                return bufferedWaveProvider != null &&
                       bufferedWaveProvider.BufferLength - bufferedWaveProvider.BufferedBytes
                       < bufferedWaveProvider.WaveFormat.AverageBytesPerSecond / 4;
            }
        }


    }
    }

