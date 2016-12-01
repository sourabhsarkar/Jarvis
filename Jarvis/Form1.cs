using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;
using System.Speech.Synthesis;
using System.Runtime.InteropServices;
using System.Globalization;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Web;
using System.Xml;

namespace Jarvis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SpeechSynthesizer sSynth = new SpeechSynthesizer();
        PromptBuilder pBuilder = new PromptBuilder();
        SpeechRecognitionEngine sRecognize = new SpeechRecognitionEngine(new CultureInfo("en-IN"));
        //MemoryStream outStream = new MemoryStream();
        //SoundPlayer sp = new SoundPlayer();
        Process pr = new Process();
        string name, vn;

        string Temperature;
        string Condition;
        string Humidity;
        string Windspeed;
        string Town;
        string TFCond;
        string TFHigh;
        string TFLow;

        bool exitcondition = false, isMinimized = false, cv = false, sleep = false;

        public void speaktext(string s)
        {
            //sRecognize.RecognizeAsyncCancel();
            //sRecognize.RecognizeAsyncStop();
            pBuilder.ClearContent();
            pBuilder.AppendText(s);
            /*
            outStream.Position = 0;
            sSynth.SetOutputToWaveStream(outStream);
            */
            try
            {
                sSynth.SelectVoice(name);
                //sSynth.SelectVoiceByHints(VoiceGender.Male);
            }
            catch
            {
                foreach (InstalledVoice voice in sSynth.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    name = info.Name;
                    break;
                }
            }
            //sSynth.Rate = -2;
            sSynth.Speak(pBuilder);
            //sRecognize.RecognizeAsyncCancel();
            //sRecognize.RecognizeAsyncStop();
            //sRecognize.RecognizeAsync(RecognizeMode.Multiple);
            /*
            outStream.Position = 0;
            sp.Stream = null;
            sp.Stream = outStream;
            sp.Play();
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (InstalledVoice voice in sSynth.GetInstalledVoices())
            {
                VoiceInfo info = voice.VoiceInfo;
                name = info.Name;
                break;
            }
            GrammarBuilder gb = new GrammarBuilder();
            try
            {
                StreamReader sr = new StreamReader("commands.txt");

                while(sr.Peek()>-1)
                {
                    cmndlist.Items.Add(sr.ReadLine());
                }
                sr.Close();

                gb.Append(new Choices(File.ReadAllLines("commands.txt")));
            }
            catch
            {
                MessageBox.Show("The \"commands\" must not contain any empty lines!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pr.StartInfo.FileName = ("commands.txt");
                pr.Start();
                Application.Exit();
                return;
            }
            gb.Culture = new CultureInfo("en-IN");
            Grammar gr = new Grammar(gb);
            try
            {
                sRecognize.UnloadAllGrammars();
                sRecognize.RecognizeAsyncCancel();
                sRecognize.RequestRecognizerUpdate();
                sRecognize.LoadGrammar(gr);
                //sRecognize.LoadGrammar(new Grammar(new GrammarBuilder("exit")));
                //sRecognize.LoadGrammar(new DictationGrammar());
                sRecognize.SpeechRecognized += sRecognize_SpeechRecognized;
                sRecognize.SetInputToDefaultAudioDevice();
                sRecognize.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                MessageBox.Show("Grammar Builder Error");
                return;
            }
        }

        private void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            sRecognize.RecognizeAsyncCancel();
            //MessageBox.Show("Speech Recognised: " + e.Result.Text.ToString());
            SetText(e.Result.Text.ToString());
            if (e.Result.Text == "sleep")
                sleep = true;
            else if (e.Result.Text == "wake")
                sleep = false;
            if (sleep == false)
            {
                if (exitcondition == true)
                {
                    if (e.Result.Text == "yes")
                    {
                        speaktext("Bye Bye");
                        sSynth.SpeakAsyncCancelAll();
                        Application.Exit();
                    }
                    else if (e.Result.Text == "no" || e.Result.Text == "cancel")
                    {
                        exitcondition = false;
                        speaktext("Exit Aborted.");
                    }
                    else
                        exitcondition = false;
                }
                if (cv == true)
                {
                    switch (e.Result.Text)
                    {
                        case "David":
                            name = "Microsoft David Mobile";
                            cv = false;
                            break;
                        case "Hazel":
                            name = "Microsoft Hazel Mobile";
                            cv = false;
                            break;
                        case "Zira":
                            name = "Microsoft Zira Mobile";
                            cv = false;
                            break;
                    }
                }
                switch (e.Result.Text)
                {
                    case "exit":
                    case "close":
                    case "quit":
                        speaktext("Are you sure you want to exit?");
                        exitcondition = true;
                        break;
                    case "minimize":
                        if (!isMinimized)
                        {
                            Invoke(new Action(() => { this.WindowState = FormWindowState.Minimized; }));
                            isMinimized = true;
                            speaktext("Apologies for coming in your way.");
                        }
                        else
                            speaktext("I am already minimized!");
                        break;
                    case "maximize":
                        if (isMinimized)
                        {
                            Invoke(new Action(() => { this.WindowState = FormWindowState.Normal; }));
                            isMinimized = false;
                            speaktext("I'm back!");
                        }
                        else
                            speaktext("I am already maximized!");
                        break;
                    case "hello":
                    case "hi":
                        //speaktext("Hello,"+Environment.UserName+", What can I do for you?");
                        speaktext("Hello, what can I do for you?");
                        break;
                    case "change voice":
                        cv = true;
                        foreach (InstalledVoice voice in sSynth.GetInstalledVoices())
                        {
                            VoiceInfo info = voice.VoiceInfo;
                            vn += info.Name + ", ";
                        }
                        speaktext("Select a voice ");
                        speaktext(vn);
                        vn = "";
                        break;
                    case "how are you":
                        speaktext("I am fine, and you?");
                        break;
                    case "who are you":
                        speaktext("I am Jarvis your personal assistant");
                        break;
                    case "open firefox":
                    case "browser":
                        Process.Start(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");
                        speaktext("Firing your web browser");
                        break;
                    case "close firefox":
                    case "close browser":
                        killProg("firefox");
                        break;
                    case "next":
                        SendKeys.SendWait("{RIGHT}");
                        break;
                    case "previous":
                    case "back":
                        SendKeys.SendWait("{LEFT}");
                        break;
                    case "play":
                    case "pause":
                        SendKeys.SendWait(" ");
                        break;
                    case "escape":
                        SendKeys.SendWait("{ESC}");
                        break;
                    case "what is today's date":
                    case "date":
                        pBuilder.ClearContent();
                        pBuilder.AppendText("Today's date is, ");
                        pBuilder.AppendTextWithHint(DateTime.Now.ToString("M/dd/yyyy"), SayAs.Date);
                        sSynth.SelectVoice(name);
                        //sSynth.Rate = -2;
                        sSynth.Speak(pBuilder);
                        break;
                    case "what time is now":
                    case "time":
                        pBuilder.ClearContent();
                        pBuilder.AppendText("It is, ");
                        pBuilder.AppendTextWithHint(DateTime.Now.ToString("hh:mm tt"), SayAs.Time12);
                        sSynth.SelectVoice(name);
                        //sSynth.Rate = -2;
                        sSynth.Speak(pBuilder);
                        break;
                    case "how's the weather":
                    case "weather":
                        if (getweather())
                            speaktext("The weather in " + Town + " is " + Condition + " at " + Temperature + " degrees celsius. There is a wind speed of " + Windspeed + " kilometers per hour and a humidity of " + Humidity);
                        else
                            speaktext("Sorry! Error retrieving weather info.");
                        break;
                    case "what's tomorrow's forecast":
                    case "forecast":
                        getweather();
                        speaktext("It looks like tomorrow will be " + TFCond + " with a high of " + TFHigh + " degree centrigrade and a low of " + TFLow + " degree centrigrade");
                        break;
                    default:
                        if (e.Result.Text.ToString().ToLower().Contains("search"))
                        {
                            string query = e.Result.Text.ToString().Replace("search", "");
                            query = HttpUtility.UrlEncode(query);
                            pr.StartInfo.FileName = "http://google.com/search?q=" + query;
                            pr.Start();
                            speaktext("Here is your search result.");
                        }
                        break;
                }
            }
            sRecognize.RecognizeAsync(RecognizeMode.Multiple);
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        { 
            if(this.cmnddisp.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.cmnddisp.Items.Add(text);
            }
        }

        public void killProg(String s)
        {
            System.Diagnostics.Process[] procs = null;
            try
            {
                procs = Process.GetProcessesByName(s);
                if (procs.Length > 0)
                {
                    Process prog = procs[0];

                    if (!prog.HasExited)
                        prog.Kill();
                }
            }
            finally
            {
                if(procs != null)
                {
                    foreach (Process p in procs)
                        p.Dispose();
                }
            }
            procs = null;
        }

        public Boolean getweather()
        {
            string query = String.Format("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22Jalandhar%2C%20Punjab%22)%20and%20u%3D'c'&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
            XmlDocument wdata = new XmlDocument();
            try
            {
                wdata.Load(query);
            
                XmlNamespaceManager manager = new XmlNamespaceManager(wdata.NameTable);
                manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");
            
                XmlNode channel = wdata.SelectSingleNode("query").SelectSingleNode("results").SelectSingleNode("channel");
                XmlNodeList nodes = wdata.SelectNodes("query/results/channel");

                Temperature = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition",manager).Attributes["temp"].Value;

                Condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;

                Humidity = channel.SelectSingleNode("yweather:atmosphere", manager).Attributes["humidity"].Value;

                Windspeed = channel.SelectSingleNode("yweather:wind", manager).Attributes["speed"].Value;

                Town = channel.SelectSingleNode("yweather:location",manager).Attributes["city"].Value;
                if (Town == "")
                    Town = "Kolkata";

                TFCond = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["text"].Value;

                TFHigh = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["high"].Value;

                TFLow = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["low"].Value;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
