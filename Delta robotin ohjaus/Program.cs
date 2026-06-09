using System;
using Delta_robotin_ohjaus;
using log4net;
using static Delta_robotin_ohjaus.HardwareProtocolClient;


double standardVel = 9999;

log4net.Config.BasicConfigurator.Configure();
HardwareProtocolClient criController = new HardwareProtocolClient();

criController.IPAddress = "localhost"; //192.168.3.11 || locahost
criController.Port = 3921; // 3920 || 3921
criController.flagHideBasicStatusMessages = false;

criController.Connect();

if (!criController.WaitForConnection())
{
    Console.WriteLine("Connection failed exiting");
    return;
}

criController.SetDigitalOutput(22, true);
criController.SetDigitalOutput(23, true);

bool[] dout = criController.GetDigitalOutputs();

for (int i = 0; i < dout.Length; i++)
{
    Console.WriteLine(dout[i] + ",");
}

// HUOM! 5mm < z < 135mm

criController.MoveToCoord(motionType.Cart, 100, 0, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, -100, -100, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, -100, 100, 70, standardVel);
if (!criController.WaitForMove()) { return; }

criController.MoveToCoord(motionType.Cart, 100, 0, 70, standardVel);
if (!criController.WaitForMove()) { return; }

Thread.Sleep(5000);

criController.Disconnect();