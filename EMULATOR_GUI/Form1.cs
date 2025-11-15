using System.Net;
using System.Net.Sockets;

namespace EMULATOR_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateUI(Boolean[] lamps, int encD, int encS, int encI)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateUI(lamps, encD, encS, encI)));
            }
            l_Adpt.Value = lamps[0];
            l_sOut.Value = lamps[1];
            l_sOver.Value = lamps[2];
            l_sHome.Value = lamps[3];
            l_dOver.Value = lamps[4];
            l_dHome.Value = lamps[5];
            l_XHome.Value = lamps[6];
            lbldEnc.Text = encD.ToString();
            lblsEnc.Text = encS.ToString();
            lbliEnc.Text = encI.ToString();

        }

        private void ConnectToServer()
        {



            #region
            // initilize the system
            Boolean[] lamps = [false, false, true, true, true, true,false];
            int encD = 0;
            int encS = 0;
            int encI = 100;
            #endregion
            UpdateUI(lamps, encD, encS, encI);

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint end = new(ip, 2321);
            using TcpListener listener = new(end);

            this.Invoke(new Action(() =>
            {
                labelStatus.Text = "Server On:" + "127.0.0.1";
            }));


            listener.Start();
            using TcpClient handler = listener.AcceptTcpClient();
            using NetworkStream stream = handler.GetStream();
            byte[] buffer = new byte[1024];



                while (true)
                {
                    UpdateUI(lamps, encD, encS, encI);
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read > 0)
                    {
                        //ProcessPacket(buffer);
                    }
                    Thread.Sleep(50);
                }

            
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Task.Run(() => ConnectToServer());
            btnStart.Enabled = false;
        }
    }
}
