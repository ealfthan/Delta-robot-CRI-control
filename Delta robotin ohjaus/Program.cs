using System;
using Delta_robotin_ohjaus;
using log4net;
using static Delta_robotin_ohjaus.HardwareProtocolClient;


double standardVel = 100;

log4net.Config.BasicConfigurator.Configure();
HardwareProtocolClient criController = new HardwareProtocolClient();

criController.IPAddress = "localhost"; //192.168.3.11 || locahost
criController.Port = 3921; // 3920 || 3921

criController.Connect();

if (!criController.WaitForConnection())
{
    Console.WriteLine("Connection failed exiting");
    return;
}

// a small wait should be added if accessing dout state right after setting it since the code might execute before irc has sent an update

criController.SetDigitalOutput(22, true);
criController.SetDigitalOutput(23, true);

Thread.Sleep(2000);

bool[] dout = criController.GetDigitalOutputs();

for (int i = 0; i < dout.Length; i++)
{
    Console.Write(dout[i] + ",");
}

Console.Write("\n");

// HUOM! 2mm < z < 138mm

criController.MoveToCoord(motionType.Cart, 100, 0, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, -100, -100, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, -100, 100, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, 100, 0, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, 0, 0, 138, standardVel);
if (!criController.WaitForMove()) { return; }


Thread.Sleep(5000);

criController.Disconnect();