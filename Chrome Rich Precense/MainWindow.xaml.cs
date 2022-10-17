using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using discord_rpc;

namespace Chrome_Rich_Precense
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DiscordCheck();
            WaitingDsicordDone();
        }

        

        void DiscordCheck()
        {
            Process[] discordapp = Process.GetProcessesByName("Discord");

            if (discordapp.Length > 0)
            {
                Console.WriteLine("Discord process is found. Starting Discord-RPC module...");
                MainText.Text = "Discord process is found. Starting Discord-RPC module...";
                RPCStart(MainText);
            }
            else
            {
                Console.WriteLine("Discord process is not found.\nFirstly start Discord before starting Download Info.\nRestart or continue without Discord");
                MainText.Text = "Discord process is not found.\nFirstly start Discord before starting Download Info.\nRestart or continue without Discord";
                //Console.WriteLine("Discord process is not found. Waiting for Discord process...");
                return;
            }
        }

        static bool ready = false;
        static RichPresence RPC = new RichPresence { Details = "Details" };
        public static bool DiscordDone;

        public static TextBlock LabelPS;

        static public void RPCStart(TextBlock Label)
        {
            LabelPS = Label;
            Task.Run(Callbacks);
            Run(Label).GetAwaiter().GetResult();
        }

        static async Task Run(TextBlock Label)
        {
            Console.WriteLine("Discord-RPC Module Started!");
            Label.Text = ("Discord-RPC Module Started!");
            if (!ready)
            {
                Console.WriteLine("Connecting...");
                Label.Text = "Connecting...";
                //await Task.Delay(2000);
                DiscordEventHandlers handlers = new DiscordEventHandlers { };
                handlers.ready = Ready;
                handlers.disconnected = Disconnected;
                await discord_rpc.Discord.InitializeAsync("506833843383435275", handlers, 1, "");
            }
        }

        static async Task Callbacks()
        {
            while (true)
            {
                discord_rpc.Discord.RunCallbacks();
                await Task.Delay(100);
            }
        }




        static void Disconnected(int errorCode, string Message)
        {
            discord_rpc.Discord.ClearPresence();
            ready = false;
            Console.WriteLine("Disconnected! {0}", Message);
        }

        static void Ready()
        {
            Console.WriteLine("Connected!");
            ready = true;

            RPC.largeImageKey = "chrome-logo";
            RPC.Details = "Starting...";

            discord_rpc.Discord.UpdatePresence(RPC);

            Console.WriteLine("Discord done");

            DiscordDone = true;
        }

        async void WaitingDsicordDone()
        {
            if (DiscordDone)
            {
                DiscordOK();
            }
            else
            {
                await Task.Delay(100);
                WaitingDsicordDone();
            }
        }

        async public void DiscordOK()
        {
            MainText.Text = "Start initialisation complet!";
            //RPC.Details = "Configuring apps";

            //Discord.UpdatePresence(RPC);

            Process[] chrome = Process.GetProcessesByName("chrome");
            //int i = discordapp.Length;

            if (chrome.Length > 0)
            {
                Console.WriteLine("Chrome process is found!");
                MainText.Text = "Chrome process is found!";
            }
            else
            {
                Console.WriteLine("Chrome process is not found.\nFirstly start Chrome before starting Download Info.\nRestart this apps");
                MainText.Text = "Chrome process is not found.\nFirstly start Chrome before starting Download Info.\nRestart this apps";
                return;
            }
            string OngletName = "Searching...";
            string OldOngletName = "";
            while (true)
            {
                Process[] chrome2 = Process.GetProcessesByName("chrome");
                try
                {
                    for (int i = 0; i < chrome.Length; i++)
                    {
                        if (chrome2[i].MainWindowTitle.ToString() != "")
                            OngletName = chrome2[i].MainWindowTitle.ToString();
                    }
                } catch (Exception e)
                {
                    MainText.Text = "Erreur: " + e;
                }

                int pos = OngletName.LastIndexOf(@" - ") + 1;
                string chromeName = (OngletName.Substring(pos, OngletName.Length - pos));
                try
                {
                    OngletName = OngletName.Replace(chromeName, "");
                } catch (Exception e)
                {
                    MainText.Text = "Erreur: " + e;
                }

                MainText.Text = OngletName;
                RPC.Details = OngletName;
                if (OldOngletName != OngletName)
                    Discord.UpdatePresence(RPC);

                await Task.Delay(1000);
                OldOngletName = OngletName;
            }
        }

        private void CopyRight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This Software was created by OLIVIER Tom (Tom60chat)\n \nClick Yes to see my Youtube Channel\nor\nClick No to make a donation to me :)\n \nClick cancel for return\nYes the method with the buttons is ridiculous", "CopyRight", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
                Process.Start("https://www.youtube.com/channel/UCFmyqR2GRUErhFiuiFFtnKQ");
            if (result == MessageBoxResult.No)
                Process.Start("https://www.paypal.me/Tom60chat");
        }

        private void CopyRight_MouseEnter(object sender, MouseEventArgs e)
        {
            CopyRight.Foreground = Brushes.Blue;
        }

        private void CopyRight_MouseLeave(object sender, MouseEventArgs e)
        {
            CopyRight.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }

        void Loil()
        {
            Console.WriteLine("lol");
        }


    }
}
