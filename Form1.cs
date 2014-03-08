using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetbarManagerClient {
    public partial class ClientForm : Form {
        private int serverport = 2500;
        private TcpClient client_socket;
        private NetworkStream ns;
        private StreamReader sr;
        private bool is_connect = false;

        public ClientForm() {
            InitializeComponent();
        }

        private void connect_to_server(){
             try {
                client_socket = new TcpClient("127.0.0.1", serverport);
                ns = client_socket.GetStream();
                sr = new StreamReader(ns);
                is_connect = true;
                Console.WriteLine("Connected to Server");
            } catch (Exception ex) {
                Console.WriteLine("Cannot connect to server");
            }
        }
        private void button1_Click(object sender, EventArgs e) {
            connect_to_server();
            if(is_connect){
                register_on_server();
            }


            }
        private void register_on_server() {
            try {
                string command = "CONNECT|" + "wangxinalex";
                Byte[] send_bytes = System.Text.Encoding.Default.GetBytes(command);
                ns.Write(send_bytes, 0, send_bytes.Length);
                string response = sr.ReadLine();
                Console.WriteLine(response);
                string[] tokens = response.Trim().Split('|');
                if (tokens[0] == "CONNECT_ACK") {
                    Console.WriteLine("Success connected to server");

                }
            } catch (Exception ex) {
                Console.WriteLine("Register failed");
            }
        }
    }
}
